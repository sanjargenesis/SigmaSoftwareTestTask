# SigmaSoftwareTestTask

Sigma API is a .NET Core-based web API for managing candidate data. It supports operations such as adding or updating if current email exists, including validation for email addresses.

## Technologies Used
- .NET 8
- Web Api
- Entity Framework
- AutoMapper
- xUnit (for testing)
- Moq (for mocking in tests)
- PostgreSQL (default database)

## Setup and Installation
1. Clone the repository:
```
git clone https://github.com/sanjargenesis/SigmaSoftwareTestTask.git
```
2. Set up the database:

Ensure you have PostgreSQL installed and running. Create a database for the project.

3. Configure the application:

Update the connection string in appsettings.json to match your PostgreSQL configuration:
```
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=YourDatabase;Username=YourUsername;Password=YourPassword;Include Error Detail=true"
}
```
4. Database Migrations
Run the following commands to apply the database migrations:
```
dotnet ef migrations add Initial
dotnet ef database update
```
## Example Usage
You might also want to add example usage for some endpoints:

### Adding or Updating if a Candidate exists

POST /api/candidates

Content-Type: application/json
```
{
    "firstName": "Sanjar",
    "lastName": "Shavkatkhujaev",
    "email": "sanjar@example.com",
    "phoneNumber": "+123456789",
    "timeIntervalToCall": {
                          "id": 0,
                          "startHour": 23,
                          "endHour": 23
    },
    "linkedInProfileUrl": "https://linkedin.com/in/sanjar",
    "gitHubProfileUrl": "https://github.com/sanjar",
    "comment": "Looking forward to the interview"
}
```
