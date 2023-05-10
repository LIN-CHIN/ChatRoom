using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttBroker
{
	public interface IMqttServerEvent
	{
		/// <summary>
		/// 連線驗證
		/// </summary>
		/// <param name="context"></param>
		void ConnectionValidator( MqttConnectionValidatorContext context );

		/// <summary>
		/// 攔截訊息
		/// </summary>
		/// <param name="context"></param>
		void InterceptMessage( MqttApplicationMessageInterceptorContext context );

		/// <summary>
		/// 接收訊息事件
		/// </summary>
		/// <param name="args"></param>
		void OnApplicationMessageReceived( MqttApplicationMessageReceivedEventArgs args );

		/// <summary>
		/// 客戶端連線後的事件
		/// </summary>
		/// <param name="args"></param>
		void OnClientConnected( MqttServerClientConnectedEventArgs args );

		/// <summary>
		/// 客戶端離線後的事件
		/// </summary>
		/// <param name="args"></param>
		void OnClientDisconnected( MqttServerClientDisconnectedEventArgs args );

		/// <summary>
		/// 客戶訂閱事件
		/// </summary>
		/// <param name="args"></param>
		void OnClientSubscribed( MqttServerClientSubscribedTopicEventArgs args );

		/// <summary>
		/// 客戶取消訂閱事件
		/// </summary>
		/// <param name="args"></param>
		void OnClientUnsubscribed( MqttServerClientUnsubscribedTopicEventArgs args );
	}
}
