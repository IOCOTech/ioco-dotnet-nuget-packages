# Introduction 
This package is for Cloud Queuing Message Service. The implementation is mainly for any code related to queue message management on the cloud platform e.g. Azure, AWS, etc

# Getting Started
To use this package:
1. Ensure you have setup your local machine IDE e.g. Visual Studio to connect to the iOCO Github Packages: https://github.com/IOCOTech/ioco-dotnet-nuget-packages
2. Go to nuget package manager on your IDE
3. Ensure that the NuGet package source is either pointing to all or the iOCO Github Packages configured at step 1
4. Search for Ioco.Infrastructure.Queue and Install in your solution

Please note that the solution consuming this package will have to setup the below. 

```
"QueueSettings" : {
	"ConnectionString": ""
	"QueueName": "",
}
```

Happy coding :)
