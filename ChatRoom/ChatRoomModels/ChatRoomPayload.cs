using ChatRoomModels.DB;
using System.ComponentModel.DataAnnotations;

namespace ChatRoomModels
{
	public class ChatRoomPayload
	{
		/// <summary>
		/// 客戶端Id (也就是使用者帳號)
		/// </summary>
		[Required]
		public string ClientId { get; set; }

		/// <summary>
		/// 主題
		/// </summary>
		[Required]
		public string Topic { get; set; }

		/// <summary>
		/// 訊息
		/// </summary>
		[Required]
		public string Message { get; set; }

		/// <summary>
		/// 轉成Messages Entity
		/// </summary>
		/// <param name="id">使用者id (流水號)</param>
		/// <returns></returns>
		public Messages ToMessagesEntity(long id)
		{
			return new Messages
			{
				Message = Message,
				Topic = Topic,
				UserId = id
			};
		}

		/// <summary>
		/// 轉成聊天室字串
		/// </summary>
		/// <returns></returns>
		public string ToChatString()
		{
			return $"{ClientId}: {Message}";
		}
		
	}
}
