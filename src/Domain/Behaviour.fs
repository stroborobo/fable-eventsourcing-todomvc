module Todo.Domain.Behaviour

open Types
open Projections

let addTask history title =
    [ TaskAdded title ]

let deleteTask history id =
    history
    |> todoListState
    |> List.tryFind (fun i -> i.Id = id)
    |> function
    | None -> [ Error NoTaskFound ]
    | Some _ -> [ TaskDeleted id ]

let executeTask item command =
    match command with
    | ChangeTitle title ->
        [ TaskChanged (item.Id, TitleChanged title) ]
    | SetDone ->
        if item.Done then [ Error AlreadyDone]
        else [ TaskChanged (item.Id, Done) ]
    | SetUndone ->
        if item.Done then [ Error NotDone]
        else [ TaskChanged (item.Id, Undone) ]

let changeTask history id itemCommand =
    history
    |> todoListState
    |> List.tryFind (fun i -> i.Id = id)
    |> function
    | Some item -> executeTask item itemCommand
    | None -> [ Error NoTaskFound ]

let execute history command =
    match command with
    | AddTask title ->
        addTask history title
    | DeleteTask id ->
        deleteTask history id
    | ChangeTask (id, itemCommand) ->
        changeTask history id itemCommand
