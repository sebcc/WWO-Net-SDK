# WWO-Net-SDK

[![Build Status](https://travis-ci.org/sebcc/WWO-Net-SDK.svg?branch=master)](https://travis-ci.org/sebcc/WWO-Net-SDK)

Wonderware Online .NET SDK
==========================
This SDK helps you create tags and upload data to [Wonderware Online](https://online.wonderware.com) through the upload csv/json API.

Requirements
-----------
- .Net Framework 4.5 and +

Supported
-----------
- .Net Standard 1.1

Features
-----------
- Buffered data transmission
- Json format upload
- Automatic/Manually buffer flush
- Supports multi region
- Limited support of process value types [Supported values](https://online.wonderware.com/Help/#324169.htm)

How it works
-----------
- Create a CSV/JSON datasources from [Wonderware Online Datasource](https://online.wonderware.com/DataSourceManagement) 
- Take the token key and provide it to the WonderwareOnlineClient

Selection of a region
-----------
To select a region, simply put the hostname of the targeted region.

```c#
using WonderwareOnlineSDK;
...
var wonderwareOnlineClient = new WonderwareOnlineClient("online.wonderware.com", "PROVIDE TOKEN HERE");
```

Automatic/manual flush
-----------
By default, the sdk will automatically flush the data every 5 seconds.  There is also a manual flush that is available.

```c#
using WonderwareOnlineSDK;
...
var wonderwareOnlineClient = new WonderwareOnlineClient("online.wonderware.com", "PROVIDE TOKEN HERE");
wonderwareOnlineClient.PurgeAsync();
```

Unsupported
-----------

The primary implications of being UNSUPPORTED are:

1. They include NO WARRANTY OF ANY KIND. My employers assumes NO responsibility for this code or any unintended consequences of using them.
2. By using this, you assume FULL responsibility for the consequences.
3. This repository assumes no responsibility to my employers
