# Tel Aviv Municipality Assessment - Rate Limiting App

This repository contains the full-stack solution for the Tel Aviv Municipality development assignment. The project demonstrates client-server communication, form validation, and a custom server-side rate-limiting mechanism with state recovery.

## Architecture & Tech Stack

The solution is divided into two main applications:

* **Frontend:** Angular 19+ (Standalone Components, SCSS, Reactive Forms, RxJS).
* **Backend:** .NET Web API (C#, `IMemoryCache`, RESTful architecture).

## The Challenge & Solution (Handling HTTP 429)

The core requirement of this task was to handle a specific rate-limiting scenario: if a user submits more than one request within a 3-second window, the server must reject the new request (HTTP 429 Too Many Requests) but **return the data of the last valid request**.

### How it works:
1.  **Backend (In-Memory Cache):** The .NET API utilizes `IMemoryCache`. When a valid request arrives, the server processes it and stores the result in the cache with a strict 3-second expiration policy. If a subsequent request arrives, the API intercepts it, stop the normal flow, and returns a `429 StatusCode` containing the cached data from the previous valid request.
2.  **Frontend (Global Interceptor):** Instead of handling the 429 error locally within the component, the Angular application uses a **Functional HttpInterceptor**. This intercepts the `HttpErrorResponse` globally, extracts the historical payload from the error body, and throws it down the RxJS pipeline to gracefully update the UI without crashing the application.


## Development server
To start a local development server, run:

```bash
dotnet run
```
