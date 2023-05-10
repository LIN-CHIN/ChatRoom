using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using System.Windows.Forms;
using MQTTnet.Client.Connecting;
using MQTTnet.Adapter;
using System.Net.Sockets;
using MQTTnet.Diagnostics.Logger;
using Microsoft.Extensions.Logging;
using ChatRoomClient.Handlers.Interfaces;
using ChatRoomClient.Extension;
using ChatRoomClient.Enums;
using ChatRoomClient.MqttService.Interfaces;

namespace ChatRoom
{
	public partial class FrmStart : Form
	{
		private readonly ILogHandler _logHandler;

		IMqttClient _mqttClient;
		IMqttClientService _mqttClientService;

		public FrmStart( IMqttClientService mqttClientService,
						 ILogHandler logHandler )
		{
			InitializeComponent();
			_mqttClientService = mqttClientService;
			_logHandler = logHandler;
		}

		/// <summary>
		/// 開啟連線
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btnConnection_Click( object sender, EventArgs e )
		{
			if( string.IsNullOrWhiteSpace( tbUserName.Text ) ) {
				rtbMessage.SendMessage( "請輸入使用者帳號" );
				return;
			}

			if( string.IsNullOrWhiteSpace( tbPwd.Text ) ) {
				rtbMessage.SendMessage( "請輸入密碼" );
				return;
			}

			if( _mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( $"使用者:{tbUserName.Text} 連線中!" );
				return;
			}

			//TCP Connection
			if( rdoTCP.Checked ) {
				await Connection();
			}
			else {
				//TODO: By WS Connection
			}
		}

		/// <summary>
		/// 關閉連線
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDisconnection_Click( object sender, EventArgs e )
		{
			if( !_mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( $"使用者 '{tbUserName.Text}' 並不是連線狀態" );
				return;
			}

			_mqttClientService.DisconnectMqttClient( _mqttClient );
		}

		/// <summary>
		/// 訂閱主題一
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTopic1_Click( object sender, EventArgs e )
		{
			OpenChatRoomForm( "主題一" );
		}

		/// <summary>
		/// 訂閱主題二
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTopic2_Click( object sender, EventArgs e )
		{
			OpenChatRoomForm( "主題二" );
		}

		private void OnConnectedHandler( MqttClientConnectedEventArgs args )
		{
			rtbMessage.SendMessage( $"{tbUserName.Text} 連線成功！" );
		}

		private void OnDisconnectedHandler( MqttClientDisconnectedEventArgs args )
		{
			if( args.Exception == null && args.ClientWasConnected ) {
				rtbMessage.SendMessage( $"{tbUserName.Text} 已離線" );
			}
		}

		/// <summary>
		/// Client連線
		/// </summary>
		/// <returns></returns>
		private async Task Connection()
		{
			try {
				_mqttClient = await _mqttClientService.CreateMqttClient(
				tbUserName.Text,
				tbPwd.Text,
				OnConnectedHandler,
				OnDisconnectedHandler );

				_logHandler.WriteInfo( "連線成功" );
			}
			catch( MqttConnectingFailedException ex ) {
				rtbMessage.SendMessageWithLog( "使用者帳號或密碼錯誤", LogLevelEnum.Info );

			}
			catch( Exception ex ) {
				if( ex.InnerException != null ) {
					if( ( (SocketException)ex.InnerException ).ErrorCode == 10061 ) {
						rtbMessage.SendMessageWithLog( $"連線不到Server，請洽管理員。", LogLevelEnum.Error );
						_logHandler.WriteError( ex.ToString() );
						return;
					}
				}

				rtbMessage.SendMessage( "連線時發生異常，請洽管理員" );
				_logHandler.WriteError( $"連線時發生異常。 Exception : {ex}" );
			}
		}

		/// <summary>
		/// 開啟聊天室視窗
		/// </summary>
		/// <param name="topic">主題</param>
		private void OpenChatRoomForm(string topic)
		{
			if( !_mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( "請先進行連線" );
				return;
			}

			FrmChatRoom frmChatRoom = new FrmChatRoom( _mqttClientService, _mqttClient, topic, tbUserName.Text );
			frmChatRoom.ShowDialog();
		}
	}
}