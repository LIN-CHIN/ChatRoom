using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatRoomModels
{
	/// <summary>
	/// 基底物件，所有Model都要繼承此物件
	/// </summary>
	public abstract class ChatRoomObject
	{
		[Key]
		[DatabaseGenerated( DatabaseGeneratedOption.Identity )]
		[Column( "id", Order = 0 )]
		[Comment( "系統id" )]
		public long Id
		{
			get; set;
		}

		/// <summary>
		/// 建立時間
		/// </summary>
		[Required]
		[Column( "create_date", Order = 1 )]
		[Comment( "建立日期" )]
		public DateTime CreateDate { get; set; } = DateTime.UtcNow;
	}
}