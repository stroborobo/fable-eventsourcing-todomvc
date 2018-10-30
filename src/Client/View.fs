module Todo.Client.View

open Fable.Helpers.React
open Fable.Helpers.React.Props

open Todo.Domain.Types

open Model

let OnKey keyValue fn =
    OnKeyDown (fun event ->
        if event.key <> keyValue then ()
        else fn event)

let OnEscape = OnKey "Escape"
let OnEnter = OnKey "Enter"

let header newTaskText dispatch =
    let addTaskHandler (event: Fable.Import.React.SyntheticEvent) =
        AddTask newTaskText |> DomainCommand |> dispatch
    header [ Class "header" ]
        [ h1 [] [str "todos"]
          input
            [ Class "new-todo"
              Placeholder "What needs to be done?"
              AutoFocus true
              Value newTaskText
              OnChange (fun e -> NewTaskTitleChanged e.Value |> dispatch)
              OnBlur addTaskHandler
              OnEnter addTaskHandler ] ]

let taskView dispatch editingTask task =
    let isEditing, title =
        match editingTask with
        | Some (id, str) when id = task.Id -> true, str
        | _ -> false, task.Title
    let className =
        let editing = if isEditing then "editing" else ""
        let completed = if task.Done then "completed" else ""
        editing + " " + completed
    let id = "todo-" + (string task.Id)

    li [ Class className ]
        [ div [ Class "view" ]
            [ input
                [ Class "toggle"
                  Type "checkbox"
                  Checked task.Done
                  OnChange (fun _ ->
                    (task.Id, not task.Done |> SetDone)
                    |> ChangeTask
                    |> DomainCommand
                    |> dispatch) ]
              label
                [ OnDoubleClick (fun _ -> StartEdit task.Id |> dispatch) ]
                [ str task.Title ]
              button
                [ Class "destroy"
                  OnClick (fun _ -> DeleteTask task.Id |> DomainCommand |> dispatch) ]
                [] ]
          input
            [ Class "edit"
              Value title
              Id id
              OnChange (fun e -> ChangeEdit e.Value |> dispatch)
              OnBlur (fun _ -> EndEdit |> dispatch)
              OnEnter (fun _ -> dispatch EndEdit)
              OnEscape (fun _ -> dispatch CancelEdit) ] ]

let main hidden model dispatch =
    let allTasksDone = List.forall (fun t -> t.Done) model.TodoList
    let toggleAll = "toggle-all"
    let tasks =
        model.TodoList
        |> List.filter (fun t ->
            match model.TaskFilter with
            | All -> true
            | ShowNotDone -> not t.Done
            | ShowDone -> t.Done)
        |> List.map (taskView dispatch model.EditingTask)

    section
        [ Class "main"
          Hidden hidden ]
        [ input
            [ Class toggleAll
              Id toggleAll
              Type "checkbox"
              Checked allTasksDone
              OnChange (fun _ ->
                not allTasksDone
                |> SetAllDone
                |> DomainCommand
                |> dispatch ) ]
          label [ HtmlFor toggleAll ] [ str "Mark all as complete" ]
          ul [ Class "todo-list" ] tasks ]

let filterButton filter activeFilter dispatch =
    let className = if activeFilter = filter then "selected" else ""
    li [ OnClick (fun _ -> TaskFilterChanged filter |> dispatch ) ]
        [ a [ Class className ] [ string filter |> str ] ]

let footer' hidden model dispatch =
    let tasksDone =
        model.TodoList |> List.filter (fun t -> t.Done) |> List.length
    let tasksLeft = List.length model.TodoList - tasksDone
    let itemsLeftSuffix =
        if tasksLeft = 1 then " item" else " items"
        + " left"

    section
        [ Class "footer"
          Hidden hidden ]
        [ span [ Class "todo-count" ]
            [ strong [] [ string tasksLeft |> str ]
              str itemsLeftSuffix ]
          ul [ Class "filters" ]
            [ filterButton All model.TaskFilter dispatch
              str " "
              filterButton ShowNotDone model.TaskFilter dispatch
              str " "
              filterButton ShowDone model.TaskFilter dispatch ]
          button
            [ Class "clear-completed"
              Hidden (tasksDone = 0)
              OnClick (fun _ -> ClearDone |> DomainCommand |> dispatch) ]
            [ "Clear completed (" + string tasksDone + ")" |> str ] ]

let info =
    footer [ Class "info" ]
        [ p [] [ str "Double-click to edit a todo" ]
          p []
            [ str "Written by "
              a [ Href "https://github.com/yannolaf" ] [ str "Yann" ]
              str " and "
              a [ Href "https://github.com/stroborobo" ] [ str "Björn" ] ]
          p []
            [ str "Part of "
              a [ Href "http://todomvc.com" ] [ str "TodoMVC" ] ] ]

let view model dispatch =
    let hidden = List.isEmpty model.TodoList
    div []
        [ section [ Class "todoapp" ]
            [ header model.NewTaskTitle dispatch
              main hidden model dispatch
              footer' hidden model dispatch ]
          info ]
