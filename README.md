# GameEngine

A dotnet Game Engine implementation

Targetting Native AOT for performance

## Architecture (...)[./Architecture]

## Best Practices / Coding Conventions:
1. Use PascalCase for class names and method names.
1. Use camelCase for variable names and parameters.
1. Ensure all class, interface, and method names are descriptive and meaningful - code should be self-documenting - i.e. it should be easy for someone reading your code to understand the purpose of a class/method without referring to comments or documentation.
1. Use XML documentation comments for all public classes, methods, and properties. This will help generate API documentation and provide IntelliSense support in IDEs.
1. All methods/functions should have a single responsibility and should be kept as small as possible. If a method is doing too much, consider breaking it down into smaller methods.
1. This solution uses xUnit for unit testing. All public methods should have corresponding unit tests to ensure they work as expected. Use descriptive names for test methods to indicate what is being tested and the expected outcome. Integration testing (where external dependencies are mocked - in this case this might be config providers, graphics displays, player inputs) may be added at a later date if it seems sensible to do so.
    - All tests MUST be run and MUST pass before code is merged into the main branch.
        - TODO: I will look to find a way to automate this in the future, but for now it is a manual process.
    - TODO: In future I would like to implement code coverage, which measures how much of your code is covered by tests. This can help identify areas of the code that may need additional testing. Ideally a minimum threshold of 80% code coverage should be aimed for, though since this is being written from scratch, I expect that higher values may be achievable.
        - Before being merged into the main branch, code coverage would need to be at least 80%, and code coverage should not have been reduced by your changes.
1. TODO: This codebase uses CSharpier to ensure consistent code formatting. All code should be formatted using CSharpier before being committed. This can be done by running the following command in the terminal:
    - `dotnet csharpier format .`
1. TODO: This codebase uses StyleCop as a static code analysis tool to enforce coding standards and best practices. All code should be analyzed using StyleCop before being committed. This can be done by running the following command in the terminal:
    - `dotnet stylecop analyze .`

### Before merging code into the main branch...
1. ALL code must be reviewed by at least one other developer. This includes reviewing the code for correctness, readability, and adherence to coding conventions.
1. Ensure that all code is properly formatted and adheres to the coding conventions outlined above. This includes consistent indentation, spacing, and naming conventions.
1. Ensure that all unit tests pass and that code coverage is maintained or improved.
