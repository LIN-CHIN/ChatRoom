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

namespace MqttBroker.Services
{
    public class MqttServerService : IMqttServerService
	{
		private readonly IWriteMessageHandler _consoleWithLogHandler;

		public MqttServerService( IWriteMessageHandler consoleWithLogHandler )
		{
			_consoleWithLogHandler = consoleWithLogHandler;
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
				_consoleWithLogHandler.WriteConsoleWithLog( $"Client '{e.ClientId}' connected." );
			} );

			mqttServer.UseClientDisconnectedHandler( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithLog( $"Client '{e.ClientId}' disconnected." );

			} );

			mqttServer.UseApplicationMessageReceivedHandler( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithLog( $"Message received from client '{e.ClientId}': Topic={e.ApplicationMessage.Topic}, Payload={e.ApplicationMessage.Payload}" );
			} );

			mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedHandlerDelegate( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithLog( $"Client '{e.ClientId}' subscribed to topic '{e.TopicFilter.Topic}' with QoS {e.TopicFilter.QualityOfServiceLevel}." );
			} );

			mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate( e =>
			{
				_consoleWithLogHandler.WriteConsoleWithLog( $"Client '{e.ClientId}' unsubscribed from topic '{e.TopicFilter}'" );
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
			if( context.Username == "admin" && context.Password == "1234" ) {
				context.ReasonCode = MqttConnectReasonCode.Success;
			}
			else {
				_consoleWithLogHandler.WriteConsoleWithLog( $"UserName: {context.Username} 嘗試登入失敗" );
				context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
			}
		}

		/// <summary>
		/// 攔截訊息
		/// </summary>
		/// <param name="context"></param>
		private void InterceptMessage( MqttApplicationMessageInterceptorContext context )
		{
			_consoleWithLogHandler.WriteConsoleWithLog(
				$"Topic: {context.ApplicationMessage.Topic}, msg: {Encoding.UTF8.GetString( context.ApplicationMessage.Payload )}" );

		}
	}
}
