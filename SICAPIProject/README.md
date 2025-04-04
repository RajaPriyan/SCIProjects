# Product API

A simple **RESTful API** built using **ASP.NET Core** to manage products. This API supports CRUD operations (Create, Read, Update, Delete), as well as search, filter, and pagination functionality for the product list.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [API Endpoints](#api-endpoints)
- [Testing the API](#testing-the-api)
- [Error Codes](#error-codes)


## Prerequisites

Before running this project, make sure you have the following installed:

- **.NET SDK** (6.0 or later) [Download .NET](https://dotnet.microsoft.com/download)
- A code editor (e.g., [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/))

## Installation


1. **Restore dependencies**:
    ```bash
    dotnet restore
    ```

2. **Run the API**:
    ```bash
    dotnet run
    ```

By default, the API will be accessible at:
- `http://localhost:5129` (HTTP)
- `https://localhost:7266` (HTTPS)

## API Endpoints

### 1. **GET /api/products**
- **Description**: Returns a list of products, with pagination and optional filtering.
- **Query Parameters**:
    - `page` (optional, default: 1): Page number.
    - `size` (optional, default: 10): Number of items per page.
    - `category` (optional): Filter by category.
    - `minPrice` (optional): Filter by minimum price.
    - `maxPrice` (optional): Filter by maximum price.
- **Example Request**:
    ```bash
    GET /api/products?page=1&size=5&category=Electronics&minPrice=20
    ```

### 2. **GET /api/ProductAPI/{id}**
- **Description**: Retrieves a specific product by `ProductID`.
- **Example Request**:
    ```bash
    GET /api/ProductAPI/1
    ```

### 3. **POST /api/products**
- **Description**: Creates a new product.
- **Request Body**:
    ```json
    {
        "ProductID": 4,
        "Name": "Product D",
        "Category": "Books",
        "Quantity": 50,
        "Price": 15.00
    }
    ```
- **Example Request**:
    ```bash
    POST /api/products
    ```

### 4. **PUT /api/ProductAPI/{id}**
- **Description**: Updates an existing product.
- **Request Body**:
    ```json
    {
        "ProductID": 4,
        "Name": "Updated Product D",
        "Category": "Books",
        "Quantity": 45,
        "Price": 18.00
    }
    ```
- **Example Request**:
    ```bash
    PUT /api/ProductAPI/4
    ```

### 5. **DELETE /api/ProductAPI/{id}**
- **Description**: Deletes a specific product by `ProductID`.
- **Example Request**:
    ```bash
    DELETE /api/ProductAPI/4
    ```

## Testing the API

You can use tools like **Postman**, **Insomnia**, or **cURL** to test the API. Here are some example `cURL` commands:

- **Get all products with pagination and filtering**:
    ```bash
    curl -X GET "https://localhost:7266/api/products?page=1&size=5&category=Electronics&minPrice=20"
    ```

- **Get a product by ID**:
    ```bash
    curl -X GET "https://localhost:7266/api/ProductAPI/1"
    ```

- **Create a product**:
    ```bash
    curl -X POST "https://localhost:7266/api/products" -H "Content-Type: application/json" -d '{
        "ProductID": 4,
        "Name": "Product D",
        "Category": "Books",
        "Quantity": 50,
        "Price": 15.00
    }'
    ```

- **Update a product**:
    ```bash
    curl -X PUT "https://localhost:7266/api/ProductAPI/4" -H "Content-Type: application/json" -d '{
        "ProductID": 4,
        "Name": "Updated Product D",
        "Category": "Books",
        "Quantity": 45,
        "Price": 18.00
    }'
    ```

- **Delete a product**:
    ```bash
    curl -X DELETE "https://localhost:7266/api/ProductAPI/4"
    ```

# FindChips Distributor Data Aggregation Tool

This project is a RESTful API that fetches distributor offers for a specific part number from the **FindChips** platform. The API supports searching for parts by part number and returns relevant distributor offers such as distributor name, seller name, MOQ (Minimum Order Quantity), SPQ (Standard Package Quantity), unit price, currency, and offer URL.

## Features

- Search for distributor offers by part number (e.g., `2N222`).
- Fetch up to 5 offers per distributor.
- Retrieve the following data:
  - Distributor Name
  - Seller Name (if available)
  - MOQ (Minimum Order Quantity)
  - SPQ (Standard Package Quantity)
  - Unit Price
  - Currency
  - Offer URL
- Returns appropriate HTTP status codes based on the result.

## Requirements

- **.NET 6 or higher** (This project is built using ASP.NET Core Web API).
- **Visual Studio 2022** or any IDE that supports .NET Core.
- **HttpClient** for making asynchronous HTTP requests.
- **Newtonsoft.Json** for JSON serialization and deserialization.


## Testing the API

You can use tools like **Postman**, **Insomnia**, or **cURL** to test the API. Here are some example `cURL` commands:

- **Get Distributor Offer API**:
    ```bash
    curl -X GET "https://localhost:7266/api/api/FindChipAPI/2n222"
    ```
- 
## Error Codes

The API returns the following HTTP status codes:

- **200 OK**: Request was successful.
- **201 Created**: Resource was created successfully (on POST).
- **204 No Content**: Resource was updated or deleted successfully (on PUT/DELETE).
- **400 Bad Request**: Invalid data or parameters in the request.
- **404 Not Found**: The requested resource (e.g., product) was not found.
- **409 Conflict**: Conflict (e.g., trying to create a product with a duplicate `ProductID`).
- **500 Internal Server Error**: Unexpected error on the server.