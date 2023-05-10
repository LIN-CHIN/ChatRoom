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
		/// �}�ҳs�u
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btnConnection_Click( object sender, EventArgs e )
		{
			if( string.IsNullOrWhiteSpace( tbUserName.Text ) ) {
				rtbMessage.SendMessage( "�п�J�ϥΪ̱b��" );
				return;
			}

			if( string.IsNullOrWhiteSpace( tbPwd.Text ) ) {
				rtbMessage.SendMessage( "�п�J�K�X" );
				return;
			}

			if( _mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( $"�ϥΪ�:{tbUserName.Text} �s�u��!" );
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
		/// �����s�u
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDisconnection_Click( object sender, EventArgs e )
		{
			if( !_mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( $"�ϥΪ� '{tbUserName.Text}' �ä��O�s�u���A" );
				return;
			}

			_mqttClientService.DisconnectMqttClient( _mqttClient );
		}

		/// <summary>
		/// �q�\�D�D�@
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTopic1_Click( object sender, EventArgs e )
		{
			OpenChatRoomForm( "�D�D�@" );
		}

		/// <summary>
		/// �q�\�D�D�G
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTopic2_Click( object sender, EventArgs e )
		{
			OpenChatRoomForm( "�D�D�G" );
		}

		private void OnConnectedHandler( MqttClientConnectedEventArgs args )
		{
			rtbMessage.SendMessage( $"{tbUserName.Text} �s�u���\�I" );
		}

		private void OnDisconnectedHandler( MqttClientDisconnectedEventArgs args )
		{
			if( args.Exception == null && args.ClientWasConnected ) {
				rtbMessage.SendMessage( $"{tbUserName.Text} �w���u" );
			}
		}

		/// <summary>
		/// Client�s�u
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

				_logHandler.WriteInfo( "�s�u���\" );
			}
			catch( MqttConnectingFailedException ex ) {
				rtbMessage.SendMessageWithLog( "�ϥΪ̱b���αK�X���~", LogLevelEnum.Info );

			}
			catch( Exception ex ) {
				if( ex.InnerException != null ) {
					if( ( (SocketException)ex.InnerException ).ErrorCode == 10061 ) {
						rtbMessage.SendMessageWithLog( $"�s�u����Server�A�Ь��޲z���C", LogLevelEnum.Error );
						_logHandler.WriteError( ex.ToString() );
						return;
					}
				}

				rtbMessage.SendMessage( "�s�u�ɵo�Ͳ��`�A�Ь��޲z��" );
				_logHandler.WriteError( $"�s�u�ɵo�Ͳ��`�C Exception : {ex}" );
			}
		}

		/// <summary>
		/// �}�Ҳ�ѫǵ���
		/// </summary>
		/// <param name="topic">�D�D</param>
		private void OpenChatRoomForm(string topic)
		{
			if( !_mqttClientService.IsConnection( _mqttClient ) ) {
				rtbMessage.SendMessage( "�Х��i��s�u" );
				return;
			}

			FrmChatRoom frmChatRoom = new FrmChatRoom( _mqttClientService, _mqttClient, topic, tbUserName.Text );
			frmChatRoom.ShowDialog();
		}
	}
}