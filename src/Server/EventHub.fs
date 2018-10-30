module Todo.Server.EventHub

open Microsoft.AspNetCore.SignalR
//open System.Threading.Tasks
open Thoth.Json.Net

open Todo.Domain
open Todo.Domain.Types


let mutable eventLog: Event list = []

type EventHub() =
    inherit Hub()


    member this.Foobar(task: Task): System.Threading.Tasks.Task =
        printfn "%A" task
        System.Threading.Tasks.Task.CompletedTask

    member this.Execute(json: string): System.Threading.Tasks.Task =
        match Decode.Auto.fromString<Command> json with
        | Ok cmd -> this.ExecuteCommand cmd
        | Result.Error e -> printfn "%s" e; System.Threading.Tasks.Task.CompletedTask

    member private this.ExecuteCommand(command: Command): System.Threading.Tasks.Task =
        let events = Behaviour.execute eventLog command

        eventLog <- List.append eventLog events

        let json = Encode.Auto.toString(0, events)
        this.Clients.All.SendAsync("EventsHappened", json)

    member __.GetHistory(): System.Threading.Tasks.Task<string> =
        let json = Encode.Auto.toString(0, eventLog)
        System.Threading.Tasks.Task.FromResult json
