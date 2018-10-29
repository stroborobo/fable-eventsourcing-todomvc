module Todo.Domain.Types

open System


// MODEL

type ItemId = TodoItemId of Guid

type Item =
    { Id: ItemId
      Title: string
      Done: bool }

type TodoList = Item list

let createEmptyItem () =
    { Id = Guid.NewGuid() |> TodoItemId
      Title = ""
      Done = false }

let emptyTodoList = []


// EVENTS

type Error =
    | TitleEmpty
    | AlreadyDone
    | NotDone
    | NoItemFound

type ItemEvent =
    | ItemTitleChanged of string
    | ItemDone
    | ItemUndone

type Event =
    | ItemAdded of string
    | ItemChanged of ItemId * ItemEvent
    | ItemDeleted of ItemId
    | DoneCleared
    | Error of Error


// COMMANDS

type ItemCommand =
    | ChangeTitle of string
    | SetDone
    | SetUndone

type Command =
    | AddItem of string
    | ChangeItem of ItemId * ItemCommand
    | DeleteItem of ItemId
