# Sharpmon (based on a school project)
A .Net Core 2.0 console version of a minimalist pokemon-like game.


## To Compile:
Clone all the repo into a folder and run the following commands:
```DIGITAL Command Language
dotnet restore
dotnet build
```
Then, depending on your platform, you can run one of those commands to create an executable:
```DIGITAL Command Language
dotnet publish -c Release -r win7-x64
dotnet publish -c Release -r win10-x64
dotnet publish -c Release -r osx.10.12-x64
dotnet publish -c Release -r linux-x64
```
