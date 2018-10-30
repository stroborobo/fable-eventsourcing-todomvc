// ts2fable 0.6.1
module rec Fable.Import.SignalR
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser
open Fable.Import.Browser.Patch

type ISignalR =
    abstract HubConnectionBuilder: HubConnectionBuilder.HubConnectionBuilderStatic
    // ...

let [<Import("*", "@aspnet/signalr")>] signalR: ISignalR = jsNative

// let [<Import("*","")>] abortController: AbortController.IExports = jsNative
// let [<Import("*","")>] errors: Errors.IExports = jsNative
// let [<Import("*","")>] handshakeProtocol: HandshakeProtocol.IExports = jsNative
// let [<Import("*","")>] httpClient: HttpClient.IExports = jsNative
// let [<Import("*","")>] httpConnection: HttpConnection.IExports = jsNative
// let [<Import("*","")>] hubConnection: HubConnection.IExports = jsNative
// let [<Import("*","")>] signalR: HubConnectionBuilder.IExports = jsNative
// let [<Import("*","")>] index: Index.IExports = jsNative
// let [<Import("*","")>] jsonHubProtocol: JsonHubProtocol.IExports = jsNative
// let [<Import("*","")>] loggers: Loggers.IExports = jsNative
// let [<Import("*","")>] longPollingTransport: LongPollingTransport.IExports = jsNative
// let [<Import("*","")>] polyfills: Polyfills.IExports = jsNative
// let [<Import("*","")>] serverSentEventsTransport: ServerSentEventsTransport.IExports = jsNative
// let [<Import("*","")>] textMessageFormat: TextMessageFormat.IExports = jsNative
// let [<Import("*","")>] utils: Utils.IExports = jsNative
// let [<Import("*","")>] webSocketTransport: WebSocketTransport.IExports = jsNative

module AbortController =

    type [<AllowNullLiteral>] IExports =
        abstract AbortController: AbortControllerStatic

    type [<AllowNullLiteral>] AbortController =
        inherit AbortSignal
        abstract isAborted: obj with get, set
        abstract onabort: (unit -> unit) option with get, set
        abstract abort: unit -> unit
        abstract signal: AbortSignal
        abstract aborted: bool

    type [<AllowNullLiteral>] AbortControllerStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> AbortController

    /// Represents a signal that can be monitored to determine if a request has been aborted.
    type [<AllowNullLiteral>] AbortSignal =
        /// Indicates if the request has been aborted.
        abstract aborted: bool with get, set
        /// Set this to a handler that will be invoked when the request is aborted.
        abstract onabort: (unit -> unit) option with get, set

module Errors =

    type [<AllowNullLiteral>] IExports =
        abstract HttpError: HttpErrorStatic
        abstract TimeoutError: TimeoutErrorStatic
        abstract AbortError: AbortErrorStatic

    /// Error thrown when an HTTP request fails.
    type [<AllowNullLiteral>] HttpError =
        inherit Error
        abstract __proto__: obj with get, set
        /// The HTTP status code represented by this error.
        abstract statusCode: float with get, set

    /// Error thrown when an HTTP request fails.
    type [<AllowNullLiteral>] HttpErrorStatic =
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.HttpError}.</summary>
        /// <param name="errorMessage">A descriptive error message.</param>
        /// <param name="statusCode">The HTTP status code represented by this error.</param>
        [<Emit "new $0($1...)">] abstract Create: errorMessage: string * statusCode: float -> HttpError

    /// Error thrown when a timeout elapses.
    type [<AllowNullLiteral>] TimeoutError =
        inherit Error
        abstract __proto__: obj with get, set

    /// Error thrown when a timeout elapses.
    type [<AllowNullLiteral>] TimeoutErrorStatic =
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.TimeoutError}.</summary>
        /// <param name="errorMessage">A descriptive error message.</param>
        [<Emit "new $0($1...)">] abstract Create: ?errorMessage: string -> TimeoutError

    /// Error thrown when an action is aborted.
    type [<AllowNullLiteral>] AbortError =
        inherit Error
        abstract __proto__: obj with get, set

    /// Error thrown when an action is aborted.
    type [<AllowNullLiteral>] AbortErrorStatic =
        /// <summary>Constructs a new instance of {@link AbortError}.</summary>
        /// <param name="errorMessage">A descriptive error message.</param>
        [<Emit "new $0($1...)">] abstract Create: ?errorMessage: string -> AbortError

module ILogger =

    type [<RequireQualifiedAccess>] LogLevel =
        | Trace = 0
        | Debug = 1
        | Information = 2
        | Warning = 3
        | Error = 4
        | Critical = 5
        | None = 6

    /// An abstraction that provides a sink for diagnostic messages.
    type [<AllowNullLiteral>] ILogger =
        /// <summary>Called by the framework to emit a diagnostic message.</summary>
        /// <param name="logLevel">The severity level of the message.</param>
        /// <param name="message">The message.</param>
        abstract log: logLevel: LogLevel * message: string -> unit

module HttpClient =
    type AbortSignal = AbortController.AbortSignal
    type ILogger = ILogger.ILogger

    type [<AllowNullLiteral>] IExports =
        abstract HttpResponse: HttpResponseStatic
        abstract HttpClient: HttpClientStatic
        abstract DefaultHttpClient: DefaultHttpClientStatic

    /// Represents an HTTP request.
    type [<AllowNullLiteral>] HttpRequest =
        /// The HTTP method to use for the request.
        abstract ``method``: string option with get, set
        /// The URL for the request.
        abstract url: string option with get, set
        /// The body content for the request. May be a string or an ArrayBuffer (for binary data).
        abstract content: U2<string, ArrayBuffer> option with get, set
        /// An object describing headers to apply to the request.
        abstract headers: obj option with get, set
        /// The XMLHttpRequestResponseType to apply to the request.
        abstract responseType: XMLHttpRequestResponseType option with get, set
        /// An AbortSignal that can be monitored for cancellation.
        abstract abortSignal: AbortSignal option with get, set
        /// The time to wait for the request to complete before throwing a TimeoutError. Measured in milliseconds.
        abstract timeout: float option with get, set

    /// Represents an HTTP response.
    type [<AllowNullLiteral>] HttpResponse =
        abstract statusCode: float
        abstract statusText: string
        abstract content: U2<string, ArrayBuffer>

    /// Represents an HTTP response.
    type [<AllowNullLiteral>] HttpResponseStatic =
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.HttpResponse} with the specified status code.</summary>
        /// <param name="statusCode">The status code of the response.</param>
        [<Emit "new $0($1...)">] abstract Create: statusCode: float -> HttpResponse
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.HttpResponse} with the specified status code and message.</summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="statusText">The status message of the response.</param>
        [<Emit "new $0($1...)">] abstract Create: statusCode: float * statusText: string -> HttpResponse
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.HttpResponse} with the specified status code, message and string content.</summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="statusText">The status message of the response.</param>
        /// <param name="content">The content of the response.</param>
        [<Emit "new $0($1...)">] abstract Create: statusCode: float * statusText: string * content: string -> HttpResponse
        /// <summary>Constructs a new instance of {@link @aspnet/signalr.HttpResponse} with the specified status code, message and binary content.</summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="statusText">The status message of the response.</param>
        /// <param name="content">The content of the response.</param>
        [<Emit "new $0($1...)">] abstract Create: statusCode: float * statusText: string * content: ArrayBuffer -> HttpResponse

    /// Abstraction over an HTTP client.
    ///
    /// This class provides an abstraction over an HTTP client so that a different implementation can be provided on different platforms.
    type [<AllowNullLiteral>] HttpClient =
        /// <summary>Issues an HTTP GET request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        abstract get: url: string -> Promise<HttpResponse>
        /// <summary>Issues an HTTP GET request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        /// <param name="options">Additional options to configure the request. The 'url' field in this object will be overridden by the url parameter.</param>
        abstract get: url: string * options: HttpRequest -> Promise<HttpResponse>
        /// <summary>Issues an HTTP POST request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        abstract post: url: string -> Promise<HttpResponse>
        /// <summary>Issues an HTTP POST request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        /// <param name="options">Additional options to configure the request. The 'url' field in this object will be overridden by the url parameter.</param>
        abstract post: url: string * options: HttpRequest -> Promise<HttpResponse>
        /// <summary>Issues an HTTP DELETE request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        abstract delete: url: string -> Promise<HttpResponse>
        /// <summary>Issues an HTTP DELETE request to the specified URL, returning a Promise that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="url">The URL for the request.</param>
        /// <param name="options">Additional options to configure the request. The 'url' field in this object will be overridden by the url parameter.</param>
        abstract delete: url: string * options: HttpRequest -> Promise<HttpResponse>
        /// <summary>Issues an HTTP request to the specified URL, returning a {@link Promise} that resolves with an {@link @aspnet/signalr.HttpResponse} representing the result.</summary>
        /// <param name="request">An {</param>
        abstract send: request: HttpRequest -> Promise<HttpResponse>

    /// Abstraction over an HTTP client.
    ///
    /// This class provides an abstraction over an HTTP client so that a different implementation can be provided on different platforms.
    type [<AllowNullLiteral>] HttpClientStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> HttpClient

    /// Default implementation of {@link @aspnet/signalr.HttpClient}.
    type [<AllowNullLiteral>] DefaultHttpClient =
        inherit HttpClient
        abstract logger: obj
        abstract send: request: HttpRequest -> Promise<HttpResponse>

    /// Default implementation of {@link @aspnet/signalr.HttpClient}.
    type [<AllowNullLiteral>] DefaultHttpClientStatic =
        /// Creates a new instance of the {@link @aspnet/signalr.DefaultHttpClient}, using the provided {@link @aspnet/signalr.ILogger} to log messages.
        [<Emit "new $0($1...)">] abstract Create: logger: ILogger -> DefaultHttpClient

module ITransport =

    type [<RequireQualifiedAccess>] HttpTransportType =
        | None = 0
        | WebSockets = 1
        | ServerSentEvents = 2
        | LongPolling = 4

    type [<RequireQualifiedAccess>] TransferFormat =
        | Text = 1
        | Binary = 2

    /// An abstraction over the behavior of transports. This is designed to support the framework and not intended for use by applications.
    type [<AllowNullLiteral>] ITransport =
        abstract connect: url: string * transferFormat: TransferFormat -> Promise<unit>
        abstract send: data: obj option -> Promise<unit>
        abstract stop: unit -> Promise<unit>
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set

module Polyfills =

    type [<AllowNullLiteral>] IExports =
        abstract EventSourceConstructor: EventSourceConstructorStatic
        abstract WebSocketConstructor: WebSocketConstructorStatic

    type [<AllowNullLiteral>] EventSourceConstructor =
        interface end

    type [<AllowNullLiteral>] EventSourceConstructorStatic =
        [<Emit "new $0($1...)">] abstract Create: url: string * ?eventSourceInitDict: EventSourceInit -> EventSourceConstructor

    type [<AllowNullLiteral>] WebSocketConstructor =
        abstract CLOSED: float
        abstract CLOSING: float
        abstract CONNECTING: float
        abstract OPEN: float

    type [<AllowNullLiteral>] WebSocketConstructorStatic =
        [<Emit "new $0($1...)">] abstract Create: url: string * ?protocols: U2<string, ResizeArray<string>> -> WebSocketConstructor

module IHttpConnectionOptions =
    type HttpClient = HttpClient.HttpClient
    type ILogger = ILogger.ILogger
    type LogLevel = ILogger.LogLevel
    type HttpTransportType = ITransport.HttpTransportType
    type ITransport = ITransport.ITransport
    type EventSourceConstructor = Polyfills.EventSourceConstructor
    type WebSocketConstructor = Polyfills.WebSocketConstructor

    /// Options provided to the 'withUrl' method on {@link @aspnet/signalr.HubConnectionBuilder} to configure options for the HTTP-based transports.
    type [<AllowNullLiteral>] IHttpConnectionOptions =
        /// An {@link @aspnet/signalr.HttpClient} that will be used to make HTTP requests.
        abstract httpClient: HttpClient option with get, set
        /// An {@link @aspnet/signalr.HttpTransportType} value specifying the transport to use for the connection.
        abstract transport: U2<HttpTransportType, ITransport> option with get, set
        /// Configures the logger used for logging.
        ///
        /// Provide an {@link @aspnet/signalr.ILogger} instance, and log messages will be logged via that instance. Alternatively, provide a value from
        /// the {@link @aspnet/signalr.LogLevel} enumeration and a default logger which logs to the Console will be configured to log messages of the specified
        /// level (or higher).
        abstract logger: U2<ILogger, LogLevel> option with get, set
        /// A function that provides an access token required for HTTP Bearer authentication.
        abstract accessTokenFactory: unit -> U2<string, Promise<string>>
        /// A boolean indicating if message content should be logged.
        ///
        /// Message content can contain sensitive user data, so this is disabled by default.
        abstract logMessageContent: bool option with get, set
        /// A boolean indicating if negotiation should be skipped.
        ///
        /// Negotiation can only be skipped when the {@link @aspnet/signalr.IHttpConnectionOptions.transport} property is set to 'HttpTransportType.WebSockets'.
        abstract skipNegotiation: bool option with get, set
        /// A constructor that can be used to create a WebSocket.
        abstract WebSocket: WebSocketConstructor option with get, set
        /// A constructor that can be used to create an EventSource.
        abstract EventSource: EventSourceConstructor option with get, set

module TextMessageFormat =

    type [<AllowNullLiteral>] IExports =
        abstract TextMessageFormat: TextMessageFormatStatic

    type [<AllowNullLiteral>] TextMessageFormat =
        abstract RecordSeparatorCode: float with get, set
        abstract RecordSeparator: string with get, set

    type [<AllowNullLiteral>] TextMessageFormatStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> TextMessageFormat
        abstract write: output: string -> string
        abstract parse: input: string -> ResizeArray<string>

module HandshakeProtocol =

    type [<AllowNullLiteral>] IExports =
        abstract HandshakeProtocol: HandshakeProtocolStatic

    type [<AllowNullLiteral>] HandshakeRequestMessage =
        abstract protocol: string
        abstract version: float

    type [<AllowNullLiteral>] HandshakeResponseMessage =
        abstract error: string

    type [<AllowNullLiteral>] HandshakeProtocol =
        abstract writeHandshakeRequest: handshakeRequest: HandshakeRequestMessage -> string
        abstract parseHandshakeResponse: data: obj option -> obj option * HandshakeResponseMessage

    type [<AllowNullLiteral>] HandshakeProtocolStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> HandshakeProtocol

module IConnection =
    type TransferFormat = ITransport.TransferFormat

    type [<AllowNullLiteral>] IConnection =
        abstract features: obj option
        abstract start: transferFormat: TransferFormat -> Promise<unit>
        abstract send: data: U2<string, ArrayBuffer> -> Promise<unit>
        abstract stop: ?error: Error -> Promise<unit>
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set

module IHubProtocol =
    type ILogger = ILogger.ILogger
    type TransferFormat = ITransport.TransferFormat

    type [<RequireQualifiedAccess>] MessageType =
        | Invocation = 1
        | StreamItem = 2
        | Completion = 3
        | StreamInvocation = 4
        | CancelInvocation = 5
        | Ping = 6
        | Close = 7

    /// Defines a dictionary of string keys and string values representing headers attached to a Hub message.
    type [<AllowNullLiteral>] MessageHeaders =
        /// Gets or sets the header with the specified key.
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> string with get, set

    type HubMessage =
        U7<InvocationMessage, StreamInvocationMessage, StreamItemMessage, CompletionMessage, CancelInvocationMessage, PingMessage, CloseMessage>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module HubMessage =
        let ofInvocationMessage v: HubMessage = v |> U7.Case1
        let isInvocationMessage (v: HubMessage) = match v with U7.Case1 _ -> true | _ -> false
        let asInvocationMessage (v: HubMessage) = match v with U7.Case1 o -> Some o | _ -> None
        let ofStreamInvocationMessage v: HubMessage = v |> U7.Case2
        let isStreamInvocationMessage (v: HubMessage) = match v with U7.Case2 _ -> true | _ -> false
        let asStreamInvocationMessage (v: HubMessage) = match v with U7.Case2 o -> Some o | _ -> None
        let ofStreamItemMessage v: HubMessage = v |> U7.Case3
        let isStreamItemMessage (v: HubMessage) = match v with U7.Case3 _ -> true | _ -> false
        let asStreamItemMessage (v: HubMessage) = match v with U7.Case3 o -> Some o | _ -> None
        let ofCompletionMessage v: HubMessage = v |> U7.Case4
        let isCompletionMessage (v: HubMessage) = match v with U7.Case4 _ -> true | _ -> false
        let asCompletionMessage (v: HubMessage) = match v with U7.Case4 o -> Some o | _ -> None
        let ofCancelInvocationMessage v: HubMessage = v |> U7.Case5
        let isCancelInvocationMessage (v: HubMessage) = match v with U7.Case5 _ -> true | _ -> false
        let asCancelInvocationMessage (v: HubMessage) = match v with U7.Case5 o -> Some o | _ -> None
        let ofPingMessage v: HubMessage = v |> U7.Case6
        let isPingMessage (v: HubMessage) = match v with U7.Case6 _ -> true | _ -> false
        let asPingMessage (v: HubMessage) = match v with U7.Case6 o -> Some o | _ -> None
        let ofCloseMessage v: HubMessage = v |> U7.Case7
        let isCloseMessage (v: HubMessage) = match v with U7.Case7 _ -> true | _ -> false
        let asCloseMessage (v: HubMessage) = match v with U7.Case7 o -> Some o | _ -> None

    /// Defines properties common to all Hub messages.
    type [<AllowNullLiteral>] HubMessageBase =
        /// A {@link @aspnet/signalr.MessageType} value indicating the type of this message.
        abstract ``type``: MessageType

    /// Defines properties common to all Hub messages relating to a specific invocation.
    type [<AllowNullLiteral>] HubInvocationMessage =
        inherit HubMessageBase
        /// A {@link @aspnet/signalr.MessageHeaders} dictionary containing headers attached to the message.
        abstract headers: MessageHeaders option
        /// The ID of the invocation relating to this message.
        ///
        /// This is expected to be present for {@link @aspnet/signalr.StreamInvocationMessage} and {@link @aspnet/signalr.CompletionMessage}. It may
        /// be 'undefined' for an {@link @aspnet/signalr.InvocationMessage} if the sender does not expect a response.
        abstract invocationId: string option

    /// A hub message representing a non-streaming invocation.
    type [<AllowNullLiteral>] InvocationMessage =
        inherit HubInvocationMessage
        abstract ``type``: MessageType
        /// The target method name.
        abstract target: string
        /// The target method arguments.
        abstract arguments: ResizeArray<obj option>

    /// A hub message representing a streaming invocation.
    type [<AllowNullLiteral>] StreamInvocationMessage =
        inherit HubInvocationMessage
        abstract ``type``: MessageType
        /// The invocation ID.
        abstract invocationId: string
        /// The target method name.
        abstract target: string
        /// The target method arguments.
        abstract arguments: ResizeArray<obj option>

    /// A hub message representing a single item produced as part of a result stream.
    type [<AllowNullLiteral>] StreamItemMessage =
        inherit HubInvocationMessage
        abstract ``type``: MessageType
        /// The invocation ID.
        abstract invocationId: string
        /// The item produced by the server.
        abstract item: obj option

    /// A hub message representing the result of an invocation.
    type [<AllowNullLiteral>] CompletionMessage =
        inherit HubInvocationMessage
        abstract ``type``: MessageType
        /// The invocation ID.
        abstract invocationId: string
        /// The error produced by the invocation, if any.
        ///
        /// Either {@link @aspnet/signalr.CompletionMessage.error} or {@link @aspnet/signalr.CompletionMessage.result} must be defined, but not both.
        abstract error: string option
        /// The result produced by the invocation, if any.
        ///
        /// Either {@link @aspnet/signalr.CompletionMessage.error} or {@link @aspnet/signalr.CompletionMessage.result} must be defined, but not both.
        abstract result: obj option

    /// A hub message indicating that the sender is still active.
    type [<AllowNullLiteral>] PingMessage =
        inherit HubMessageBase
        abstract ``type``: MessageType

    /// A hub message indicating that the sender is closing the connection.
    ///
    /// If {@link @aspnet/signalr.CloseMessage.error} is defined, the sender is closing the connection due to an error.
    type [<AllowNullLiteral>] CloseMessage =
        inherit HubMessageBase
        abstract ``type``: MessageType
        /// The error that triggered the close, if any.
        ///
        /// If this property is undefined, the connection was closed normally and without error.
        abstract error: string option

    /// A hub message sent to request that a streaming invocation be canceled.
    type [<AllowNullLiteral>] CancelInvocationMessage =
        inherit HubInvocationMessage
        abstract ``type``: MessageType
        /// The invocation ID.
        abstract invocationId: string

    /// A protocol abstraction for communicating with SignalR Hubs.
    type [<AllowNullLiteral>] IHubProtocol =
        /// The name of the protocol. This is used by SignalR to resolve the protocol between the client and server.
        abstract name: string
        /// The version of the protocol.
        abstract version: float
        /// The {@link @aspnet/signalr.TransferFormat} of the protocol.
        abstract transferFormat: TransferFormat
        /// <summary>Creates an array of {@link @aspnet/signalr.HubMessage} objects from the specified serialized representation.
        ///
        /// If {@link @aspnet/signalr.IHubProtocol.transferFormat} is 'Text', the `input` parameter must be a string, otherwise it must be an ArrayBuffer.</summary>
        /// <param name="input">A string, or ArrayBuffer containing the serialized representation.</param>
        /// <param name="logger">A logger that will be used to log messages that occur during parsing.</param>
        abstract parseMessages: input: U2<string, ArrayBuffer> * logger: ILogger -> ResizeArray<HubMessage>
        /// <summary>Writes the specified {@link @aspnet/signalr.HubMessage} to a string or ArrayBuffer and returns it.
        ///
        /// If {@link @aspnet/signalr.IHubProtocol.transferFormat} is 'Text', the result of this method will be a string, otherwise it will be an ArrayBuffer.</summary>
        /// <param name="message">The message to write.</param>
        abstract writeMessage: message: HubMessage -> U2<string, ArrayBuffer>

module Stream =

    /// Defines the expected type for a receiver of results streamed by the server.
    type [<AllowNullLiteral>] IStreamSubscriber<'T> =
        /// A boolean that will be set by the {@link @aspnet/signalr.IStreamResult} when the stream is closed.
        abstract closed: bool option with get, set
        /// Called by the framework when a new item is available.
        abstract next: value: 'T -> unit
        /// Called by the framework when an error has occurred.
        ///
        /// After this method is called, no additional methods on the {@link @aspnet/signalr.IStreamSubscriber} will be called.
        abstract error: err: obj option -> unit
        /// Called by the framework when the end of the stream is reached.
        ///
        /// After this method is called, no additional methods on the {@link @aspnet/signalr.IStreamSubscriber} will be called.
        abstract complete: unit -> unit

    /// Defines the result of a streaming hub method.
    type [<AllowNullLiteral>] IStreamResult<'T> =
        /// Attaches a {@link @aspnet/signalr.IStreamSubscriber}, which will be invoked when new items are available from the stream.
        abstract subscribe: subscriber: IStreamSubscriber<'T> -> ISubscription<'T>

    /// An interface that allows an {@link @aspnet/signalr.IStreamSubscriber} to be disconnected from a stream.
    type [<AllowNullLiteral>] ISubscription<'T> =
        /// Disconnects the {@link @aspnet/signalr.IStreamSubscriber} associated with this subscription from the stream.
        abstract dispose: unit -> unit

module Loggers =
    type ILogger = ILogger.ILogger
    type LogLevel = ILogger.LogLevel

    type [<AllowNullLiteral>] IExports =
        abstract NullLogger: NullLoggerStatic

    /// A logger that does nothing when log messages are sent to it.
    type [<AllowNullLiteral>] NullLogger =
        inherit ILogger
        /// The singleton instance of the {@link @aspnet/signalr.NullLogger}.
        abstract instance: ILogger with get, set
        abstract log: _logLevel: LogLevel * _message: string -> unit

    /// A logger that does nothing when log messages are sent to it.
    type [<AllowNullLiteral>] NullLoggerStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> NullLogger

module Utils =
    type HttpClient = HttpClient.HttpClient
    type ILogger = ILogger.ILogger
    type LogLevel = ILogger.LogLevel
    type IStreamResult<'a> = Stream.IStreamResult<'a>
    type IStreamSubscriber<'a> = Stream.IStreamSubscriber<'a>
    type ISubscription<'a> = Stream.ISubscription<'a>

    type [<AllowNullLiteral>] IExports =
        abstract Arg: ArgStatic
        abstract getDataDetail: data: obj option * includeContent: bool -> string
        abstract formatArrayBuffer: data: ArrayBuffer -> string
        abstract sendMessage: logger: ILogger * transportName: string * httpClient: HttpClient * url: string * accessTokenFactory: (unit -> U2<string, Promise<string>>) option * content: U2<string, ArrayBuffer> * logMessageContent: bool -> Promise<unit>
        abstract createLogger: ?logger: U2<ILogger, LogLevel> -> ILogger
        abstract Subject: SubjectStatic
        abstract SubjectSubscription: SubjectSubscriptionStatic
        abstract ConsoleLogger: ConsoleLoggerStatic

    type [<AllowNullLiteral>] Arg =
        interface end

    type [<AllowNullLiteral>] ArgStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> Arg
        abstract isRequired: ``val``: obj option * name: string -> unit
        abstract isIn: ``val``: obj option * values: obj option * name: string -> unit

    type [<AllowNullLiteral>] Subject<'T> =
        inherit IStreamResult<'T>
        abstract observers: Array<IStreamSubscriber<'T>> with get, set
        abstract cancelCallback: (unit -> Promise<unit>) with get, set
        abstract next: item: 'T -> unit
        abstract error: err: obj option -> unit
        abstract complete: unit -> unit
        abstract subscribe: observer: IStreamSubscriber<'T> -> ISubscription<'T>

    type [<AllowNullLiteral>] SubjectStatic =
        [<Emit "new $0($1...)">] abstract Create: cancelCallback: (unit -> Promise<unit>) -> Subject<'T>

    type [<AllowNullLiteral>] SubjectSubscription<'T> =
        inherit ISubscription<'T>
        abstract subject: obj with get, set
        abstract observer: obj with get, set
        abstract dispose: unit -> unit

    type [<AllowNullLiteral>] SubjectSubscriptionStatic =
        [<Emit "new $0($1...)">] abstract Create: subject: Subject<'T> * observer: IStreamSubscriber<'T> -> SubjectSubscription<'T>

    type [<AllowNullLiteral>] ConsoleLogger =
        inherit ILogger
        abstract minimumLogLevel: obj
        abstract log: logLevel: LogLevel * message: string -> unit

    type [<AllowNullLiteral>] ConsoleLoggerStatic =
        [<Emit "new $0($1...)">] abstract Create: minimumLogLevel: LogLevel -> ConsoleLogger

module HubConnection =
    type IConnection = IConnection.IConnection
    type IHubProtocol = IHubProtocol.IHubProtocol
    type ILogger = ILogger.ILogger
    type IStreamResult<'a> = Stream.IStreamResult<'a>

    type [<AllowNullLiteral>] IExports =
        abstract HubConnection: HubConnectionStatic

    type [<RequireQualifiedAccess>] HubConnectionState =
        | Disconnected = 0
        | Connected = 1

    /// Represents a connection to a SignalR Hub.
    type [<AllowNullLiteral>] HubConnection =
        abstract cachedPingMessage: obj
        abstract connection: obj
        abstract logger: obj
        abstract protocol: obj with get, set
        abstract handshakeProtocol: obj with get, set
        abstract callbacks: obj with get, set
        abstract methods: obj with get, set
        abstract id: obj with get, set
        abstract closedCallbacks: obj with get, set
        abstract receivedHandshakeResponse: obj with get, set
        abstract connectionState: obj with get, set
        abstract timeoutHandle: obj option with get, set
        abstract pingServerHandle: obj option with get, set
        /// The server timeout in milliseconds.
        ///
        /// If this timeout elapses without receiving any messages from the server, the connection will be terminated with an error.
        /// The default timeout value is 30,000 milliseconds (30 seconds).
        abstract serverTimeoutInMilliseconds: float with get, set
        /// Default interval at which to ping the server.
        ///
        /// The default value is 15,000 milliseconds (15 seconds).
        /// Allows the server to detect hard disconnects (like when a client unplugs their computer).
        abstract keepAliveIntervalInMilliseconds: float with get, set
        /// Indicates the state of the {@link HubConnection} to the server.
        abstract state: HubConnectionState
        /// Starts the connection.
        abstract start: unit -> Promise<unit>
        /// Stops the connection.
        abstract stop: unit -> Promise<unit>
        /// <summary>Invokes a streaming hub method on the server using the specified name and arguments.</summary>
        /// <param name="methodName">The name of the server method to invoke.</param>
        /// <param name="args">The arguments used to invoke the server method.</param>
        abstract stream<'t> : methodName: string * [<ParamArray>] args: ResizeArray<obj option> -> IStreamResult<'t>
        abstract sendMessage: message: obj -> unit
        /// <summary>Invokes a hub method on the server using the specified name and arguments. Does not wait for a response from the receiver.
        ///
        /// The Promise returned by this method resolves when the client has sent the invocation to the server. The server may still
        /// be processing the invocation.</summary>
        /// <param name="methodName">The name of the server method to invoke.</param>
        /// <param name="args">The arguments used to invoke the server method.</param>
        abstract send: methodName: string * [<ParamArray>] args: ResizeArray<obj option> -> Promise<unit>
        /// <summary>Invokes a hub method on the server using the specified name and arguments.
        ///
        /// The Promise returned by this method resolves when the server indicates it has finished invoking the method. When the promise
        /// resolves, the server has finished invoking the method. If the server method returns a result, it is produced as the result of
        /// resolving the Promise.</summary>
        /// <param name="methodName">The name of the server method to invoke.</param>
        /// <param name="args">The arguments used to invoke the server method.</param>
        abstract invoke: methodName: string * [<ParamArray>] args: obj option array -> Promise<'T>
        /// <summary>Registers a handler that will be invoked when the hub method with the specified method name is invoked.</summary>
        /// <param name="methodName">The name of the hub method to define.</param>
        /// <param name="newMethod">The handler that will be raised when the hub method is invoked.</param>
        abstract on<'t> : methodName: string * newMethod: ('t -> unit) -> unit
        /// <summary>Removes all handlers for the specified hub method.</summary>
        /// <param name="methodName">The name of the method to remove handlers for.</param>
        abstract off: methodName: string -> unit
        /// <summary>Removes the specified handler for the specified hub method.
        ///
        /// You must pass the exact same Function instance as was previously passed to {@link @aspnet/signalr.HubConnection.on}. Passing a different instance (even if the function
        /// body is the same) will not remove the handler.</summary>
        /// <param name="methodName">The name of the method to remove handlers for.</param>
        /// <param name="method">The handler to remove. This must be the same Function instance as the one passed to {</param>
        abstract off: methodName: string * ``method``: (ResizeArray<obj option> -> unit) -> unit
        /// <summary>Registers a handler that will be invoked when the connection is closed.</summary>
        /// <param name="callback">The handler that will be invoked when the connection is closed. Optionally receives a single argument containing the error that caused the connection to close (if any).</param>
        abstract onclose: callback: (Error -> unit) -> unit
        abstract processIncomingData: data: obj -> unit
        abstract processHandshakeResponse: data: obj -> unit
        abstract resetKeepAliveInterval: unit -> unit
        abstract resetTimeoutPeriod: unit -> unit
        abstract serverTimeout: unit -> unit
        abstract invokeClientMethod: invocationMessage: obj -> unit
        abstract connectionClosed: ?error: obj -> unit
        abstract cleanupPingTimer: unit -> unit
        abstract cleanupTimeout: unit -> unit
        abstract createInvocation: methodName: obj * args: obj * nonblocking: obj -> unit
        abstract createStreamInvocation: methodName: obj * args: obj -> unit
        abstract createCancelInvocation: id: obj -> unit

    /// Represents a connection to a SignalR Hub.
    type [<AllowNullLiteral>] HubConnectionStatic =
        abstract create: connection: IConnection * logger: ILogger * protocol: IHubProtocol -> HubConnection
        [<Emit "new $0($1...)">] abstract Create: unit -> HubConnection

module LongPollingTransport =
    type HttpClient = HttpClient.HttpClient
    type ILogger = ILogger.ILogger
    type ITransport = ITransport.ITransport
    type TransferFormat = ITransport.TransferFormat

    type [<AllowNullLiteral>] IExports =
        abstract LongPollingTransport: LongPollingTransportStatic

    type [<AllowNullLiteral>] LongPollingTransport =
        inherit ITransport
        abstract httpClient: obj
        abstract accessTokenFactory: obj
        abstract logger: obj
        abstract logMessageContent: obj
        abstract pollAbort: obj
        abstract url: obj option with get, set
        abstract running: obj with get, set
        abstract receiving: obj option with get, set
        abstract closeError: obj option with get, set
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set
        abstract pollAborted: bool
        abstract connect: url: string * transferFormat: TransferFormat -> Promise<unit>
        abstract getAccessToken: unit -> unit
        abstract updateHeaderToken: request: obj * token: obj -> unit
        abstract poll: url: obj * pollOptions: obj -> unit
        abstract send: data: obj option -> Promise<unit>
        abstract stop: unit -> Promise<unit>
        abstract raiseOnClose: unit -> unit

    type [<AllowNullLiteral>] LongPollingTransportStatic =
        [<Emit "new $0($1...)">] abstract Create: httpClient: HttpClient * accessTokenFactory: (unit -> U2<string, Promise<string>>) option * logger: ILogger * logMessageContent: bool -> LongPollingTransport

module ServerSentEventsTransport =
    type HttpClient = HttpClient.HttpClient
    type ILogger = ILogger.ILogger
    type ITransport = ITransport.ITransport
    type TransferFormat = ITransport.TransferFormat
    type EventSourceConstructor = Polyfills.EventSourceConstructor

    type [<AllowNullLiteral>] IExports =
        abstract ServerSentEventsTransport: ServerSentEventsTransportStatic

    type [<AllowNullLiteral>] ServerSentEventsTransport =
        inherit ITransport
        abstract httpClient: obj
        abstract accessTokenFactory: obj
        abstract logger: obj
        abstract logMessageContent: obj
        abstract eventSourceConstructor: obj
        abstract eventSource: obj option with get, set
        abstract url: obj option with get, set
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set
        abstract connect: url: string * transferFormat: TransferFormat -> Promise<unit>
        abstract send: data: obj option -> Promise<unit>
        abstract stop: unit -> Promise<unit>
        abstract close: ?e: obj -> unit

    type [<AllowNullLiteral>] ServerSentEventsTransportStatic =
        [<Emit "new $0($1...)">] abstract Create: httpClient: HttpClient * accessTokenFactory: (unit -> U2<string, Promise<string>>) option * logger: ILogger * logMessageContent: bool * eventSourceConstructor: EventSourceConstructor -> ServerSentEventsTransport

module WebSocketTransport =
    type ILogger = ILogger.ILogger
    type ITransport = ITransport.ITransport
    type TransferFormat = ITransport.TransferFormat
    type WebSocketConstructor = Polyfills.WebSocketConstructor

    type [<AllowNullLiteral>] IExports =
        abstract WebSocketTransport: WebSocketTransportStatic

    type [<AllowNullLiteral>] WebSocketTransport =
        inherit ITransport
        abstract logger: obj
        abstract accessTokenFactory: obj
        abstract logMessageContent: obj
        abstract webSocketConstructor: obj
        abstract webSocket: obj option with get, set
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set
        abstract connect: url: string * transferFormat: TransferFormat -> Promise<unit>
        abstract send: data: obj option -> Promise<unit>
        abstract stop: unit -> Promise<unit>
        abstract close: ?``event``: obj -> unit

    type [<AllowNullLiteral>] WebSocketTransportStatic =
        [<Emit "new $0($1...)">] abstract Create: accessTokenFactory: (unit -> U2<string, Promise<string>>) option * logger: ILogger * logMessageContent: bool * webSocketConstructor: WebSocketConstructor -> WebSocketTransport

module HttpConnection =
    type IConnection = IConnection.IConnection
    type IHttpConnectionOptions = IHttpConnectionOptions.IHttpConnectionOptions
    type HttpTransportType = ITransport.HttpTransportType
    type TransferFormat = ITransport.TransferFormat

    type [<AllowNullLiteral>] IExports =
        abstract HttpConnection: HttpConnectionStatic

    type [<AllowNullLiteral>] INegotiateResponse =
        abstract connectionId: string option with get, set
        abstract availableTransports: ResizeArray<IAvailableTransport> option with get, set
        abstract url: string option with get, set
        abstract accessToken: string option with get, set

    type [<AllowNullLiteral>] IAvailableTransport =
        abstract transport: obj with get, set
        abstract transferFormats: Array<obj> with get, set

    type [<AllowNullLiteral>] HttpConnection =
        inherit IConnection
        abstract connectionState: obj with get, set
        abstract baseUrl: obj with get, set
        abstract httpClient: obj
        abstract logger: obj
        abstract options: obj
        abstract transport: obj option with get, set
        abstract startPromise: obj option with get, set
        abstract stopError: obj option with get, set
        abstract accessTokenFactory: obj option with get, set
        abstract features: obj option
        abstract onreceive: (U2<string, ArrayBuffer> -> unit) option with get, set
        abstract onclose: (Error -> unit) option with get, set
        abstract start: unit -> Promise<unit>
        abstract start: transferFormat: TransferFormat -> Promise<unit>
        abstract send: data: U2<string, ArrayBuffer> -> Promise<unit>
        abstract stop: ?error: Error -> Promise<unit>
        abstract startInternal: transferFormat: obj -> unit
        abstract getNegotiationResponse: url: obj -> unit
        abstract createConnectUrl: url: obj * connectionId: obj -> unit
        abstract createTransport: url: obj * requestedTransport: obj * negotiateResponse: obj * requestedTransferFormat: obj -> unit
        abstract constructTransport: transport: obj -> unit
        abstract resolveTransport: endpoint: obj * requestedTransport: obj * requestedTransferFormat: obj -> unit
        abstract isITransport: transport: obj -> unit
        abstract changeState: from: obj * ``to``: obj -> unit
        abstract stopConnection: ?error: obj -> unit
        abstract resolveUrl: url: obj -> unit
        abstract resolveNegotiateUrl: url: obj -> unit

    type [<AllowNullLiteral>] HttpConnectionStatic =
        [<Emit "new $0($1...)">] abstract Create: url: string * ?options: IHttpConnectionOptions -> HttpConnection

module JsonHubProtocol =
    type HubMessage = IHubProtocol.HubMessage
    type IHubProtocol = IHubProtocol.IHubProtocol
    type ILogger = ILogger.ILogger
    type TransferFormat = ITransport.TransferFormat

    type [<AllowNullLiteral>] IExports =
        abstract JsonHubProtocol: JsonHubProtocolStatic

    /// Implements the JSON Hub Protocol.
    type [<AllowNullLiteral>] JsonHubProtocol =
        inherit IHubProtocol
        abstract name: string
        abstract version: float
        abstract transferFormat: TransferFormat
        /// <summary>Creates an array of {@link @aspnet/signalr.HubMessage} objects from the specified serialized representation.</summary>
        /// <param name="input">A string containing the serialized representation.</param>
        /// <param name="logger">A logger that will be used to log messages that occur during parsing.</param>
        abstract parseMessages: input: string * logger: ILogger -> ResizeArray<HubMessage>
        /// <summary>Writes the specified {@link @aspnet/signalr.HubMessage} to a string and returns it.</summary>
        /// <param name="message">The message to write.</param>
        abstract writeMessage: message: HubMessage -> string
        abstract isInvocationMessage: message: obj -> unit
        abstract isStreamItemMessage: message: obj -> unit
        abstract isCompletionMessage: message: obj -> unit
        abstract assertNotEmptyString: value: obj * errorMessage: obj -> unit

    /// Implements the JSON Hub Protocol.
    type [<AllowNullLiteral>] JsonHubProtocolStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> JsonHubProtocol

module HubConnectionBuilder =
    type HubConnection = HubConnection.HubConnection
    type IHttpConnectionOptions = IHttpConnectionOptions.IHttpConnectionOptions
    type IHubProtocol = IHubProtocol.IHubProtocol
    type ILogger = ILogger.ILogger
    type LogLevel = ILogger.LogLevel
    type HttpTransportType = ITransport.HttpTransportType

    type [<AllowNullLiteral>] IExports =
        abstract HubConnectionBuilder: HubConnectionBuilderStatic

    /// A builder for configuring {@link @aspnet/signalr.HubConnection} instances.
    type [<AllowNullLiteral>] HubConnectionBuilder =
        abstract protocol: IHubProtocol option with get, set
        abstract httpConnectionOptions: IHttpConnectionOptions option with get, set
        abstract url: string option with get, set
        abstract logger: ILogger option with get, set
        /// <summary>Configures console logging for the {@link @aspnet/signalr.HubConnection}.</summary>
        /// <param name="logLevel">The minimum level of messages to log. Anything at this level, or a more severe level, will be logged.</param>
        abstract configureLogging: logLevel: LogLevel -> HubConnectionBuilder
        /// <summary>Configures custom logging for the {@link @aspnet/signalr.HubConnection}.</summary>
        /// <param name="logger">An object implementing the {</param>
        abstract configureLogging: logger: ILogger -> HubConnectionBuilder
        /// <summary>Configures custom logging for the {@link @aspnet/signalr.HubConnection}.</summary>
        /// <param name="logging">An object implementing the {</param>
        abstract configureLogging: logging: U2<LogLevel, ILogger> -> HubConnectionBuilder
        /// <summary>Configures the {@link @aspnet/signalr.HubConnection} to use HTTP-based transports to connect to the specified URL.
        ///
        /// The transport will be selected automatically based on what the server and client support.</summary>
        /// <param name="url">The URL the connection will use.</param>
        abstract withUrl: url: string -> HubConnectionBuilder
        /// <summary>Configures the {@link @aspnet/signalr.HubConnection} to use the specified HTTP-based transport to connect to the specified URL.</summary>
        /// <param name="url">The URL the connection will use.</param>
        /// <param name="transportType">The specific transport to use.</param>
        abstract withUrl: url: string * transportType: HttpTransportType -> HubConnectionBuilder
        /// <summary>Configures the {@link @aspnet/signalr.HubConnection} to use HTTP-based transports to connect to the specified URL.</summary>
        /// <param name="url">The URL the connection will use.</param>
        /// <param name="options">An options object used to configure the connection.</param>
        abstract withUrl: url: string * options: IHttpConnectionOptions -> HubConnectionBuilder
        /// <summary>Configures the {@link @aspnet/signalr.HubConnection} to use the specified Hub Protocol.</summary>
        /// <param name="protocol">The {</param>
        abstract withHubProtocol: protocol: IHubProtocol -> HubConnectionBuilder
        /// Creates a {@link @aspnet/signalr.HubConnection} from the configuration options specified in this builder.
        abstract build: unit -> HubConnection

    /// A builder for configuring {@link @aspnet/signalr.HubConnection} instances.
    type [<AllowNullLiteral>] HubConnectionBuilderStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> HubConnectionBuilder

module Index =

    type [<AllowNullLiteral>] IExports =
        abstract VERSION: string
