@echo off

dotnet publish -c Release -p:PublishSingleFile=true --self-contained false
copy bin\Release\net8.0-windows\win-x64\publish\AddRemoveProgramsCleaner.exe c:\Programs\Accessories\AddRemoveProgramsCleaner.exe