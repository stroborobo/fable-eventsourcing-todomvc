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

type Model =
    { EventLog: Event list
      TodoList: TodoList
      NewTaskTitle: string
      EditingTask: (TaskId * string) option
      TaskFilter: TaskFilter }

let initialModel : Model =
    { EventLog = []
      TodoList = emptyTodoList
      NewTaskTitle = ""
      EditingTask = None
      TaskFilter = All }

type Msg =
    | DomainCommand of Command
    | DomainEvent of Event
    | NewTaskTitleChanged of string
    | ClearNewTaskTitle
    | TaskFilterChanged of TaskFilter
    | StartEdit of TaskId
    | ChangeEdit of string
    | EndEdit
    | CancelEdit
