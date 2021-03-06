# Sitecore.Support.130247

The `DataProviderHashKeys` class inefficiently queries the `Reporting` database that leads to timeouts and SQL Server overloads. 

[![Total downloads](https://img.shields.io/github/downloads/SitecoreSupport/Sitecore.Support.130247/total.svg)](https://github.com/SitecoreSupport/Sitecore.Support.130247/releases)

## Main

This repository contains Sitecore Patch #130247, which overrides SQL commands to query database in more efficient way.

``` sql
SELECT * 
  FROM [DimensionKeys]
 WHERE [DimensionKeyId] = @hashValue
```

## License

This patch is licensed under the [Sitecore Corporation A/S License](./LICENSE).

## Download

Downloads are available via [GitHub Releases](https://github.com/SitecoreSupport/Sitecore.Support.130247/releases).
