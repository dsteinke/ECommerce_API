# ECommerce_API

## Technologies
- **.NET 8**
- **Entity Framework Core**
- **SQLite**
- **Microsoft Identity**
- **JWT Authentication**
- **Swagger**
- **AutoMapper**

## Architecture
- Repository Pattern with Clean Architecture
- Dependency Injection

## Features

### **User Management**
- Registration and Login with Microsoft Identity
- JWT Authentication with Access Token
- Refresh Token stored in Http-only Cookie
- Logout Mechanism

### **Product Management**
- CRUD Operations for Products

### **Cart Management**
- Add, Remove, and Edit Products in the Cart

### **API Documentation and Testing**
- Swagger UI
- Postman Collection with Endpoints in Solution

## Authentication
### Test user

UserId: 1B565FFA-8EE5-4794-B316-F8E268400065  
"email": "michael@example.com",  
"password": "test.123"  

UserId: E3BBCFD7-3A5F-4D37-90E5-71A74D37DCF3  
"email": "sergey@example.com",  
"password": "test.123"  

## Notes
- Postman Collection with Endpoints in Solution
- ecommerce_demo.db file is currently in ECommerce_API, but should be ECommerce_API.Infrastructure (with SQLite you cannot simply move the local db-file)

## To do:
- Order Function
- Unit Tests
- Migration from SQLite to PostgreSQL
- Containerization with Docker


Created by dsteinke
