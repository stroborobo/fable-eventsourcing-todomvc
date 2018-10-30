module Todo.Client.Subscription

open Elmish
open Fable.Core
open Fable.Import.SignalR
open Thoth.Json

open Todo.Domain.Types

open Model

// SignalR
let connection = signalR.HubConnectionBuilder.Create().withUrl("/hub").build()

let eventsJsonToDispatch dispatch json =
    match Decode.Auto.fromString<Event list> json with
    | Result.Error e -> printfn "%s" e; ()
    | Ok events -> events |> List.iter (DomainEvent >> dispatch)

let connectionCmd createPromise toConnectionState =
    Cmd.ofAsync
        (createPromise >> Async.AwaitPromise)
        ()
        (fun _ ->
            toConnectionState connection.state
            |> ConnectionChanged
            |> ConnectionMsg)
        (string >> ConnectionError >> ConnectionChanged >> ConnectionMsg)

let startConnection() =
    connectionCmd
        connection.start
        (function
        | HubConnection.HubConnectionState.Disconnected ->
            ConnectionError "reconnect succeeded, but still disconnected"
        | HubConnection.HubConnectionState.Connected
        | _-> Connected)

let stopConnection() =
    connectionCmd
        connection.stop
        (function
        | HubConnection.HubConnectionState.Connected ->
            ConnectionError "Disconnect failed"
        | HubConnection.HubConnectionState.Disconnected
        | _-> NotConnected)

let getHistory() =
    Cmd.ofAsync
        (fun () ->
            connection.invoke<string>("GetHistory") |> Async.AwaitPromise)
        ()
        (fun json ->
            match Decode.Auto.fromString<Event list> json with
            | Result.Error e -> printfn "%s" e; NoOp // error in ui missing now
            | Ok events -> HistoryLoaded events)
        (fun _ -> NoOp)


let sendCommand (command: Command) =
    Cmd.ofAsync
        (fun () ->
            let json = Encode.Auto.toString(0, command)
            connection.send("Execute", Some (json :> obj)) |> Async.AwaitPromise)
        ()
        (fun _ -> NoOp)
        (fun _ -> NoOp)

let subscriptions _ =
    let sub dispatch =
        connection.onclose
            (string >> ConnectionError >> ConnectionChanged >> ConnectionMsg >> dispatch)
        connection.on<string> ("EventsHappened", eventsJsonToDispatch dispatch)
    Cmd.ofSub sub
