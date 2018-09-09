### Requirements

Create a git repository (either local or public one on GitHub) that contains a RESTful web-service written in C#. The service should allow users to place new car adverts and view, modify and delete existing car adverts.

Car adverts should have the following fields:
* **id** (_required_): **int** or **guid**, choose whatever is more convenient for you;
* **title** (_required_): **string**, e.g. _"Audi A4 Avant"_;
* **fuel** (_required_): gasoline or diesel, use some type which could be extended in the future by adding additional fuel types;
* **price** (_required_): **integer**;
* **new** (_required_): **boolean**, indicates if car is new or used;
* **mileage** (_only for used cars_): **integer**;
* **first registration** (_only for used cars_): **date** without time.

Service should:
* have functionality to return list of all car adverts;
  * optional sorting by any field specified by query parameter, default sorting - by **id**;
* have functionality to return data for single car advert by id;
* have functionality to add car advert;
* have functionality to modify car advert by id;
* have functionality to delete car advert by id;
* have validation (see required fields and fields only for used cars);
* accept and return data in JSON format, use standard JSON date format for the **first registration** field.

### Additional requirements

* Service should be able to handle CORS requests from any domain.
* Think about test pyramid and write unit-, integration- and acceptance-tests if needed.
* It's not necessary to document your code, but having a readme.md for developers who will potentially use your service would be great.

### Tips, hints & insights

* Feel free to use any C#/.NET version.
* Feel free to make any assumptions as long as you can explain them.
* Think how to use HTTP verbs and construct your HTTP paths to represent different actions (key word - RESTful).
* Commit frequently, small commits will help us to understand how you tackle the problem.

* We're using ASP.NET Web API at AutoScout24, but feel free to use a different framework of your choice.

* Feel free to use any storage as long as we will be able to run it without doing excessive configuration work.
  
* Feel free to ask questions! :)

### Sending us your work

If you used GitHub repository, just send us a link to your repository.

If you used local repository, zip it (folder called ".git" in your working directory, it is hidden usually!) and send it us by email or put it on Dropbox and send us a link. 



### Implementation Details
This RESTful Api requires _**CarContract**_ and _**Car**_ objects to operate. It supports GET, GET (By Id), POST, DELETE, PUT. The rationale behind PUT has choosen over PATCH is that client no need to track the changes as well as the number of fields are very less.

For any developer, ICarsController interface is a starting point.

* DataContract is in a separate project **CarDataContract**.
* Validation logic is in a separate project **CarDataContractValidator**.
* **AutoRepository** is a repository to deal with database concerns.
* Currently it uses in-memory persistence model, see _**InMemoryDataStore**_.
* This solution has unit test and integration test projects under **Tests** folder.
* There is a **Config** folder contains bunch of classes for assisting _**Startup**_ class.
* Serilog is used to log all the requests and it's duration, see _**RequestLogMiddleware**_.
* Global exception handler is a middleware handles all the exceptions, see _**ExceptionMiddleware**_.

### Technology Choices
* **OData** for sorting, pagination, filter. Pagination and filter are also important as the number of records will be very high. Sooner we need to use CreatedDate for sorting.
* **AutoMapper** for mapping objects.
* **Serilog** for logging.
* **XUnit** for unit testing.
* **FluentValidator** for validating user input and giving meaningful error message.

### Assumptions
* Timezone is not considered. There is a risk if the api is operated across the countries.
* We need to track the user information, datetime posted, modified datetime. Currently it is not handled.
* Authentication is not implemented. When it is integrated with UI, we can use any third party **OpenId Connect** implementation such as facebook or google.
* Idempotency is not checked. If the user provides Car registration number, we should be able to track it. Since this Api doesn't consumes a message queue, we don't need right now.
* Throttling is not implemented. It needs to be implemented to prevent from DDOS attack. If the requests are from same client server, we can slow down the response time exponentially.
* Swagger with OData for .Net core is not supported yet. Not implemented yet.
* Logging is implemented in file, we can use ELK stack for production.
* In-Memory data store is used to simulate EntityFramework.
