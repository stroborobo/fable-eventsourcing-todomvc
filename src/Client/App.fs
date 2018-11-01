module Todo.Client.App

open Elmish
open Elmish.React
#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

open Fable.Core.JsInterop

open Model
open View
open Subscription
open Update

importAll "./public/index.html" |> ignore

let init () = initialModel, startConnection()

Program.mkProgram init update view
|> Program.withSubscription subscriptions
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReactUnoptimized "elmish-app"
|> Program.run
