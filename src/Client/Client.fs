module Client

open Elmish
open Elmish.React

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.PowerPack.Fetch

open Thoth.Json

open Todo.Domain
open Todo.Domain.Types


type Model =
    { EventLog: Event list
      TodoList: TodoList }

let initialModel : Model =
    { EventLog = []
      TodoList = emptyTodoList }

type Msg =
    | DomainCommand of Command
    | DomainEvent of Event

let init () =
    initialModel, Cmd.none

let update msg model =
    match msg with
    | DomainCommand cmd ->
        let cmd =
            Behaviour.execute model.EventLog cmd
            |> List.map (DomainEvent >> Cmd.ofMsg)
            |> Cmd.batch
        model, cmd
    | DomainEvent event ->
        let nextModel =
            { model with
                TodoList = Projections.apply model.TodoList event
                EventLog = model.EventLog @ [event] }
        nextModel, Cmd.none

let view model dispatch =
    div [] []


#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
