using MQTTnet.Protocol;
using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MqttBroker.Handlers.Interfaces;
using MqttBroker.Services.Interfaces;
using ChatRoomModels;
using MqttBroker.DAOs.UserDAO;
using MqttBroker.DAOs.MessagesDAO;
using MqttBroker.Enums;

namespace MqttBroker.Services
{
    public class MqttServerService : IMqttServerService
	{
		private readonly IWriteMessageHandler _consoleWithLogHandler;
		private readonly IUserDAO _userDAO;
		private readonly IMessageDAO _messageDAO;

		public MqttServerService( IWriteMessageHandler consoleWithLogHandler, IUserDAO userDAO,
								  IMessageDAO messageDAO)
		{
			_consoleWithLogHandler = consoleWithLogHandler;
			_userDAO = userDAO;
			_messageDAO = messageDAO;
		}

		///<inheritdoc/>
		public async Task<IMqttServer> StartMqttServer()
		{
			IMqttServer mqttServer;

			var options = new MqttServerOptionsBuilder()
				.WithDefaultEndpoint()
				.WithDefaultEndpointPort( 1883 )
				.WithConnectionValidator( ConnectionValidator )
				.WithApplicationMessageInterceptor( InterceptMessage )
				.Build();

			mqttServer = new MqttFactory().CreateMqttServer();

			mqttServer.UseClientConnectedHandler( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{e.ClientId}' connected." );

			mqttServer.UseClientDisconnectedHandler( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{e.ClientId}' disconnected." );

			} );
			} );

			mqttServer.UseApplicationMessageReceivedHandler( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Message received from client '{e.ClientId}': Topic={e.ApplicationMessage.Topic}, Payload={e.ApplicationMessage.Payload}" );
			} );

			mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedHandlerDelegate( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{e.ClientId}' subscribed to topic '{e.TopicFilter.Topic}' with QoS {e.TopicFilter.QualityOfServiceLevel}." );
			} );

			mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{e.ClientId}' unsubscribed from topic '{e.TopicFilter}'" );
			} );

			await mqttServer.StartAsync( options );

			return mqttServer;
		}

		/// <summary>
		/// 連線驗證
		/// </summary>
		/// <param name="context"></param>
		private void ConnectionValidator( MqttConnectionValidatorContext context )
		{
			Users? user = _userDAO.Get( context.Username );

			if( user != null && context.Password == user.Pwd ) 
			{
				context.ReasonCode = MqttConnectReasonCode.Success;
				
				//紀錄流水號id 
				context.SessionItems.Add( "Id", user.Id );
			}
			else
			{
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"UserName: {context.Username} 嘗試登入失敗" );
				context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
			}
		}

		/// <summary>
		/// 攔截訊息
		/// </summary>
		/// <param name="context"></param>
		private void InterceptMessage( MqttApplicationMessageInterceptorContext context )
		{
			//取得user的流水號
			var id = context.SessionItems["Id"];
			
			Messages message = new Messages()
			{
				Message = Encoding.UTF8.GetString( context.ApplicationMessage.Payload ),
				Topic = context.ApplicationMessage.Topic,
				UserId = Convert.ToInt64( id )
			};
			
			_messageDAO.Insert( message );

			_consoleWithLogHandler.WriteConsoleWithInfoLog(
				$"Topic: {context.ApplicationMessage.Topic}, msg: {Encoding.UTF8.GetString( context.ApplicationMessage.Payload )}" );

		}
	}
}
