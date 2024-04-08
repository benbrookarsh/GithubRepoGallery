# GitHub Repository Explorer

This project is a demo application designed to query GitHub repositories. It features an Angular 17 client for the frontend and a .NET 6 server for the backend.

## Overview

The GitHub Repository Explorer allows users to search for GitHub repositories using various criteria. The frontend is built with Angular 17, offering a dynamic and responsive user interface. The backend, developed with .NET 6, handles API requests to GitHub and serves the Angular application.

## Getting Started

Follow these steps to get the project running on your local machine for development and testing purposes.

### Prerequisites

Ensure you have the following installed:
- [Node.js](https://nodejs.org/) (which includes npm) for the Angular client.
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) for the .NET server.
- [Angular CLI](https://angular.io/cli) for Angular project commands.

### Installation

1. **Clone the repository**

   ```sh
   git clone https://github.com/benbrookarsh/GithubRepoGallery/tree/master

2. **Install Client dependencies**

Navigate to the Client directory and run:

```sh
npm install
```

3. **Install server dependencies:**

```sh 
dotnet restore
```


## Running the Project

navigate to **Server.API** 

```sh
dotnet run
```

Server app should start running on https://localhost:7168/

navigte to **Client Directory** 

```sh
ng serve
```



Open a web browser and navigate to http://localhost:4200.

Click on register, create an account and start exploring github repositories

**Features**

Search GitHub repositories by name.

Responsive and intuitive user interface.

Authorization with JWT token and dotnet Identity User

SQlite database to store user data and Bookmarks

