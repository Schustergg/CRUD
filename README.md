# CRUD API

## Overview
The **CRUD API** is a robust, scalable, and modular application designed with best practices in mind. It provides a clean and maintainable architecture following **Clean Code** principles and **SOLID** design patterns. The API is built to handle complex business logic while ensuring high performance and reliability.
---

## Features
1. **Authentication and Authorization**:
   - Secured endpoints using `Authorize` attributes.
   - Protects resources and ensures only authenticated users can access them.

2. **API Versioning**:
   - Supports versioning through URL paths (e.g., `api/v1/products`).

3. **Distributed Caching**:
   - Uses Redis to cache frequently accessed data, reducing database load and improving response times.

4. **Clean Code and SOLID Principles**:
   - Follows best practices for maintainable and readable code.
   - Implements **Dependency Injection** and **Separation of Concerns**.

5. **Error Handling**:
   - Provides meaningful error responses.
   - Validates input and handles exceptions gracefully.

6. **Scalable and Modular Architecture**:
   - Built with a layered architecture to separate concerns.
   - Easily extendable for new features or modules.
     
7. **Unit Testing**:
   - All endpoints are covered with automated unit tests using xUnit and Moq, ensuring full test coverage, isolated dependencies, and validation of both successful and error scenarios.
---

## API Endpoints

### Products Controller
The `ProductsController` provides CRUD operations for managing products.

## **1. Get All Products (Paged)**
- **Endpoint**: `GET /api/v1/products`
- **Description**: Retrieves a paginated list of products.
- **Query Parameters**:
  - `PageNumber` (int): The page number to retrieve.
  - `PageSize` (int): The number of items per page.
- **Response**:
  ```json
  {
    "items": [
      {
        "id": "guid",
        "name": "string",
        "price": "decimal",
      }
    ],
    "totalCount": 100,
    "pageNumber": 1,
    "pageSize": 10
  }

## 2. Get Product by ID
**Endpoint**: `GET /api/v1/products/{id}`  
**Description**: Retrieves a product by its unique identifier.  
**Path Parameters**:  
- `id` (GUID): The unique identifier of the product.  
- **Response**:  
    ```json
     {
       "id": "guid",
       "name": "string",
       "price": "decimal",
     }

## 3. Add a New Product
**Endpoint**: `POST /api/v1/products`  
**Description**: Adds a new product to the system.  
**Request Body**:  
**Response**:  
    ```json
     {
       "id": "guid",
       "name": "string",
       "price": "decimal",
     }
---

## 4. Update a Product
**Endpoint**: `PUT /api/v1/products/{id}`  
**Description**: Updates an existing product.  
**Path Parameters**:  
- `id` (GUID): The unique identifier of the product.  
**Request Body**:  
**Response**:  
    ```json
     {
       "id": "guid",
       "name": "string",
       "price": "decimal",
     }
---

## 5. Delete a Product
**Endpoint**: `DELETE /api/v1/products/{id}`  
**Description**: Deletes a product by its unique identifier.  
**Path Parameters**:  
- `id` (GUID): The unique identifier of the product.  
**Response**:  
- `204 No Content`  

---

### Technologies Used
- **.NET 8**: Framework for building the API.
- **Redis**: Distributed caching system.
- **AutoMapper**: For object-to-object mapping.
