namespace MicroApi.Handlers
{
    public class Handlers
    {
        public Handlers(ForecastHandler forecastHandler, UserHandler userHandler, DownloadHandler downloadHandler)
        {
            this.Download = downloadHandler;
            this.Forecast = forecastHandler;
            this.User = userHandler;
        }
        
        public DownloadHandler Download { get; }
        public ForecastHandler Forecast { get; }
        public UserHandler User { get; }
    }
}