﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MqttBroker.EFs;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MqttBroker.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231221022259_add user seeds ")]
    partial class adduserseeds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatRoomModels.DB.Messages", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasColumnOrder(0)
                        .HasComment("系統id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date")
                        .HasColumnOrder(1)
                        .HasComment("建立日期");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message")
                        .HasComment("訊息");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("topic")
                        .HasComment("主題");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasComment("Users表的id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("messages", "chat_room");
                });

            modelBuilder.Entity("ChatRoomModels.DB.Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasColumnOrder(0)
                        .HasComment("系統id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date")
                        .HasColumnOrder(1)
                        .HasComment("建立日期");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("pwd")
                        .HasComment("密碼");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_id")
                        .HasComment("使用者帳號");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("users", "chat_room");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDate = new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3829),
                            Pwd = "1234",
                            UserId = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            CreateDate = new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3832),
                            Pwd = "1234",
                            UserId = "user1"
                        },
                        new
                        {
                            Id = 3L,
                            CreateDate = new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3833),
                            Pwd = "1234",
                            UserId = "user2"
                        },
                        new
                        {
                            Id = 4L,
                            CreateDate = new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3834),
                            Pwd = "1234",
                            UserId = "user3"
                        });
                });

            modelBuilder.Entity("ChatRoomModels.DB.Messages", b =>
                {
                    b.HasOne("ChatRoomModels.DB.Users", "Users")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ChatRoomModels.DB.Users", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
