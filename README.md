# .NET Web App with Keycloak Authentication 

This project is a simple .NET Web Application integrated with Keycloak for authentication and authorization.
The application allows users to manage user data (view, add, edit, delete) after logging in via Keycloak.

# Technologies Used
- .NET Web Application: The web application is built using .NET.
- Keycloak: Used for user authentication and authorization.
- PostgreSQL: Database to store keycloak data.
- Docker Compose: For defining and running multi-container Docker applications, with services for the app, Keycloak, and PostgreSQL.

# Features
Keycloak Authentication: User authentication is handled by Keycloak.
User Management: After logging in, users can view, add, edit, and delete user information.
Dockerized Services: The app, Keycloak, and PostgreSQL are all containerized using Docker and orchestrated with Docker Compose.
