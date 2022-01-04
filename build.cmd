@echo off

rem ------------------------------------------------------------------------------
rem Locate the Visual Studio 2019 MSBuild Tool and Nuget CLI Tool
rem ------------------------------------------------------------------------------

set VS2019PATH=""
set VSWHERE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist %VSWHERE% (
    for /f "delims=" %%i in ('call %VSWHERE% -version "[16.11,17.0)" -property installationPath') do set VS2019PATH=%%i
)

set "INVALID="
if "%VS2019PATH%"=="" set INVALID=1
if %errorlevel%==1 set INVALID=1
if defined INVALID (
    :inputpath
    echo Unable to locate path to Visual Studio 2019 Automatically
    echo Make sure you have Visual Studio 2019 Installed
    echo A good path looks like: "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional"
    set /P VS2019PATH=Please Enter the Path: %=%
)
set MSBUILD2019="%VS2019PATH%\MSBuild\Current\Bin\MSBuild.exe"
if not exist ""%MSBUILD2019%"" (
    echo Invalid Path, MS Not Find %MSBUILD2019%
    set VS2019PATH=""
    goto :inputpath
)
rem echo Found It! %MSBUILD2019%

set NUGET="%ProgramFiles(x86)%\NuGet\nuget.exe"
if not exist ""%NUGET%"" (
    echo Unable to find the Nuget package manager CLI at %NUGET%
    echo If not installed, you can install from: https://www.nuget.org/downloads
    :inputnuget
    set /P NUGET=Please Enter the Location to Nuget: %=%
)
if not exist ""%NUGET%"" (
    echo Invalid Location - Try Again
    goto :inputnuget
)
rem echo Found It! %NUGET%

rem ------------------------------------------------------------------------------
rem Build the Solutions Required for Catalogs
rem ------------------------------------------------------------------------------

echo Building Browser... ("%~dp0Browser\Browser.sln")
pause
%NUGET% restore "%~dp0Browser\Browser.sln"
%MSBUILD2019% "%~dp0Browser\Browser.sln" /p:Configuration=Release /t:Rebuild /v:n

echo Building Online SDK... ("%~dp0OnlineSDK\Mitchell1.Online.Catalog.Host\Mitchell1.Online.Catalog.Host.sln")
pause
%NUGET% restore "%~dp0OnlineSDK\Mitchell1.Online.Catalog.Host\Mitchell1.Online.Catalog.Host.sln"
%MSBUILD2019% "%~dp0OnlineSDK\Mitchell1.Online.Catalog.Host\Mitchell1.Online.Catalog.Host.sln" /p:Configuration=Release /t:Rebuild /v:n

echo Building Example Online Catalog... ("%~dp0OnlineSDK\Samples\C#\ExampleCatalog.sln")
pause
%NUGET% restore "%~dp0OnlineSDK\Samples\C#\ExampleCatalog.sln"
%MSBUILD2019% "%~dp0OnlineSDK\Samples\C#\ExampleCatalog.sln" /p:Configuration=Release /t:Rebuild /v:n

echo Done!