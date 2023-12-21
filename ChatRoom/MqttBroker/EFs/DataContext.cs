using ChatRoomModels.DB;
using Microsoft.EntityFrameworkCore;

namespace MqttBroker.EFs
{
    public class DataContext : DbContext
	{
		public DataContext( DbContextOptions<DataContext> options ) : base( options )
		{
		}

		public DbSet<Users> Users => Set<Users>();
		public DbSet<Messages> Messages => Set<Messages>();

		protected override void OnModelCreating( ModelBuilder modelBuilder )
		{
			modelBuilder.Entity<Users>()
				.HasIndex( u => new { u.UserId } )
				.IsUnique();

			modelBuilder.Entity<Messages>()
				.HasOne( m => m.Users )
				.WithMany( m => m.Messages )
				.IsRequired()
				.OnDelete( DeleteBehavior.Restrict );

			modelBuilder.Entity<Users>().HasData( GetDefaultUsers() );
		}

		/// <summary>
		/// 取得預設的使用者清單
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Users> GetDefaultUsers()
		{
			IList<Users> users = new List<Users>();

			users.Add( new Users
			{
				Id = 1,
				UserId = "admin",
				Pwd = "1234"
			} );

            users.Add(new Users
            {
                Id = 2,
                UserId = "user1",
                Pwd = "1234"
            });

            users.Add(new Users
            {
                Id = 3,
                UserId = "user2",
                Pwd = "1234"
            });

            users.Add(new Users
            {
                Id = 4,
                UserId = "user3",
                Pwd = "1234"
            });

            return users;
		}
	}
}
