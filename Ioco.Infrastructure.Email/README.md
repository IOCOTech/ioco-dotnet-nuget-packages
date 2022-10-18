# Introduction 
This package is for Email Service. The implementation is mainly for any code related to emails.

# Getting Started
To use this package:
1. Ensure you have setup your local machine IDE e.g. Visual Studio to connect to the iOCO GitHub Packages: https://github.com/IOCOTech/ioco-dotnet-nuget-packages
2. Go to nuget package manager on your IDE
3. Ensure that the NuGet package source is either pointing to all or the iOCO GitHub Packages configured at step 1
4. Search for Simplify.Infrastructure.Email and Install in your solution

Please note that the solution consuming this package will have to setup the below. 

```
"EmailSettings" : {
	"Host": "",
	"Port": "",
	"Username": "",
	"Password": ""
}
```

Happy coding :)
