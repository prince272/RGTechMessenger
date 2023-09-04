# RGTechMessenger - ASP.NET API & Next.js 🚀

Welcome to RGTechMessenger, a starter template that combines an ASP.NET API backend with a Next.js frontend. This template provides a foundation for building modern web applications with a powerful backend and a dynamic frontend. Below you'll find details about the template's structure and its key components.

## Table of Contents 📑

- [RGTechMessenger - ASP.NET API \& Next.js 🚀](#rgtechmessenger---aspnet-api--nextjs-)
  - [Table of Contents 📑](#table-of-contents-)
  - [Introduction 🌟](#introduction-)
  - [Getting Started 🚀](#getting-started-)
    - [Prerequisites 🛠️](#prerequisites-️)
  - [Backend Installation ⚙️](#backend-installation-️)
  - [Frontend Installation ⚙️](#frontend-installation-️)
  - [Backend Setup 🔧](#backend-setup-)
    - [Configuration ⚙️](#configuration-️)
    - [Authentication 🔑](#authentication-)
    - [Database 🗄️](#database-️)
  - [Frontend Setup 🔨](#frontend-setup-)
    - [Scripts 📜](#scripts-)
    - [Dependencies 📦](#dependencies-)
    - [Dev Dependencies 🔧](#dev-dependencies-)
  - [Deployment 🚀](#deployment-)

## Introduction 🌟

RGTechMessenger is a template that brings together an ASP.NET API backend and a Next.js frontend. This combination offers the benefits of a robust backend with ASP.NET and a responsive, interactive frontend using Next.js.

## Getting Started 🚀

### Prerequisites 🛠️

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/)
- [npm](https://www.npmjs.com/get-npm)

## Backend Installation ⚙️

1. Clone this repository: `git clone https://github.com/prince272/rgtechmessenger.git`
2. Navigate to the backend directory: `cd RGTechMessenger.WebApi`
3. Install backend dependencies: `dotnet restore`
4. Build and run the backend application: `dotnet run`

## Frontend Installation ⚙️

1. Return to the main directory: `cd rgtechmessenger`
2. Navigate to the frontend directory: `cd RGTechMessenger.WebApp`
3. Install frontend dependencies: `npm install`

## Backend Setup 🔧

The backend is organized into different projects for core functionality and infrastructure.

### Configuration ⚙️

The backend's configuration is managed through the `appsettings.json` file. Update settings such as database connection strings, authentication providers, and email settings based on your project's requirements.

### Authentication 🔑

The template supports user authentication and authorization using ASP.NET Identity. You can configure authentication settings in the `Startup.cs` file.

### Database 🗄️

The template uses Entity Framework Core for database operations. Database context is configured in the `AppDbContext.cs` file, and migrations are managed using Entity Framework's tools.

## Frontend Setup 🔨

The frontend is built with Next.js, providing a fast and dynamic user experience.

### Scripts 📜

In the `RGTechMessenger.WebApp` directory, you can use the following npm scripts:

- `dev`: Start the development server
- `dev:ssl`: Start the development server with SSL (useful for testing)
- `start`: Start the production server
- `lint`: Run ESLint
- `format:check`: Check code formatting using Prettier
- `format`: Format code using Prettier

### Dependencies 📦

The frontend relies on several dependencies, including:

- Next.js
- React
- Axios
- React Hook Form
- Tailwind CSS
- FilePond
- nextui
- libphonenumber-js
- lodash
- ....

### Dev Dependencies 🔧

Dev dependencies for the frontend include tools like Prettier, ESLint, and others. Refer to the `package.json` file for the complete list.

## Deployment 🚀

For hosting my ASP.NET API backend, I have chosen [SmartASP](https://www.smartasp.net/). SmartASP offers a reliable and scalable hosting platform that perfectly suits the needs of my ASP.NET application. With their robust infrastructure and easy-to-use deployment tools, I have ensured that my backend is running smoothly and efficiently.

Similarly, for hosting my Next.js frontend, I have opted for [Vercel](https://vercel.com/). Vercel provides an exceptional hosting experience for Next.js applications, offering automatic deployments directly from my GitHub repository. By connecting my repository to Vercel, I have streamlined the deployment process and can instantly publish updates to my frontend.

Both SmartASP and Vercel have been integral to the success of my RGTechMessenger project. Their hosting solutions have allowed me to focus on building and improving my application, knowing that my backend and frontend are in capable hands.
