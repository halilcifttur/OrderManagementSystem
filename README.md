# Order Management System

## Introduction

The **Order Management System** is a backend service designed for an e-commerce platform. It allows users to:

- Create orders via a REST API.
- Manage order processing using RabbitMQ and a worker service.
- Query and list orders.

This system is built using .NET 8, Entity Framework Core, RabbitMQ, and SQLite.

## Features

- **REST API Endpoints**:
  - `POST /api/orders`: Create a new order and enqueue it to RabbitMQ.
  - `GET /api/orders`: Retrieve a list of all orders.
  - `GET /api/orders/{id}`: Get details of a specific order by ID.
- **Order Processing**:
  - Orders are processed by a background worker service and RabbitMQ.
  - After creating an order, the handler publishes an event to the RabbitMQ queue, and the consumer consumes the event message by updating order statuses from "Pending" to "Processed".
  - The worker works every 1 min and updates order statuses from "Processed" to "Completed".
- **Database**:
  - SQLite is used to store order data.
- **Validation**:
  - FluentValidation ensures input data is valid.

## Technologies Used

- **.NET 8.0**: Core framework for building the application.
- **Entity Framework Core**: ORM for database management.
- **RabbitMQ**: Message broker for order processing.
- **MassTransit**: Abstraction layer for RabbitMQ.
- **SQLite**: Lightweight database for persistent storage.
- **xUnit**: Unit testing framework.
- **Moq**: For mocking dependencies in unit tests.
- **Docker**: For containerized deployment.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [RabbitMQ Management Plugin](http://localhost:15672) (enabled in Docker Compose)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-repo/order-management-system.git
cd order-management-system
```

### 2. Build and Run with Docker Compose

Ensure Docker is installed and running, then execute:

```bash
docker-compose up --build
```

This command will:

- Start RabbitMQ with the management interface at `http://localhost:15672`.
- Start the API service on `http://localhost:5000`.
- Start the Worker service.

### 3. Access the API

- **Swagger UI**: The API documentation and testing interface is available at:
  - `http://localhost:5000/swagger`

### 4. Running Tests

Run the unit tests with:

```bash
dotnet test
```

## Project Structure

```
OrderManagementSystem
├── src
│   ├── OrderManagementSystem.API         # API service
│   ├── OrderManagementSystem.Application # Business logic and CQRS
│   ├── OrderManagementSystem.Domain      # Domain entities and interfaces
│   ├── OrderManagementSystem.Infrastructure # Data and messaging implementations
│   └── OrderManagementSystem.Worker      # Background worker service
├── tests
│   └── OrderManagementSystem.Tests       # Unit tests
├── docker-compose.yml                    # Docker Compose configuration
└── README.md                             # Project documentation
```

## API Endpoints

- **POST /api/orders**:

  - **Description**: Creates a new order.
  - **Request Body**:
    ```json
    {
      "productName": "string",
      "price": decimal
    }
    ```
  - **Response**:
    ```json
    {
      "id": "guid"
    }
    ```

- **GET /api/orders**:

  - **Description**: Retrieves a list of all orders.

- **GET /api/orders/{id}**:

  - **Description**: Retrieves the details of an order by its ID.

