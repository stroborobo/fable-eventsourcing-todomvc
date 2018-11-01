module Todo.Client.Update

open Elmish

open Todo.Domain
open Todo.Domain.Types

open Model
open Subscription

let uiUpdate msg model =
    match msg with
    | NewTaskTitleChanged title ->
        { model with NewTaskTitle = title }, Cmd.none
    | ClearNewTaskTitle ->
        { model with NewTaskTitle = "" }, Cmd.none
    | TaskFilterChanged filter ->
        { model with TaskFilter = filter }, Cmd.none
    | StartEdit taskId ->
        let task = model.TodoList |> List.find (fun t -> t.Id = taskId)
        { model with EditingTask = Some (taskId, task.Title) }, Cmd.none
    | ChangeEdit newTitle ->
        let newEditingTask =
            model.EditingTask
            |> Option.map (fun (taskId, _) -> taskId, newTitle)
        { model with EditingTask = newEditingTask }, Cmd.none
    | EndEdit ->
        let cmd =
            model.EditingTask
            |> Option.map (fun (taskId, title) ->
                (taskId, ChangeTitle title) |> ChangeTask |> DomainCommand |> Cmd.ofMsg)
            |> Option.defaultValue Cmd.none
        model, cmd
    | CancelEdit ->
        { model with EditingTask = None }, Cmd.none

let connectionUpdate msg model =
    match msg with
    | ConnectionChanged state ->
        let cmd =
            match state with
            | Connected -> getHistory()
            | _ -> Cmd.none

        { model with ConnectionState = state }, cmd
    | Connect ->
         model, startConnection()
    | Disconnect ->
         model, stopConnection()

let update msg model =
    match msg with
    | UIMsg msg -> uiUpdate msg model
    | ConnectionMsg msg -> connectionUpdate msg model

    | HistoryLoaded events ->
        let todoList =
            (emptyTodoList, events)
            ||> List.fold (fun todoList event -> Projections.apply todoList event)
        { model with
            EventLog = events
            TodoList = todoList }, Cmd.none

    | DomainCommand cmd ->
        model, sendCommand cmd

    | DomainEvent event ->
        let nextModel =
            { model with
                TodoList = Projections.apply model.TodoList event
                EventLog = model.EventLog @ [event] }

        // follow up messages for ui state
        let cmd =
            match event, model.EditingTask with
            | TaskChanged (taskId, TitleChanged _), Some (editingId, _) when taskId = editingId ->
                Some CancelEdit

            | TaskAdded (_, title), _ when title = model.NewTaskTitle ->
                Some ClearNewTaskTitle

            | _ -> None
            |> Option.map (UIMsg >> Cmd.ofMsg)
            |> Option.defaultValue Cmd.none

        nextModel, cmd

    | NoOp -> model, Cmd.none
