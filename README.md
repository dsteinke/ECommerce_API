# ECommerce API

## Technologies
- **.NET 8 (ASP.NET Core Web API)**
- **Entity Framework Core (Code-First)**
- **PostgreSQL (earlier SQLite)**
- **Microsoft Identity**
- **JWT Authentication**
- **Swagger**
- **AutoMapper**
- **xUnit(with Moq and FluentAssertions)**
- **Containerization with Docker**

## Architecture
- Repository Pattern with Clean Architecture
- Dependency Injection

## Deployment
API, Database and nginx reverse proxy are running in docker containers on a private Raspberry PI 5 (running on an Ubuntu Server) which i set up.

## Features

### **User Management**
- Registration and Login with Microsoft Identity
- JWT Authentication with Access Token
- Refresh Token stored in Http-only Cookie

### **Product Management**
- CRUD Operations for Products

### **Cart Management**
- Add, Remove, and Edit Products in the Cart

### **Order Management**
- Checkout Order, Read Orders, and Edit Order Status

### **API Documentation and Testing**
- Unit tests (at the moment just for ProductService)
- Swagger UI
- Postman Collection with Endpoints in Solution

## Authentication
### Test user
"email": "michael@example.com",  
"password": "test.123"  

## Setup
git clone https://github.com/dsteinke/ECommerce_API.git  
cd ECommerce_API  
docker compose up --build  

Swagger: https://localhost:3001/swagger/index.html

## Notes
- Postman Collection with Endpoints in Solution (under ECommerce_API)

## To do:
- Complete Unit Tests
- Setup CI/CD pipeline
- Implementing MediatR

Created by dsteinke
