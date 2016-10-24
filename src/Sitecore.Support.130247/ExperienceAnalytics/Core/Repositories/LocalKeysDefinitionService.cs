using Sitecore.ExperienceAnalytics.Core.Repositories.Model;
using System;
using System.Data;

namespace Sitecore.Support.ExperienceAnalytics.Core.Repositories
{
  /// <summary>
  /// Copied-pasted from the origin class due to the 'internal' access modifier. 
  /// </summary>
  class LocalKeysDefinitionService : LocalDefinitionServiceBase<DimensionKey>
  {
    protected override string SqlExpression { get { return "SELECT * FROM [DimensionKeys]"; } }

    public LocalKeysDefinitionService([NotNull] string connectionStringName)
      : base(connectionStringName)
    {
    }

    public LocalKeysDefinitionService()
    {
    }

    protected override DimensionKey CreateEntityFromReader(IDataRecord reader)
    {
      object dimensionKeyId = reader["DimensionKeyId"];
      var dimensionKey = (string)reader["DimensionKey"];

      if ((dimensionKeyId == DBNull.Value) ||
          ((long)dimensionKeyId == 0) ||
          string.IsNullOrEmpty(dimensionKey))
      {
        return null;
      }

      return new DimensionKey
      {
        Hash = dimensionKeyId.ToString(),
        KeyValue = dimensionKey
      };
    }
  }
}