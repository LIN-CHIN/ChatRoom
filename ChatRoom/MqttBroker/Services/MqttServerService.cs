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
using MqttBroker.DAOs.UserDAO;
using MqttBroker.DAOs.MessagesDAO;
using MqttBroker.Enums;
using ChatRoomModels.DB;
using ChatRoomModels;
using Newtonsoft.Json;

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

			mqttServer.UseClientConnectedHandler( OnClientConnected );
			mqttServer.UseClientDisconnectedHandler( OnClientDisconnected );
			mqttServer.UseApplicationMessageReceivedHandler( OnApplicationMessageReceived );
			mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedTopicHandlerDelegate(OnClientSubscribed);
			mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate( OnClientUnsubscribed );

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
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"User '{context.Username}' login attempt failed" );
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

			var chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString(context.ApplicationMessage.Payload) );

			_messageDAO.Insert( chatRoomPayload!.ToMessagesEntity(Convert.ToInt64(id)) );

			_consoleWithLogHandler.WriteConsoleWithInfoLog(
				$"Topic: {context.ApplicationMessage.Topic}, Message: {chatRoomPayload!.ToMessagesEntity(Convert.ToInt64(id))}" );

		}

		/// <summary>
		/// 接收訊息事件
		/// </summary>
		/// <param name="args"></param>
		private void OnApplicationMessageReceived( MqttApplicationMessageReceivedEventArgs args )
		{
			ChatRoomPayload chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString( args.ApplicationMessage.Payload ) )!;

			//加上ClientId 到payload中
			chatRoomPayload.ClientId = args.ClientId;

			//重新包裝payload
			args.ApplicationMessage.Payload = Encoding.UTF8.GetBytes( chatRoomPayload.ToChatString() );

			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId} publish message': Topic={args.ApplicationMessage.Topic}, Payload={chatRoomPayload.Message}" );
		}

		/// <summary>
		/// 客戶端連線後的事件
		/// </summary>
		/// <param name="args"></param>
		private void OnClientConnected( MqttServerClientConnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' connected." );
		}

		/// <summary>
		/// 客戶端離線後的事件
		/// </summary>
		/// <param name="args"></param>
		private void OnClientDisconnected( MqttServerClientDisconnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' disconnected." );
		}


		/// <summary>
		/// 客戶訂閱事件
		/// </summary>
		/// <param name="args"></param>
		private void OnClientSubscribed( MqttServerClientSubscribedTopicEventArgs args)
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' subscribed to topic '{args.TopicFilter.Topic}' with QoS {args.TopicFilter.QualityOfServiceLevel}." );
		}

		/// <summary>
		/// 客戶取消訂閱事件
		/// </summary>
		/// <param name="args"></param>
		private void OnClientUnsubscribed( MqttServerClientUnsubscribedTopicEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' unsubscribed from topic '{args.TopicFilter}'" );
		}
	}
}
