# Country Service. Async I/O bound operations

A advanced level task for practicing asynchronous programming for I/O bound operations.

In this task you 
- will learn the basics of async programming;
- will get acquainted with [Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-6.0) and [Task<TResult>](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1?view=net-6.0) classes from [System.Threading.Tasks](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks?view=net-6.0) namespace;
- will learn how to use `async/await` key words in asynchronous programming. 

Before starting the task learn how to use `Task`/`Task<TResult>` classes and `async/await` key words in C#.

Estimated time to complete the task: 6h.

## Task Description

 - Review the articles
    - [Asynchronous programming with async and await](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/).
    - [Your Ultimate async / await Tutorial in C#](https://www.codingame.com/playgrounds/4240/your-ultimate-async-await-tutorial-in-c/introduction). 
    - [Async and Await](https://blog.stephencleary.com/2012/02/async-and-await.html).
    - [WebClient Class](https://docs.microsoft.com/en-us/dotnet/api/system.net.webclient?view=net-6.0).
    - [HttpClient Class](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0).
    - [JsonSerializer Class](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer?view=net-6.0).
 - Learn how the [REST COUNTRIES 🇵🇪](https://restcountries.com/#api-endpoints-v2) RESTful service works.
 - Implement the `CountryService` class whose methods return data as a result of requests to the following endpoints [API ENDPOINTS V2](https://restcountries.com/#api-endpoints-v2) of the RESTful service.
 - Compare synchronous and asynchronous implementations of the methods. Make inferences about usage preference.
