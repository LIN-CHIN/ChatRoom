﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRoomModels.DB
{
    [Table("messages", Schema = "chat_room")]
    public class Messages : ChatRoomObject
    {
        /// <summary>
        /// 訊息
        /// </summary>
        [Required]
        [Column("message", TypeName = "text")]
        [Comment("訊息")]
        public string Message { get; set; }

        /// <summary>
        /// 主題
        /// </summary>
        [Required]
        [Column("topic", TypeName = "varchar(255)")]
        [Comment("主題")]
        public string Topic { get; set; }

        /// <summary>
        /// 使用者id
        /// </summary>
        [ForeignKey("Users")]
        [Required]
        [Column("user_id")]
        [Comment("Users表的id")]
        public long UserId { get; set; }

        /// <summary>
        /// 關聯Users
        /// </summary>
        public Users Users { get; set; }

    }
}
