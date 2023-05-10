using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomClient.MqttService.Interfaces
{
    /// <summary>
    /// MQTT Client 的 Service Interface
    /// </summary>
    public interface IMqttClientService
    {
        /// <summary>
        /// 建立連線
        /// </summary>
        /// <param name="userId">使用者帳號</param>
        /// <param name="userPwd">使用者密碼</param>
        /// <param name="onConnectedHandler">連線事件處理</param>
        /// <param name="onDisConnctedHandler">離線事件處理</param>
        /// <returns></returns>
        Task<IMqttClient> CreateMqttClient(
            string userId,
            string userPwd,
            Action<MqttClientConnectedEventArgs> onConnectedHandler,
            Action<MqttClientDisconnectedEventArgs> onDisConnctedHandler);

        /// <summary>
        /// 關閉連線
        /// </summary>
        /// <param name="mqttClient"></param>
        void DisconnectMqttClient(IMqttClient mqttClient);

        /// <summary>
        /// 檢查是否與Server連線
        /// </summary>
        /// <param name="mqttClient"></param>
        /// <returns></returns>
        bool IsConnection(IMqttClient mqttClient);

        /// <summary>
        /// 發布訊息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        void Publish(IMqttClient mqttClient, string message, string topic);

        /// <summary>
        /// 訂閱
        /// </summary>
        /// <param name="topic"></param>
        void Subscribe(IMqttClient mqttClient, string topic);
    }
}
