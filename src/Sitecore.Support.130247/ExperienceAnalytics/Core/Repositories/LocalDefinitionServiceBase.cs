using Sitecore.Data.DataProviders.Sql;
using Sitecore.Data.SqlServer;
using Sitecore.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Sitecore.Support.ExperienceAnalytics.Core.Repositories
{
  /// <summary>
  /// Copied-pasted from the origin class due to the 'internal' access modifier. 
  /// </summary>
  abstract class LocalDefinitionServiceBase<T>
   where T : class
  {
    protected SqlServerDataApi SqlServerApi { get; set; }
    protected abstract string SqlExpression { get; }

    protected LocalDefinitionServiceBase([NotNull] string connectionStringName)
    {
      Assert.ArgumentNotNullOrEmpty(connectionStringName, "connectionStringName");

      ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

      if (connectionString != null && !string.IsNullOrEmpty(connectionString.ConnectionString))
      {
        this.SqlServerApi = new SqlServerDataApi(connectionString.ConnectionString);
      }
    }

    protected LocalDefinitionServiceBase()
    {
    }

    public List<T> GetEntities()
    {
      using (DataProviderReader reader = this.SqlServerApi.CreateReader(this.SqlExpression, new object[0]))
      {
        return this.GetEntities(reader.InnerReader);
      }
    }

    public List<T> GetEntities(IDataReader reader)
    {
      var entities = new List<T>();

      while (reader.Read())
      {
        T entity = this.CreateEntityFromReader(reader);
        if (entity != null)
        {
          entities.Add(entity);
        }
      }

      return entities;
    }

    protected abstract T CreateEntityFromReader(IDataRecord reader);
  }
}