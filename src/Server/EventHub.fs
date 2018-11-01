namespace Todo.Server

open Microsoft.AspNetCore.SignalR
open Todo.Domain.Types
open System.Threading.Tasks

type EventHub() =
    inherit Hub()

    let mutable eventLog: Event list = []

    member this.EventsHappened(events: Event list): Task =
        eventLog <- List.append eventLog events

        this.Clients
            .AllExcept(this.Context.ConnectionId)
            .SendAsync("EventsHappened", events)


    member __.GetHistory(): Task<Event list> =
        Task.FromResult eventLog
