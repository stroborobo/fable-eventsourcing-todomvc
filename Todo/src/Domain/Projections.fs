module Todo.Domain.Projections

open Types

let private itemApply event item =
    match event with
    | ItemTitleChanged title ->
        { item with Title = title }
    | ItemDone ->
        { item with Done = true }
    | ItemUndone ->
        { item with Done = false }

let private replaceItem id (fn: Item -> Item) items =
    items
    |> List.map (fun item ->
        if item.Id <> id then item
        else fn item)

let apply todoList event =
    match event with
    | ItemAdded title ->
        { createEmptyItem() with Title = title }
        |> List.singleton
        |> (@) todoList
    | ItemChanged (id, event) ->
        todoList 
        |> replaceItem id (itemApply event)
    | ItemDeleted id ->
        todoList
        |> List.filter (fun item -> item.Id <> id)
    | DoneCleared ->
        todoList 
        |> List.filter (fun item -> not item.Done)
    | Error _ -> todoList
        
 
let todoListState (history: Event list) =
    history
    |> List.fold apply emptyTodoList