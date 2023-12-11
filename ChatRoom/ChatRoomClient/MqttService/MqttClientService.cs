using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using ChatRoomClient.MqttService.Interfaces;
using ChatRoomModels;
using Newtonsoft.Json;
using MQTTnet.Client.Unsubscribing;
using ChatRoomClient.Settings;

namespace ChatRoomClient.MqttService
{
	/// <summary>
	/// MQTT Client 的 Service
	/// </summary>
    public class MqttClientService : IMqttClientService
	{
		private readonly MqttBrokerInfo _mqttBrokerInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mqttBrokerInfo"></param>
        public MqttClientService(MqttBrokerInfo mqttBrokerInfo) 
		{
            _mqttBrokerInfo = mqttBrokerInfo;
		}

		///<inheritdoc/>
		public async Task<IMqttClient> CreateMqttClient( string userId,
														string userPwd,
														Action<MqttClientConnectedEventArgs> onConnectedHandler,
														Action<MqttClientDisconnectedEventArgs> onDisConnctedHandler )
		{
			IMqttClient mqttClient ;

			//建立MQTT client
			var factory = new MqttFactory();
			mqttClient = factory.CreateMqttClient();

			//建立MQTT連線選項
			var options = new MqttClientOptionsBuilder()
				.WithTcpServer(_mqttBrokerInfo.Host,
                    _mqttBrokerInfo.Port ) //指定MQTT broker的IP或網域名稱
				.WithCredentials( userId, userPwd )
				.WithClientId( userId )
				.Build();

			mqttClient.UseConnectedHandler( onConnectedHandler );
			mqttClient.UseDisconnectedHandler( onDisConnctedHandler );

			//連線到MQTT broker
			await mqttClient.ConnectAsync( options );

			return mqttClient;
		}

		///<inheritdoc/>
		public void DisconnectMqttClient( IMqttClient mqttClient )
		{
			mqttClient.DisconnectAsync( new MqttClientDisconnectOptions() { ReasonCode = MqttClientDisconnectReason.NormalDisconnection } );
		}

		///<inheritdoc/>
		public bool IsConnection( IMqttClient mqttClient )
		{
			if( mqttClient == null ) {
				return false;
			}

			if( !mqttClient.IsConnected ) {
				return false;
			}

			return true;
		}

		///<inheritdoc/>
		public void Publish( IMqttClient mqttClient, string message, string topic )
		{
			ChatRoomPayload chatRoomPayload = new ChatRoomPayload()
			{
				Topic = topic,
				Message = message
			};

			mqttClient.PublishAsync( new MqttApplicationMessageBuilder()
					  .WithTopic( topic )
					  .WithPayload( JsonConvert.SerializeObject( chatRoomPayload ) )
					  .WithQualityOfServiceLevel( MqttQualityOfServiceLevel.AtLeastOnce )
					  .Build() );
		}

		///<inheritdoc/>
		public void Subscribe( IMqttClient mqttClient, string topic )
		{
			mqttClient.SubscribeAsync(
				new MqttTopicFilterBuilder()
					.WithTopic( topic )
					.Build() );
		}

		///<inheritdoc/>
		public void UnSubscribe( IMqttClient mqttClient, string topic )
		{
			mqttClient.UnsubscribeAsync(
				new MqttClientUnsubscribeOptionsBuilder()
					.WithTopicFilter( topic )
					.Build() );
		}
	}
}
