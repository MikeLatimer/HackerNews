# News Explorer Application

Welcome to the Hacker News application! This project showcases an Angular-based front-end integrated with a C# .NET Core backend API. 
The application provides users with up-to-date news stories, search capabilities, and a streamlined interface for reading the latest articles. 
Here’s a breakdown of the application’s features and the technical approach used to build it.

---

## Project Overview

This project is built to demonstrate my skills in developing a complete web application, combining both front-end and back-end technologies. It includes:

- **Angular Front-End**: A dynamic user interface for displaying, searching, and paginating news stories.
- **.NET Core Back-End API**: A robust API with dependency injection and caching, ensuring quick responses and efficient data management.
- **Automated Testing**: Unit and integration tests for both front and back ends, maintaining code quality and functionality.

---

## Key Features

### Front-End (Angular)
- **Newest Stories List**: Displays a curated list of the latest news articles, complete with titles and direct links. Stories without hyperlinks are handled gracefully.
- **Search Functionality**: An intuitive search bar allowing users to filter stories by keywords.
- **Pagination**: A smooth paging mechanism ensures users view a manageable number of stories per page.
- **Automated Tests**: Comprehensive tests validate UI components and ensure search and pagination work as expected.

### Back-End (C# .NET Core API)
- **Dependency Injection**: Utilizes built-in dependency injection to manage service lifetimes and dependencies, promoting clean, maintainable code.
- **Caching**: Implements caching for storing the newest stories, reducing API calls and enhancing performance.
- **Automated Tests**: Unit tests ensure core functionalities perform correctly, and integration tests validate end-to-end interactions.

---

## Technical Stack

- **Front-End**: Angular, TypeScript, HTML, CSS
- **Back-End**: .NET Core API, C#
- **Testing**: Jasmine, Karma (for Angular), xUnit (for .NET Core)

---

## Installation and Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/NewsExplorer.git
   cd NewsExplorer


2.  **Set Up the Back-End API**:
    Navigate to the API project directory.
    Restore packages and run the API
  
    ```bash
    dotnet restore
    dotnet run


3. **Set Up the Front-End:**:
   Navigate to the Angular project directory.
   Install dependencies and start the Angular app
   ```bash
   npm install
   ng serve


4. **Front End Testing:**:
   Front-End: Run Angular tests using Karma:
   ```bash
   ng test

5. **Back End .NET Core Testing:**:
   ```bash
   dotnet test

