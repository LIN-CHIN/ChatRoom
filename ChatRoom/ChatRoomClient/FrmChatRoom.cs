﻿using ChatRoomClient.Extension;
using ChatRoomClient.MqttService.Interfaces;
using MQTTnet.Client;
using System.Text;

namespace ChatRoom
{
	public partial class FrmChatRoom : Form
	{
		private string _topic = "";
		private IMqttClientService _mqttClientService;
		private IMqttClient _mqttClient;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mqttClientService"></param>
		/// <param name="mqttClient"></param>
		/// <param name="topic">主題</param>
		/// <param name="userName">使用者名稱(ClientId)</param>
		public FrmChatRoom( IMqttClientService mqttClientService,
							IMqttClient mqttClient,
							string topic,
							string userName )
		{
			//init 
			InitializeComponent();
			Text = topic;
			_mqttClientService = mqttClientService;
			_mqttClient = mqttClient;
			_topic = topic;

			JoinTopic( userName );
		}

		/// <summary>
		/// 進入聊天室的動作
		/// </summary>
		private void JoinTopic(string userName)
		{
			//訂閱主題
			_mqttClientService.Subscribe( _mqttClient, _topic );

			//接收訊息
			_mqttClient.UseApplicationMessageReceivedHandler( e =>
			{
				rtbMessage.SendMessage( Encoding.UTF8.GetString( e.ApplicationMessage.Payload ) );
			} );

			rtbMessage.SendMessage( $"進入房間: {_topic}" );
		}

		/// <summary>
		/// 輸入訊息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEnter_Click( object sender, EventArgs e )
		{
			string message = tbInput.Text;
			_mqttClientService.Publish( _mqttClient, message, _topic );

			tbInput.Text = "";
		}
	}
}
