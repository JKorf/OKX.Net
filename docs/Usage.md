---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the OKX API, the `OKXRestClient` and `OKXSocketClient`. They can be created manually on the fly or be added to the dotnet DI using the `AddOKX` extension method.

*Create a new rest client*
```csharp
var okxRestClient = new OKXRestClient(options =>
{
    // Set options here for this client
});

var okxSocketClient = new OKXSocketClient(options =>
{
    // Set options here for this client
});
```

*Using dotnet dependency inject*
```csharp
services.AddOKX(
    restOptions => {
        // set options for the rest client
    },
    socketClientOptions => {
        // set options for the socket client
    }); 
    
// IOKXRestClient, IOKXSocketClient and IOKXOrderBookFactory are now available for injecting
```

Different options are available to set on the clients, see this example
```csharp
var okxRestClient = new OKXRestClient(options =>
{
    options.ApiCredentials = new OKXApiCredentials("API-KEY", "API-SECRET", "API-PASSPHRASE");
    options.RequestTimeout = TimeSpan.FromSeconds(60);
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions` or during the registration in the DI container:  
```csharp
OKXRestClient.SetDefaultOptions(options => {
    // Set options here for all new clients
});
var okxRestClient = new OKXRestClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

