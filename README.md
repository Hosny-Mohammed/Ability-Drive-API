# Ability-Drive-API

## Overview
The Ability-Drive-API is a comprehensive API developed in C#. It is designed to manage and facilitate various operations related to transportation services, including user registration, ride booking, voucher management, and more. This project aims to provide a robust and scalable backend solution for transportation applications.

## Key Features
- **User Management**: Register and authenticate users, retrieve user details.
- **Ride Management**: Book rides, manage ride statuses, and assign drivers to rides.
- **Voucher Management**: Validate and apply discount vouchers.
- **Bus Schedule Management**: Manage and book bus seats.
- **Driver Management**: Assign and manage drivers, track driver availability and locations.
- **Asynchronous Operations**: Efficient handling of database operations using asynchronous programming.

## Installation
To set up the project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/Hosny-Mohammed/Ability-Drive-API.git
    ```
2. Navigate to the project directory:
    ```bash
    cd Ability-Drive-API
    ```
3. Open the project in Visual Studio.

4. Restore the NuGet packages:
    ```bash
    dotnet restore
    ```

5. Update the database to apply migrations:
    ```bash
    dotnet ef database update
    ```

6. Run the project:
    ```bash
    dotnet run
    ```

## Usage
### API Endpoints
- **User Registration**: `POST /api/user/register`
- **User Login**: `POST /api/user/login`
- **Get User Details**: `GET /api/user/{userId}`
- **Book Ride**: `POST /api/ride/book`
- **Get Ride Status**: `GET /api/ride/status/{rideId}`
- **Book Bus Seat**: `POST /api/bus/book`
- **Get Bus Schedules**: `GET /api/bus/schedules`

### Example Request
To register a new user, send a POST request to `/api/user/register` with the following JSON body:
```json
{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "password": "password123",
    "phoneNumber": "1234567890"
}
```
## Contributing
Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a pull request.

## License
This project is licensed under the MIT License.

## Author
[Hosny-Mohammed](https://github.com/Hosny-Mohammed)
