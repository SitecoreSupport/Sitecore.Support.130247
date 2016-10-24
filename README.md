# Sitecore.Support.130247

The `DataProviderHashKeys` class inefficiently queries the `Reporting` database that leads to timeouts and SQL Server overloads. 

## Main

This repository contains Sitecore Patch #130247, which overrides SQL commands to query database in more efficient way.

## Deployment

To apply the patch, perform the following steps on CM servers:

1. Place the `Sitecore.Support.130247.dll` assembly into the `\bin` directory.
2. Place the `Sitecore.Support.130247.config` file into the `\App_Config\Include\zzz` directory.

## Content 

Sitecore Patch includes the following files:

1. `\bin\Sitecore.Support.130247.dll`
2. `\App_Config\Include\zzz\Sitecore.Support.130247.config`

## License

This patch is licensed under the [Sitecore Corporation A/S License](./LICENSE).

## Download

Downloads are available via [GitHub Releases](https://github.com/SitecoreSupport/Sitecore.Support.130247/releases).
