module Todo.Client.App

open Elmish
open Elmish.React
open Fable.Core
open Fable.Core.JsInterop

open Todo.Domain
open Todo.Domain.Types

open Model
open View

importAll "./public/index.html" |> ignore

let init () =
    initialModel, Cmd.none

let update msg model =
    match msg with

    // Domain messages

    | DomainCommand cmd ->
        let cmd =
            Behaviour.execute model.EventLog cmd
            |> List.map (DomainEvent >> Cmd.ofMsg)
            |> Cmd.batch
        model, cmd
    //| DomainEvent (Error e) ->
    | DomainEvent event ->
        let nextModel =
            { model with
                TodoList = Projections.apply model.TodoList event
                EventLog = model.EventLog @ [event] }

        // follow up messages for ui state
        let cmd =
            match event, model.EditingTask with
            | TaskChanged (taskId, TitleChanged _), Some (editingId, _) when taskId = editingId ->
                CancelEdit |> Cmd.ofMsg
            | TaskAdded (_, title), _ when title = model.NewTaskTitle ->
                ClearNewTaskTitle |> Cmd.ofMsg
            | _ -> Cmd.none

        nextModel, cmd


    // UI Messages

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



#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReactUnoptimized "elmish-app"
// |> Program.withReact "elmish-app"
#if DEBUG
// |> Program.withDebugger
#endif
|> Program.run
