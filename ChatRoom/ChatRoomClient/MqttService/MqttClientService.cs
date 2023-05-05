using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomClient.MqttService.Interfaces;

namespace ChatRoomClient.MqttService
{
    public class MqttClientService : IMqttClientService
	{

		///<inheritdoc/>
		public async Task<IMqttClient> CreateMqttClient( string userId,
														string userPwd,
														Action<MqttClientConnectedEventArgs> onConnectedHandler,
														Action<MqttClientDisconnectedEventArgs> onDisConnctedHandler )
		{
			IMqttClient mqttClient = null;

			//建立MQTT client
			var factory = new MqttFactory();
			mqttClient = factory.CreateMqttClient();

			//建立MQTT連線選項
			var options = new MqttClientOptionsBuilder()
				.WithTcpServer( "127.0.0.1", 8888 ) //指定MQTT broker的IP或網域名稱
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
			mqttClient.PublishAsync( new MqttApplicationMessageBuilder()
					  .WithTopic( topic )
					  .WithPayload( Encoding.UTF8.GetBytes( message ) )
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
	}
}
