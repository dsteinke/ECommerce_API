# ECommerce_API

## Technologies
- **.NET 8**
- **Entity Framework Core**
- **SQLite**
- **Microsoft Identity**
- **JWT Authentication**
- **Swagger**
- **AutoMapper**
- **xUnit(with Moq and FluentAssertions)**

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

### **Order Management**
- Checkout Order, Read Orders, and Edit Order Status

### **API Documentation and Testing**
- Unit tests (at the moment just for ProductService)
- Swagger UI
- Postman Collection with Endpoints in Solution

## Authentication
### Test user

**UserId: 1B565FFA-8EE5-4794-B316-F8E268400065**  
"email": "michael@example.com",  
"password": "test.123"  

**UserId: E3BBCFD7-3A5F-4D37-90E5-71A74D37DCF3**  
"email": "sergey@example.com",  
"password": "test.123"  

## Notes
- Postman Collection with Endpoints in Solution (under ECommerce_API)

## To do:
- Complete Unit Tests
- Migration from SQLite to PostgreSQL
- Containerization with Docker  


Created by dsteinke
