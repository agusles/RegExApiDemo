# RegExApiDemo
A simple API for matching regular expressions

Gets a regular expression and matches entries against the pattern (api/RegEx/). 
Endpoint is configurable on appsettings.json:

 "JsonEndpoint": {
    "Url": "https://jsonplaceholder.typicode.com/posts"
  }

The response data contains information on how many records matches the expression, the percentage, time elapsed (in millisecconds) and a list of the matching results along with the index of the first occurence of the pattern.
```
{
  "regExQuery": "occa",
  "count": 3,
  "total": 100,
  "percentage": 3,
  "timeElapsed": 699.5331,
  "entries": [
    {
      "expressionIndex": 35,
      "id": 1,
      "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit"
    },
    {
      "expressionIndex": 11,
      "id": 4,
      "title": "eum et est occaecati"
    },
    {
      "expressionIndex": 37,
      "id": 84,
      "title": "optio ipsam molestias necessitatibus occaecati facilis veritatis dolores aut"
    }
  ]
}
```
## Notes
- When using complex regular expressions with backslashes or not supported HTML charactes, HTML encoding should be used. As an example, to find the words with 13 or more characters using this expression: "\b\w{13,}\b", the endpoint should be invoked using the following formatted parameter: 
    https://localhost:44380/api/RegEx/%5Cb%5Cw%7B13%2C%7D%5Cb
- a Dockerfile is provided to easily run the containerized application
- Unit Tests have been added for testing the RegExService and the RegExHandler components
- Elapsed time of querying the json endpoint and matching the regular expression is logged on the console as well as informed on the result object

## Project Structure

Project is built according to Clean Architecture principles:

**Domain layer** 
contains business entities and business rules
- Uses Mediator pattern (through MediatR library)
- Has no dependencies on other components (dependency inversion)
- Use cases included on the Features folder: RegExHandler

**Infrastructure**
contains service implementations:
- JsonPlaceholderClient - simple http client that retrieves data from the endpoint
- RegExService - simple service that matches a regular expression pattern against a collection of entries

**UI layer**
Contains the external interface with the application. In this scenario it's just an API
- Single API endpoint for querying regular expressions
- Services are configured and injected using Extensions (AddServicesExtensions)
- A aimple middleware exception handling layer has been added for generic handling of exceptions
