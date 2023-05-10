using ChatRoomClient.Enums;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomClient.Extension
{
	/// <summary>
	/// RichTextBox 擴充
	/// </summary>
	public static class RichTextBoxExtension
	{
		/// <summary>
		/// 新增RichTextBox訊息
		/// </summary>
		/// <param name="richTextBox"><要新增訊息的RichTextBox/param>
		/// <param name="message">訊息</param>
		public static void SendMessage( this RichTextBox richTextBox, string message )
		{
			WriteToRichTextBox( richTextBox, message );
		}

		/// <summary>
		/// 新增RichTextBox訊息，且紀錄log
		/// </summary>
		/// <param name="richTextBox"><要新增訊息的RichTextBox/param>
		/// <param name="message">訊息</param>
		/// <param name="logLevelEnum"> Log嚴重等級 </param>
		public static void SendMessageWithLog( this RichTextBox richTextBox, string message, LogLevelEnum logLevelEnum )
		{
			ILogger logger = LogManager.GetCurrentClassLogger();

			WriteToRichTextBox( richTextBox, message );

			if( logLevelEnum == LogLevelEnum.Info ) {
				logger.Info( message );
			}
			else if( logLevelEnum == LogLevelEnum.Error ) {
				logger.Error( message );
			}
		}

		/// <summary>
		/// 寫訊息到RichText
		/// </summary>
		/// <param name="richTextBox"></param>
		/// <param name="message"></param>
		private static void WriteToRichTextBox( RichTextBox richTextBox, string message )
		{
			if( richTextBox.InvokeRequired ) {

				richTextBox.Invoke( new Action( () => richTextBox.AppendText( message + "\r\n" ) ) );
			}
			else {
				richTextBox.AppendText( message );
				richTextBox.AppendText( "\r\n" );
			}
		}
	}
}
