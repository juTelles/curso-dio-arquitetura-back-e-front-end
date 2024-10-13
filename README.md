This project was developed as part of a .NET course focused on integration tests. The goal of the project was to demonstrate the correct implementation of integration tests for all key features of a student registration system using the xUnit framework.

During the course, we followed along with the instructor to build tests for a small application designed to register students and their courses in a SQL database, as well as handle authenticated logins. The tests covered student registration in the database, student login, course registration, and the retrieval of registered courses.
 
The tests covered:

Student registration
Student login
Course registration
Retrieval of registered courses

Initially, the tests were performed with the application connected to a local SQL database. To simulate a production environment and ensure the application’s portability, the final tests were executed in a Docker container with a SQL image, ensuring the app could run in varied environments without retaining test data.

Technologies and techniques used:

xUnit: Primary testing framework.
IAsyncLifetime: Controlled the test execution order.
IClassFixture: Managed shared test state across different test runs.
ITestOutputHelper: Enhanced log readability for better debugging.
Autobogus: Automatically generated mock data for testing.
