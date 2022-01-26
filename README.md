# catalogsdk
Mitchell1 Catalog SDK

This repo is meant for third party catalog integrators to be able to test out new changes from Mitchell1 related to catalogs.
## 


## Getting Started - Using Online Release Zip File (**Preferred**)

**Requirements**
* Microsoft Windows 8+ - 32/64 Bit (64 bit preferred)
* .Net 4.7.2 (4.8 preferred)

1. Download & Extract the latest release - **Mitchell1OnlineCatalogSDK.zip**
2. Read the documentation provided for using the Online Catalog Driver Executable. This driver hosts catalog development, as well as providing a Browser test bed of our Embedded Browser Control.

## Building from Source (**Advanced Users Only**)

**Requirements**
* Microsoft Windows 8+ - 32/64 Bit (64 bit preferred)
* .Net 4.7.2 (4.8 preferred)
* Visual Studio 2019

1. **Before** attempting to build from source, please ensure you have done the preferred getting started method above (using only zip files/binary release)
2. Clone this repository
3. Download the latest Release file named: **Mitchell1OnlineCatalogSDK.zip** - **Do not** extract all files
4. **Extract Only** Mitchell1OnlineCatalogSDK\\bin folder from the release zip to **[cloned repo]**\\ root folder. You see the following structure in your cloned git folder after extracting:
   1. **[repo]**\\bin\\
   2. **[repo]**\\bin\\Browser\\
   3. **[repo]**\\bin\\Data\\
   4. And several dll's/exe's in these folders
5. Open & Build All Browser\\Browser.sln (Embedded Browser wrapper/communication layer)
6. Open & Build All OnlineSDK\\Mitchell1.Online.Catalog.Host\\Mitchell1.Online.Catalog.Host.sln (ManagerSE Catalog Adapter Layer)
7. Open as **admin** (needed for debugging/running) & Build All OnlineSDK\\Samples\\C#\\ExampleCatalog.sln (Sample C# Self-Hosting WebService/App Catalog)