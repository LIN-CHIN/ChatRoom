﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MqttBroker;
using MqttBroker.EFs;
using MqttBroker.Handlers;
using MqttBroker.Handlers.Interfaces;
using MqttBroker.Services;
using MqttBroker.Services.Interfaces;
using NLog;

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
		services.AddDbContext<DataContext>(
			c => c.UseNpgsql( appSettings!.ConnectionString ) );

		services.AddSingleton<IWriteMessageHandler, WriteMessageHandler>()
				.AddSingleton( appSettings! )
				.AddScoped<Application>()
				.AddScoped<IMqttServerService, MqttServerService>();
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