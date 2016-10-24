using Sitecore.Analytics.Reporting;
using Sitecore.ExperienceAnalytics.Core;
using Sitecore.ExperienceAnalytics.Core.Repositories.Contracts;
using Sitecore.ExperienceAnalytics.Core.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sitecore.Support.ExperienceAnalytics.Core.Repositories
{
  class DataProviderHashKeys : IKeysRepository
  {
    private static string dataSourceName;
    private readonly Lazy<ReportDataProviderBase> reportingDataProvider;

    [Obsolete("Obsoleted in Experience Analytics 1.2.0")]
    public DataProviderHashKeys()
      : this(CoreContainer.Configuration.GetLazyReportingDataProvider(), "reporting")
    { }

    public DataProviderHashKeys(Lazy<ReportDataProviderBase> reportingDataProvider, string reportingDataSourceName)
    {
      dataSourceName = reportingDataSourceName;
      this.reportingDataProvider = reportingDataProvider;
    }

    public string GetHashByKeyValue(string key)
    {
      var sqlCommand = $"SELECT * FROM [DimensionKeys] WHERE [DimensionKey] = '{key}'";

      var hashByKey = GetEntitiesFromDb(sqlCommand)
        .Where(x => x.KeyValue == key)
        .Select(x => x.Hash).SingleOrDefault();

      if (!string.IsNullOrEmpty(hashByKey))
      {
        return hashByKey;
      }

      return GetEntitiesFromDb(sqlCommand, true)
        .Where(x => x.KeyValue == key)
        .Select(x => x.Hash)
        .Single();
    }

    public string GetKeyValueByHash(string hashValue)
    {
      var sqlCommand = $"SELECT * FROM [DimensionKeys] WHERE [DimensionKeyId] = {hashValue}";

      var keyByHash = GetEntitiesFromDb(sqlCommand)
        .Where(x => x.Hash == hashValue)
        .Select(x => x.KeyValue)
        .SingleOrDefault();

      if (!string.IsNullOrEmpty(keyByHash))
      {
        return keyByHash;
      }

      return GetEntitiesFromDb(sqlCommand, true)
        .Where(x => x.Hash == hashValue)
        .Select(x => x.KeyValue)
        .SingleOrDefault();
    }

    private IEnumerable<DimensionKey> GetEntitiesFromDb(string sqlCommand, bool skipCache = false)
    {
      var cachingPolicy = new CachingPolicy
      {
        NoCache = skipCache,
        ExpirationPeriod = Config.InternalCacheExpiration
      };

      var query = new ReportDataQuery(sqlCommand);

      DataTable dataTable = reportingDataProvider.Value
        .GetData(dataSourceName, query, cachingPolicy)
        .GetDataTable();

      DataTableReader reader = dataTable.CreateDataReader();
      var keyReader = new LocalKeysDefinitionService();

      return keyReader.GetEntities(reader);
    }
  }
}