module Todo.Domain.Behaviour

open Types
open Projections

let addItem history title =
    [ ItemAdded title ]

let deleteItem history id =
    history
    |> todoListState
    |> List.tryFind (fun i -> i.Id = id)
    |> function
    | None -> [ Error NoItemFound ]
    | Some _ -> [ ItemDeleted id ]

let executeItem item command =
    match command with
    | ChangeTitle title ->
        [ ItemChanged (item.Id, ItemTitleChanged title) ]
    | SetDone ->
        if item.Done then [ Error AlreadyDone]
        else [ ItemChanged (item.Id, ItemDone) ]
    | SetUndone ->
        if item.Done then [ Error NotDone]
        else [ ItemChanged (item.Id, ItemUndone) ]

let changeItem history id itemCommand =
    history
    |> todoListState
    |> List.tryFind (fun i -> i.Id = id)
    |> function
    | Some item -> executeItem item itemCommand
    | None -> [ Error NoItemFound ]

let execute history command =
    match command with
    | AddItem title ->
        addItem history title
    | DeleteItem id ->
        deleteItem history id
    | ChangeItem (id, itemCommand) ->
        changeItem history id itemCommand
