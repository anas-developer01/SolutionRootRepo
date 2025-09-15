Customer/Lead Image Upload API
Overview
This project is a .NET 8 Web API that allows users to upload, list, and delete images for customer/lead profiles. Each customer/lead can have up to 10 images, which are stored as Base64-encoded strings in the database.
---
Project Structure
solution-root/
│
├── Api/                # ASP.NET Core Web API (controllers, middleware, startup)
├── Application/        # Application logic, DTOs, services, interfaces, exceptions
├── Domain/             # Entity models (CustomerLead, CustomerLeadImage)
├── Infrastructure/     # Data access (DbContext, repositories)
├── README.md           # Project documentation
└── ...                 # Solution and project files

Design & Architecture
•	Clean Architecture: Separation of concerns between API, Application, Domain, and Infrastructure layers.
•	Entity Framework Core: Used for data access and migrations.
•	Global Exception Handling: Custom middleware for consistent error responses.
•	Validation & Limits: Enforces a maximum of 10 images per customer/lead.
•	Base64 Storage: Images are stored as Base64 strings in the database.
---
Main Entities
•	CustomerLead
•	Id (int, PK)
•	Name (string)
•	Images (collection of CustomerLeadImage)
•	CustomerLeadImage
•	Id (int, PK)
•	Base64Data (string, required)
•	CreatedAt (DateTime)
•	CustomerLeadId (int, FK to CustomerLead)
---
API Endpoints
| Method | Endpoint                                 | Description                                 | |--------|------------------------------------------|---------------------------------------------| | GET    | /api/customers/{customerId}/images     | List all images for a customer/lead         | | POST   | /api/customers/{customerId}/images     | Upload one or more images                   | | DELETE | /api/customers/{customerId}/images/{imageId} | Delete an image from a customer/lead  |
---
Flow
1.	Create a Customer/Lead
Insert a record into the CustomerLeads table (manually or via a separate endpoint/tool).
2.	Upload Images
Use the POST endpoint to upload images (multipart/form-data). The API enforces a maximum of 10 images per customer/lead.
3.	List Images
Use the GET endpoint to retrieve all images for a customer/lead.
4.	Delete Image
Use the DELETE endpoint to remove an image by its ID.
---
Setup & Running
Setup & Running
1.	Clone the Repository
git clone https://github.com/anas-developer01/SolutionRootRepo.git
cd SolutionRootRepo
2.	Configure the Database
•	Update the connection string in Api/appsettings.json to point to your SQL Server instance.
3.	Apply Migrations
dotnet ef database update --project Infrastructure --startup-project Api
4.	Run the API
dotnet run --project Api
5.	Open Swagger UI
•	Navigate to https://localhost:7077/swagger in your browser to test the API.
---
Usage Examples
Upload Image (Swagger/Postman/cURL)
•	POST /api/customers/1/images
•	Body: multipart/form-data, key: Files, value: (select image file)
List Images
•	GET /api/customers/1/images
Delete Image
•	DELETE /api/customers/1/images/{imageId}
---
Error Handling
•	Returns 404 Not Found if the customer/lead or image does not exist.
•	Returns 400 Bad Request if the image limit is exceeded or files are invalid.
•	Returns 500 Internal Server Error for unexpected issues (with detailed error in development).
---
Notes
•	Make sure a customer/lead exists before uploading images.
•	The API enforces a strict 10-image limit per customer/lead.
•	Images are stored as Base64 strings for simplicity and demo purposes.
