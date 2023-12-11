namespace MqttBroker
{
	/// <summary>
	/// appsettings.json的model
	/// </summary>
	public class AppSettings
	{
		/// <summary>
		/// DB連線字串
		/// </summary>
		public string ConnectionString
        {
			get; private set;
		}
	}
}
