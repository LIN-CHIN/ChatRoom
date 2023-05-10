using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MqttBroker;
using MqttBroker.DAOs.MessagesDAO;
using MqttBroker.DAOs.UserDAO;
using MqttBroker.EFs;
using MqttBroker.Handlers;
using MqttBroker.Handlers.Interfaces;
using MqttBroker.Services;
using MqttBroker.Services.Interfaces;
using NLog;
using NLog.Extensions.Logging;

var logger = LogManager.GetCurrentClassLogger();

try {
	//註冊appsettings.json
	var config = new ConfigurationBuilder()
					.AddJsonFile( "./appsettings.json" )
					.Build();

	var appSettings = config.GetSection( "AppSettings" ).Get<AppSettings>( c => c.BindNonPublicProperties = true );

	//Host設定
	var host = Host.CreateDefaultBuilder( args )
	.ConfigureServices( ( hostContext, services ) =>
	{
		//注入DataContext，並且將Entity Framework產生的 __EFMigrationsHistory 存放位置改為指定Schema
		services.AddDbContext<DataContext>(
			c => c.UseNpgsql( appSettings!.ConnectionString,
							  x => x.MigrationsHistoryTable( HistoryRepository.DefaultTableName, "chat_room" ) ) );

		//設定nlog 
		services.AddLogging( x =>
		{
			x.ClearProviders();
			x.SetMinimumLevel( Microsoft.Extensions.Logging.LogLevel.Trace );
			x.AddNLog( "./NLog.xml" );
		} );

		services.AddSingleton<IWriteMessageHandler, WriteMessageHandler>()
				.AddSingleton( appSettings! )
				.AddScoped<Application>()
				.AddScoped<IMqttServerService, MqttServerService>()
				.AddScoped<IUserDAO, UserDAO>()
				.AddScoped<IMessageDAO, MessageDAO>()
				.AddScoped<IMqttServerEvent, MqttServerEvent>();
	} )
	.Build();

	//Entity Framework migrate 更新資料庫
	using( var scope = host.Services.CreateScope() ) {
		var services = scope.ServiceProvider;

		var context = services.GetRequiredService<DataContext>();
		context.Database.Migrate();
	}

	//程式進入點
	await host.Services.GetService<Application>()!.Start();

}
catch( Exception ex ) {
	logger.Error( ex, "Stopped program because of exception" );
	throw ex;
}
finally {
	//需確定在關閉時，把nlog關閉
	LogManager.Shutdown();
}