# Product Web Api

This is an example of a master Product Web Api developed in .NET 8.

## Application Architecture

The Product Web Api is developed using clean architecture. In the `src` directory, the following projects are included:

### Service Layer
- **Byakkoder.Product.Api**: Web Api project that exposes functionality through endpoints.

### Core Layer
- **Byakkoder.Product.Application**: Contains the application's specific business logic using the CQRS pattern.
- **Byakkoder.Product.Domain**: Contains the application's domain.

***Note***: The domain entity includes validations and domain functionality, thus avoiding the [anemic domain model anti-pattern](https://martinfowler.com/bliki/AnemicDomainModel.html).

### Infrastructure Layer
- **Byakkoder.Product.Infrastructure**: Implements all business-external functionalities such as data contexts, data entities, Entity Framework data migrations, repository pattern, utilities for interacting with external Apis, and functionalities that use caching.

### Design Patterns

The Product Web Api implements the following patterns:

- **CQRS (Command Query Responsibility Segregation)**: Allows decoupling of business functionality code.
- **Mediator**: Uses MediatR to promote class decoupling.
- **Repository Pattern**: Enables abstraction of the data layer.
- **AAA Pattern**: "Arrange, Act, and Assert" pattern to facilitate test maintainability.

### Principles and Practices

For the core (application and domain), TDD (Test-Driven Development) has been predominantly used to ensure clean code, leveraging SOLID principles to guarantee quality. Unit tests are located in the `tests` folder and are developed with xUnit following the AAA (Arrange, Act, and Assert) pattern.

For more information on Clean Architecture, visit: [Clean Architecture, Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

For more on Clean Architecture in .NET, see: [.NET Clean Architecture, Jason Taylor](https://jasontaylor.dev/clean-architecture-getting-started/)


## Endpoints

The Product Web Api includes Swagger documentation, allowing you to view contract details and invocation methods by running the main project **Byakkoder.Product.Api**. Below are some special features to consider in the Api's three endpoints:

- **GetById**:

Retrieves a product based on its internal registration ID. Additionally, this endpoint returns the discount percentage obtained from an external Api and the total product value with the corresponding discount percentage applied to the product's base price.

To obtain the product's discount percentage, an external Api containing mock data is called. Therefore, it's recommended to assign a `ProductId` with one of the default values provided by the external discount Api. To view available test data, execute the following endpoint in your browser or a Web Api testing tool like Postman:

   ```
   https://671d221a09103098807c5cf7.mockapi.io/discount/api/v1/discounts
   ```

  The Product API queries the following endpoint to get the product discount based on its `ProductId`:

   ```
   https://671d221a09103098807c5cf7.mockapi.io/discount/api/v1/discounts/{ProductId}
   ```

***Note***: The 'ProductId' is created as a business identifier for the product to decouple its responsibility from the internal registration ID. Therefore, the discount should be invoked using the `ProductId` instead of the `Id`.

***Important***: The URL of the external discount Api is configured in the `appsettings.json` file of the **Byakkoder.Product.Api** project under the setting `DiscountsApiUrl`.

- **Insert**:

Allows you to create a product. Only mandatory product information is included. Refer to the Swagger documentation for details.

- **Update**:

Enables you to update a product. Only mandatory product information is included. Consult the Swagger documentation for details.


## Running the Web Api in a Development Environment

**Prerequisites** to download and use the Api:

- Visual Studio 2022 or later.
- .NET 8
- Microsoft SQL Server (developed and tested with version 2022 RTM Developer Edition). 
- Git

To run the Product Web Api in a development environment, follow these steps:

1. Clone this repository.
2. Open the solution `Byakkoder.Product.sln` in Visual Studio.
3. Build the solution to restore its dependencies (NuGet packages).
4. Add the connection string for the database to be created in the `appsettings.json` file of the `Byakkoder.Product.Api` project under the setting `ConnectionStrings.DefaultConnection`. For example:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=MY_PC;Initial Catalog=ProductManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  }
```

5. Create the products database (Code First) using the following command in a PowerShell terminal, navigating to the directory where the `Byakkoder.Product.Infrastructure.csproj` is located:

```shell
   dotnet ef database update --context ProductManagementContext --startup-project ../Byakkoder.Product.Api
```

6. Set `Byakkoder.Product.Api` as the startup project and run the solution.
7. Swagger documentation will automatically open in a browser for interacting with the Web API.


## Maintenance of Database Entities

If you need to regenerate `EF Migrations` for maintenance and updating entities, run the following command before executing `dotnet ef database update`:

```shell
   dotnet ef migrations add InitialCreate --context ProductManagementContext --startup-project ../Byakkoder.Product.Api --output-dir Data/Migrations
```
