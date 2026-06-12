# Contributing to MyPortfolio

Thank you for considering contributing to MyPortfolio! This document provides guidelines for contributing to the project.

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- Clear title and description
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable
- Environment details (.NET version, OS, browser)

### Suggesting Features

Feature requests are welcome! Please create an issue with:
- Clear description of the feature
- Use cases and benefits
- Any implementation ideas

### Pull Requests

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Make your changes**
   - Follow the existing code style
   - Add tests if applicable
   - Update documentation

4. **Commit your changes**
   ```bash
   git commit -m "Add: brief description"
   ```
   Use prefixes: `Add:`, `Fix:`, `Update:`, `Remove:`

5. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request**
   - Describe what changes you made
   - Reference any related issues
   - Ensure all tests pass

## Code Style

- Use meaningful variable and method names
- Add XML documentation comments for public methods
- Follow C# naming conventions
- Use async/await for database operations
- Keep methods focused and small
- Use `required` keyword for mandatory properties

## Testing

Run tests before submitting:
```bash
dotnet test
```

## Questions?

Feel free to open an issue for any questions or clarifications.

Thank you for your contributions! 🎉
