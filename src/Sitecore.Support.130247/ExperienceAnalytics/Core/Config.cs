using Sitecore.Configuration;
using System;

namespace Sitecore.Support.ExperienceAnalytics.Core
{
  /// <summary>
  /// Copied-pasted from the origin class due to the 'internal' access modifier. 
  /// </summary>
  public static class Config
  {
    private static TimeSpan? cacheExpiration;

    public const string ExperienceAnalyticsSection = "experienceAnalytics";

    internal static TimeSpan InternalCacheExpirationDefault { get; private set; }

    static Config()
    {
      InternalCacheExpirationDefault = TimeSpan.FromMinutes(5);
    }

    internal static TimeSpan InternalCacheExpiration
    {
      get
      {
        if (!cacheExpiration.HasValue)
        {
          cacheExpiration = Settings.GetTimeSpanSetting("ExperienceAnalytics.InternalCacheExpiration", InternalCacheExpirationDefault);
        }

        return cacheExpiration.Value;
      }

      set { cacheExpiration = value; }
    }
  }
}