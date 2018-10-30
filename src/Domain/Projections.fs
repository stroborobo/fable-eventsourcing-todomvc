module Todo.Domain.Projections

open Types

let private itemApply event item =
    match event with
    | TitleChanged title ->
        { item with Title = title }
    | Done value ->
        { item with Done = value }

let private replaceTask id (fn: Task -> Task) items =
    items
    |> List.map (fun item ->
        if item.Id <> id then item
        else fn item)

let apply todoList event =
    match event with
    | TaskAdded (id, title) ->
        { emptyTask with Id = id; Title = title }
        |> List.singleton
        |> (@) todoList
    | TaskChanged (id, event) ->
        todoList
        |> replaceTask id (itemApply event)
    | TaskDeleted id ->
        todoList
        |> List.filter (fun item -> item.Id <> id)
    | AllDone value ->
        todoList
        |> List.map (fun task -> { task with Done = value })
    | DoneCleared ->
        todoList
        |> List.filter (fun item -> not item.Done)
    | Error _ -> todoList

let todoListState (history: Event list) =
    history
    |> List.fold apply emptyTodoList
