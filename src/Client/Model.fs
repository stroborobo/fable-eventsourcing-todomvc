module Todo.Client.Model

open Todo.Domain.Types

type TaskFilter =
    | All
    | ShowDone
    | ShowNotDone
    with
    override this.ToString() =
        match this with
        | All -> "All"
        | ShowDone -> "Completed"
        | ShowNotDone -> "Active"

type UIMsg =
    | NewTaskTitleChanged of string
    | ClearNewTaskTitle
    | TaskFilterChanged of TaskFilter
    | StartEdit of TaskId
    | ChangeEdit of string
    | EndEdit
    | CancelEdit

type ConnectionState =
    | NotConnected
    | Connected
    | ConnectionError of string

type ConnectionMsg =
    | ConnectionChanged of ConnectionState
    | Connect
    | Disconnect

type Msg =
    | DomainCommand of Command
    | DomainEvent of Event
    | UIMsg of UIMsg
    | ConnectionMsg of ConnectionMsg

type Model =
    { EventLog: Event list
      TodoList: TodoList
      NewTaskTitle: string
      EditingTask: (TaskId * string) option
      TaskFilter: TaskFilter
      ConnectionState: ConnectionState }

let initialModel =
    { EventLog = []
      TodoList = emptyTodoList
      NewTaskTitle = ""
      EditingTask = None
      TaskFilter = All
      ConnectionState = NotConnected }
