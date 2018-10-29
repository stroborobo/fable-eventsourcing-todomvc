module Todo.Domain.Types

open System


// MODEL

type TaskId = TaskId of Guid

type Task =
    { Id: TaskId
      Title: string
      Done: bool }

type TodoList = Task list

let createEmptyTask () =
    { Id = Guid.NewGuid() |> TaskId
      Title = ""
      Done = false }

let emptyTodoList = []


// EVENTS

type Error =
    | TitleEmpty
    | AlreadyDone
    | NotDone
    | NoTaskFound

type TaskEvent =
    | TitleChanged of string
    | Done
    | Undone

type Event =
    | TaskAdded of string
    | TaskChanged of TaskId * TaskEvent
    | TaskDeleted of TaskId
    | DoneCleared
    | Error of Error


// COMMANDS

type TaskCommand =
    | ChangeTitle of string
    | SetDone
    | SetUndone

type Command =
    | AddTask of string
    | ChangeTask of TaskId * TaskCommand
    | DeleteTask of TaskId
