module Todo.Domain.Behaviour

open Types
open Projections

let addTask title =
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
    | SetDone value ->
        if value && item.Done then [ Error AlreadyDone]
        elif not value && not item.Done then [ Error NotDone]
        else [ TaskChanged (item.Id, Done value) ]

let changeTask history id itemCommand =
    history
    |> todoListState
    |> List.tryFind (fun i -> i.Id = id)
    |> function
    | Some item -> executeTask item itemCommand
    | None -> [ Error NoTaskFound ]

let setAllDone history value =
    history
    |> todoListState
    |> function
    | [] -> [ Error NoTaskFound ]
    | model ->
        let hasDifferences = model |> List.forall (fun t -> t.Done <> value)
        match hasDifferences, value with
        | false, false -> [ Error NotDone ]
        | false, true -> [ Error AlreadyDone ]
        | true, _ -> [ AllDone value ]

let clearDone history =
    history
    |> todoListState
    |> List.filter (fun t -> t.Done)
    |> function
    | [] -> [ Error NoTaskFound ]
    | _ -> [ DoneCleared ]

let execute history command =
    match command with
    | AddTask title ->
        addTask title
    | DeleteTask id ->
        deleteTask history id
    | ChangeTask (id, itemCommand) ->
        changeTask history id itemCommand
    | SetAllDone value ->
        setAllDone history value
    | ClearDone ->
        clearDone history
