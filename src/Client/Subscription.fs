module Todo.Client.Subscription

open Elmish
open Fable.Core
open Fable.Import.SignalR

open Todo.Domain.Types

open Model

// SignalR
let connection = signalR.HubConnectionBuilder.Create().withUrl("/hub").build()

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

let sendEvents (events: Event list) =
    let events =
        events
        |> List.map (Some)
        |> Array.ofList
    Cmd.ofAsync
        (connection.send("EventsHappened", events) >> Async.AwaitPromise)
        ()
        (fun _ ->
            toConnectionState connection.state
            |> ConnectionChanged
            |> ConnectionMsg)
        (string >> ConnectionError >> ConnectionChanged >> ConnectionMsg)

let subscriptions _ =
    let sub dispatch =
        connection.onclose
            (string >> ConnectionError >> ConnectionChanged >> ConnectionMsg >> dispatch)
        connection.on<Event list> ("EventsHappened", List.iter (DomainEvent >> dispatch))
    Cmd.ofSub sub
