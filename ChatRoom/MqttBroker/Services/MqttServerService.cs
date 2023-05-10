using MQTTnet.Server;
using MQTTnet;
using MqttBroker.Services.Interfaces;
using MQTTnet.Client;
using ChatRoomModels;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using MqttBroker.DAOs.MessagesDAO;
using MqttBroker.DAOs.UserDAO;
using MqttBroker.Handlers.Interfaces;
using ChatRoomModels.DB;
using System.Text;

namespace MqttBroker.Services
{
    public class MqttServerService : IMqttServerService
	{
		IMqttServer mqttServer;
		private readonly IWriteMessageHandler _consoleWithLogHandler;
		private readonly IUserDAO _userDAO;
		private readonly IMessageDAO _messageDAO;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="consoleWithLogHandler"></param>
		/// <param name="userDAO"></param>
		/// <param name="messageDAO"></param>
		public MqttServerService( IWriteMessageHandler consoleWithLogHandler,
								  IUserDAO userDAO,
								  IMessageDAO messageDAO )
		{
			_consoleWithLogHandler = consoleWithLogHandler;
			_userDAO = userDAO;
			_messageDAO = messageDAO;
		}

		///<inheritdoc/>
		public void StartMqttServer()
		{
			var options = new MqttServerOptionsBuilder()
				.WithDefaultEndpoint()
				.WithDefaultEndpointPort( 1883 )
				.WithConnectionValidator( VerifyConnection )
				.WithApplicationMessageInterceptor( InterceptMessage )
				.Build();

			mqttServer = new MqttFactory().CreateMqttServer();

			mqttServer.UseClientConnectedHandler( OnClientConnected );
			mqttServer.UseClientDisconnectedHandler( OnClientDisconnected );
			mqttServer.UseApplicationMessageReceivedHandler( OnApplicationMessageReceived );
			mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedTopicHandlerDelegate( OnClientSubscribed );
			mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate( OnClientUnsubscribed );

			mqttServer.StartAsync( options );

		}

		/// <summary>
		/// 發布訊息
		/// </summary>
		/// <param name="clientId">ClientID</param>
		/// <param name="topic">主題</param>
		/// <param name="payload">訊息</param>
		private void PublishMessage( string clientId, string topic, string payload )
		{
			ChatRoomPayload chatRoomPayload = new ChatRoomPayload
			{
				ClientId = clientId,
				Topic = topic,
				Message = payload
			};

			var message = new MqttApplicationMessageBuilder()
							.WithTopic( topic )
							.WithPayload( JsonConvert.SerializeObject( chatRoomPayload ) )
							.WithQualityOfServiceLevel( MqttQualityOfServiceLevel.AtLeastOnce )
							.Build();


			mqttServer.PublishAsync( message );
		}

		/// <summary>
		/// 驗證連線
		/// </summary>
		/// <param name="context"></param>
		public void VerifyConnection( MqttConnectionValidatorContext context )
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

		/// <summary>
		/// 攔截訊息
		/// </summary>
		/// <param name="context"></param>
		public void InterceptMessage( MqttApplicationMessageInterceptorContext context )
		{
			var chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString( context.ApplicationMessage.Payload ) );

			//如果是系統發送的訊息
			if( chatRoomPayload!.ClientId != "system" ) 
			{
				//取得user的流水號
				var id = context.SessionItems[ "Id" ];
				_messageDAO.Insert( chatRoomPayload!.ToMessagesEntity( Convert.ToInt64( id ) ) );
			}
			
			_consoleWithLogHandler.WriteConsoleWithInfoLog(
				$"Topic: {context.ApplicationMessage.Topic}, Message: {chatRoomPayload.ToChatString()}" );

		}

		/// <summary>
		/// 接收訊息事件
		/// </summary>
		/// <param name="args"></param>
		public void OnApplicationMessageReceived( MqttApplicationMessageReceivedEventArgs args )
		{
			ChatRoomPayload chatRoomPayload = JsonConvert.DeserializeObject<ChatRoomPayload>( Encoding.UTF8.GetString( args.ApplicationMessage.Payload ) )!;

			//如果不是系統發送的訊息
			if( chatRoomPayload!.ClientId != "system" ) {
				//加上ClientId 到payload中
				chatRoomPayload.ClientId = args.ClientId;

				//重新包裝payload
				args.ApplicationMessage.Payload = Encoding.UTF8.GetBytes( chatRoomPayload.ToChatString() );
				_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId} publish message': Topic={args.ApplicationMessage.Topic}, Payload={chatRoomPayload.Message}" );
			}
			else 
			{
				//重新包裝payload
				args.ApplicationMessage.Payload = Encoding.UTF8.GetBytes( chatRoomPayload.Message );
			}
		}

		/// <summary>
		/// 連線事件
		/// </summary>
		/// <param name="args"></param>
		public void OnClientConnected( MqttServerClientConnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' connected." );
		}

		/// <summary>
		/// 離線事件
		/// </summary>
		/// <param name="args"></param>
		public void OnClientDisconnected( MqttServerClientDisconnectedEventArgs args )
		{
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' disconnected." );
		}

		/// <summary>
		/// 訂閱事件
		/// </summary>
		/// <param name="args"></param>
		public void OnClientSubscribed( MqttServerClientSubscribedTopicEventArgs args )
		{
			PublishMessage( "system", args.TopicFilter.Topic, $"'{args.ClientId}' 加入聊天" );
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' subscribed to topic '{args.TopicFilter.Topic}' with QoS {args.TopicFilter.QualityOfServiceLevel}." );
		}

		/// <summary>
		/// 取消訂閱事件
		/// </summary>
		/// <param name="args"></param>
		public void OnClientUnsubscribed( MqttServerClientUnsubscribedTopicEventArgs args )
		{
			PublishMessage( "system", args.TopicFilter, $"'{args.ClientId}' 已離開房間" );
			_consoleWithLogHandler.WriteConsoleWithInfoLog( $"Client '{args.ClientId}' unsubscribed from topic '{args.TopicFilter}'" );
		}
	}
}
