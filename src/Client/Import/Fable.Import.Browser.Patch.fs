module Fable.Import.Browser.Patch

open System
open Fable.Core
open Fable.Import.JS

// Vorläufige Lösung, da Fable.Import.Browser outdated ist
// und SignalR moderne Typen verwendet. In Zukunft von
// lib.dom.d.ts aktuelle Fable.Import.Browser.fs erstellen.

type [<AllowNullLiteral>] EventSourceInit =
    abstract withCredentials: bool

type [<StringEnum>] [<RequireQualifiedAccess>] XMLHttpRequestResponseType =
    | [<CompiledName "">] Empty
    | Arraybuffer
    | Blob
    | Document
    | Json
    | Text
