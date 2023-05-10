using MQTTnet.Server;
using MQTTnet;
using MqttBroker.Services.Interfaces;

namespace MqttBroker.Services
{
    public class MqttServerService : IMqttServerService
	{
		private readonly IMqttServerEvent _mqttServerEvent;

		public MqttServerService(IMqttServerEvent mqttServerEvent )
		{
			_mqttServerEvent = mqttServerEvent;
		}

		///<inheritdoc/>
		public async Task<IMqttServer> StartMqttServer()
		{
			IMqttServer mqttServer;

			var options = new MqttServerOptionsBuilder()
				.WithDefaultEndpoint()
				.WithDefaultEndpointPort( 1883 )
				.WithConnectionValidator( _mqttServerEvent.ConnectionValidator )
				.WithApplicationMessageInterceptor( _mqttServerEvent.InterceptMessage )
				.Build();

			mqttServer = new MqttFactory().CreateMqttServer();

			mqttServer.UseClientConnectedHandler( _mqttServerEvent.OnClientConnected );
			mqttServer.UseClientDisconnectedHandler( _mqttServerEvent.OnClientDisconnected );
			mqttServer.UseApplicationMessageReceivedHandler( _mqttServerEvent.OnApplicationMessageReceived );
			mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedTopicHandlerDelegate( _mqttServerEvent.OnClientSubscribed );
			mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate( _mqttServerEvent.OnClientUnsubscribed );

			await mqttServer.StartAsync( options );

			return mqttServer;
		}
	}
}
