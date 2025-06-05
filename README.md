Comfort Hotel API

Comfort Hotel API is a Web API built with ASP.NET Core that provides hotel room booking management functionalities. It allows users to view available rooms, make bookings, and manage customer and booking data.

🛠️ Technologies Used

Language: C#

Framework: ASP.NET Core Web API

Database:  SQL Server

Tools & Libraries:
Entity Framework => Packages:Microsoft.EntityFrameworkCore.Tools, Microsoft.EntityFrameworkCore.SqlServe, Microsoft.EntityFrameworkCore
Jwt and Identity => Packages:Microsoft.AspNetCore.Identity.EntityFrameworkCore, Microsoft.AspNetCore.Authentication.JwtBearer
AutoMapper => Packages:AutoMapper


⚙️ Getting Started

1. Clone the repository

git clone https://github.com/mahamednasser/Comfort_Hotel_Api.git

2. Open the project

Open the Comfort Hotel.sln solution file using Visual Studio.

3. Restore dependencies

Make sure to restore NuGet packages:

dotnet restore

4. Configure the database

Update the appsettings.json file with your database connection string.

Run migrations if needed:

dotnet ef database update


5. Run the application



📌 Features

Hotel room booking management

CRUD operations for rooms and customers and Bookings 

User authentication 

Booking status updates
