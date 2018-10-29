module Client

open Elmish
open Elmish.React

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.PowerPack.Fetch

open Thoth.Json

open Todo.Domain
open Todo.Domain.Types


type Model = TodoList

type Msg =
    | DomainEvent of Event
    | NoOp

let init () =
    emptyTodoList, Cmd.none

let update msg model =
    match msg with
    | DomainEvent event ->
        let nextModel = Projections.apply model event
        nextModel, Cmd.none
    | NoOp -> model, Cmd.none

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
