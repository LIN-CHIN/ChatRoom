using ChatRoomModels.DB;
using ChatRoomModels;
using MQTTnet.Protocol;
using MQTTnet.Server;
using MQTTnet;
using Newtonsoft.Json;
using System.Text;
using MqttBroker.DAOs.MessagesDAO;
using MqttBroker.DAOs.UserDAO;
using MqttBroker.Handlers.Interfaces;

namespace MqttBroker
{
	/// <summary>
	/// 實作MqttServer事件
	/// </summary>
	public class MqttServerEvent : IMqttServerEvent
	{
		private readonly IWriteMessageHandler _consoleWithLogHandler;
		private readonly IUserDAO _userDAO;
		private readonly IMessageDAO _messageDAO;

		public MqttServerEvent( IWriteMessageHandler consoleWithLogHandler,
								IUserDAO userDAO,
								IMessageDAO messageDAO )
		{
			_consoleWithLogHandler = consoleWithLogHandler;
			_userDAO = userDAO;
			_messageDAO = messageDAO;
		}

		///<inheritdoc/>
		public void ConnectionValidator( MqttConnectionValidatorContext context )
		{
			Users? user = _userDAO.Get( context.Username );

			if( user != null && context.Password == user.Pwd ) {
				context.ReasonCode = MqttConnectReasonCode.Success;

				//紀錄流水號id 
				context.SessionItems.Add( "Id", user.Id );
			}
			else {
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"User '{context.Username}' login attempt failed" );
				context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
			}
		}

		///<inheritdoc/>
		public void InterceptMessage( MqttApplicationMessageInterceptorContext context )
		{
			//取得user的流水號
			var id = context.SessionItems[ "Id" ];

			var chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString( context.ApplicationMessage.Payload ) );

			_messageDAO.Insert( chatRoomPayload!.ToMessagesEntity( Convert.ToInt64( id ) ) );

			_consoleWithLogHandler.WriteConsoleWithInfoLog(
				$"Topic: {context.ApplicationMessage.Topic}, Message: {chatRoomPayload!.ToMessagesEntity( Convert.ToInt64( id ) )}" );

		}

		///<inheritdoc/>
		public void OnApplicationMessageReceived( MqttApplicationMessageReceivedEventArgs args )
		{
			ChatRoomPayload chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString( args.ApplicationMessage.Payload ) )!;

			//加上ClientId 到payload中
			chatRoomPayload.ClientId = args.ClientId;

			//重新包裝payload
			args.ApplicationMessage.Payload = Encoding.UTF8.GetBytes( chatRoomPayload.ToChatString() );

			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId} publish message': Topic={args.ApplicationMessage.Topic}, Payload={chatRoomPayload.Message}" );
		}

		///<inheritdoc/>
		public void OnClientConnected( MqttServerClientConnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' connected." );
		}

		///<inheritdoc/>
		public void OnClientDisconnected( MqttServerClientDisconnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' disconnected." );
		}

		///<inheritdoc/>
		public void OnClientSubscribed( MqttServerClientSubscribedTopicEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' subscribed to topic '{args.TopicFilter.Topic}' with QoS {args.TopicFilter.QualityOfServiceLevel}." );
		}

		///<inheritdoc/>
		public void OnClientUnsubscribed( MqttServerClientUnsubscribedTopicEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' unsubscribed from topic '{args.TopicFilter}'" );
		}
	}
}
