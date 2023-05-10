using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatRoomModels.DB
{
    [Table("users", Schema = "chat_room")]
    public class Users : ChatRoomObject
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        [Required]
        [Column("user_id", TypeName = "varchar(50)")]
        [Comment("使用者帳號")]
        public string UserId { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [Required]
        [Column("pwd", TypeName = "varchar(50)")]
        [Comment("密碼")]
        public string Pwd { get; set; }

        /// <summary>
        /// 關聯Messages
        /// </summary>
        public ICollection<Messages> Messages { get; set; }
    }
}
