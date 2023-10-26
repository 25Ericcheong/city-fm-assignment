using CityFm.Models.Config.Api;
using CityFm.Models.Config.EndPoint;
using CityFm.Models.Config.Logging;

namespace CityFm.Models.Config;

public class AppSettings
{
    public LoggingSettings Logging { get; set; }

    public Urls Urls { get; set; }

    public AllTheClouds AllTheClouds { get; set; }
}