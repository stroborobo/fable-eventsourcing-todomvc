# wip

# Fable + Eventsourcing • [TodoMVC](http://todomvc.com)

This example is built using:

> Fable is an F# to JavaScript compiler powered by Babel, designed to produce
> readable and standard code.  
> -- [Fable](http://fable.io)


> F# is a mature, open source, cross-platform, functional-first programming
> language. It empowers users and organizations to tackle complex computing
> problems with simple, maintainable and robust code.  
> -- [F#](https://fsharp.org)


> Elmish implements core abstractions that can be used to build Fable
> applications following the “model view update” style of architecture, as made
> famous by Elm.  
> -- [Elmish](https://elmish.github.io/elmish/)


> React is a JavaScript library for building declarative, component based
> user interfaces.  
> -- [React](https://reactjs.org)


> Capture all changes to an application state as a sequence of events.
> Event Sourcing ensures that all changes to application state are stored as a
> sequence of events. Not just can we query these events, we can also use the
> event log to reconstruct past states, and as a foundation to automatically
> adjust the state to cope with retroactive changes.  
> -- [Martin Fowler on Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html)


## Build + Run

Dependencies:

- .NET Core 2.1+
- FAKE 5+
- Yarn package manager
- Node 8+
- On systems other than windows: Mono

Build: `fake build`  
Build + Run: `fake build --target run`
