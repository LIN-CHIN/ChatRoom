﻿using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client;

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
        /// <param name="message">要發布的訊息</param>
        /// <param name="topic">主題</param>
        void Publish(IMqttClient mqttClient, string message, string topic);

		/// <summary>
		/// 訂閱
		/// </summary>
		/// <param name="topic">要訂閱的主題</param>
		void Subscribe(IMqttClient mqttClient, string topic);

		/// <summary>
		/// 取消訂閱
		/// </summary>
		/// <param name="topic">要取消訂閱的主題</param>
		void UnSubscribe( IMqttClient mqttClient, string topic );
	}
}
