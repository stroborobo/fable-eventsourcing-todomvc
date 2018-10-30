module Todo.Server.App

open System
open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore
open Microsoft.AspNetCore.SignalR
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Newtonsoft.Json.Serialization

open FSharp.Control.Tasks.V2

open Todo.Server.EventHub

let publicPath = Path.GetFullPath "../Client/dist"
let port = 8085us

let configureApp (app : IApplicationBuilder) =
    app.UseDefaultFiles()
       .UseStaticFiles()
       .UseSignalR(fun routes -> routes.MapHub<EventHub>(PathString("/hub")))
    |> ignore

let configureServices (services : IServiceCollection) =
    services
        .AddSignalR()
        .AddJsonProtocol(fun options ->
            options.PayloadSerializerSettings.ContractResolver <- new DefaultContractResolver())
    |> ignore

WebHost
    .CreateDefaultBuilder()
    .UseWebRoot(publicPath)
    .UseContentRoot(publicPath)
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .UseUrls("http://0.0.0.0:" + port.ToString() + "/")
    .Build()
    .Run()
