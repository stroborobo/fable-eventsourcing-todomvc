module Todo.Domain.Types

open System


// MODEL

type TaskId =
    | TaskId of Guid
    with
    override this.ToString() =
        let (TaskId guid) = this
        string guid

type Task =
    { Id: TaskId
      Title: string
      Done: bool }

type TodoList = Task list

let emptyTask =
    { Id = TaskId Guid.Empty
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
    | Done of bool

type Event =
    | TaskAdded of TaskId * string
    | TaskChanged of TaskId * TaskEvent
    | TaskDeleted of TaskId
    | AllDone of bool
    | DoneCleared
    | Error of Error


// COMMANDS

type TaskCommand =
    | ChangeTitle of string
    | SetDone of bool

type Command =
    | AddTask of string
    | ChangeTask of TaskId * TaskCommand
    | DeleteTask of TaskId
    | SetAllDone of bool
    | ClearDone
