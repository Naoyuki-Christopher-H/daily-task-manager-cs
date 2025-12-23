# Daily Task Manager - Console Application

## Table of Contents
1. Project Overview
2. Features
3. System Requirements
4. Installation Instructions
5. Folder Structure
6. Quick Start Guide
7. Detailed Usage Instructions
8. Data Storage and Security
9. Building from Source
10. Testing
11. Troubleshooting
12. Contributing
13. License
14. Support and Contact

## 1. Project Overview

The Daily Task Manager is a console-based task management application developed in C# 4.7.2. 
It provides users with a secure, efficient way to manage daily tasks through a command-line 
interface. The application features user authentication with password hashing, comprehensive 
task management capabilities, and persistent data storage using JSON files.

This project serves as a demonstration of professional C# development practices including 
modular architecture, secure authentication, data persistence, and robust error handling.

## 2. Features

### Core Functionality
- **User Authentication**: Secure account creation and login with SHA256 password hashing
- **Task Management**: Full CRUD (Create, Read, Update, Delete) operations for tasks
- **Task Properties**:
  - Title (required)
  - Optional due date with YYYY-MM-DD format
  - Priority levels: Low, Medium, High
  - Completion status tracking
- **Data Persistence**: Automatic JSON file storage that persists between sessions
- **User Interface**: Color-coded console output with intuitive menu navigation

### Advanced Features
- **Date Validation**: Prevents creation of tasks with past due dates
- **Intelligent Sorting**: Tasks sorted by completion status, due date, and priority
- **Search Functionality**: Search tasks by title with case-insensitive matching
- **Specialized Views**:
  - Overdue tasks display with days overdue calculation
  - Today's tasks highlight
- **Statistics Dashboard**: Comprehensive completion statistics and task counts
- **Robust Input Validation**: Validation for all user inputs with helpful error messages
- **Error Recovery**: Graceful handling of file errors and corrupted data

## 3. System Requirements

### Minimum Requirements
- **Operating System**: Windows 7, 8, 10, or 11
- **.NET Framework**: Version 4.7.2 or higher
- **Processor**: 1 GHz or faster processor
- **Memory**: 512 MB RAM minimum
- **Storage**: 10 MB available disk space
- **Display**: Standard Windows Console or compatible terminal

### Verification Commands
To check if your system meets requirements:

1. **Verify .NET Framework Version**:
   ```cmd
   reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release
   ```
   The Release value should be 461808 or higher for .NET Framework 4.7.2.

2. **Check Available Resources**:
   ```cmd
   systeminfo | findstr /C:"Total Physical Memory" /C:"Available Physical Memory"
   ```

## 4. Installation Instructions

### Method 1: Using Git (Recommended for Developers)

1. **Clone the Repository**:
   ```cmd
   git clone https://github.com/Naoyuki-Christopher-H/daily-task-manager-cs.git
   cd daily-task-manager-cs
   ```

2. **Build the Application**:
   ```cmd
   dotnet build
   ```

3. **Run the Application**:
   ```cmd
   dotnet run
   ```

### Method 2: Pre-compiled Binary

1. **Download the Release**:
   - Visit the GitHub releases page
   - Download the latest `daily-task-manager-cs.zip` file

2. **Extract the Files**:
   ```cmd
   Expand-Archive -Path daily-task-manager-cs.zip -DestinationPath C:\DailyTaskManager
   ```

3. **Run the Application**:
   ```cmd
   cd C:\DailyTaskManager
   daily-task-manager-cs.exe
   ```

### Method 3: Manual Installation

1. **Create Application Directory**:
   ```cmd
   mkdir C:\Programs\DailyTaskManager
   ```

2. **Copy Required Files**:
   - Copy all files from the project to the created directory
   - Ensure `daily-task-manager-cs.exe` is in the directory

3. **Add to PATH (Optional)**:
   ```cmd
   setx PATH "%PATH%;C:\Programs\DailyTaskManager"
   ```

4. **Run from Anywhere**:
   ```cmd
   daily-task-manager-cs
   ```

### Post-Installation Verification

1. **First Run Test**:
   ```cmd
   cd "C:\Programs\DailyTaskManager"
   daily-task-manager-cs.exe
   ```

2. **Verify ASCII Art Display**:
   - On first run, you should see ASCII art welcome message
   - This confirms the application is running correctly

3. **Create Test Account**:
   - Follow the prompts to create a test account
   - Verify you can log in successfully

## 5. Folder Structure

### Complete Project Structure
```
daily-task-manager-cs/
├── daily-task-manager-cs.csproj    # Project configuration file
├── Program.cs                      # Main application entry point
├── Models/                         # Data model classes
│   ├── User.cs                     # User account model
│   ├── TaskItem.cs                 # Task item model
│   ├── Priority.cs                 # Priority enumeration
│   └── AppData.cs                  # Application data container
├── Managers/                       # Business logic classes
│   ├── TaskManager.cs              # Task management operations
│   ├── AuthenticationManager.cs    # User authentication
│   └── JsonDataManager.cs          # JSON data persistence
├── Utils/                          # Utility classes
│   ├── ConsoleHelper.cs            # Console I/O utilities
│   └── PasswordHasher.cs           # Password security utilities
├── Documentation/                  # Project documentation
│   ├── Documentation.md            # Comprehensive documentation
│   └── Project_Plan.md             # Project planning document
├── tasks.json                      # Data storage file (created on first run)
├── README.md                       # This file
└── bin/                            # Compiled binaries (after build)
    ├── Debug/
    │   └── net472/
    │       ├── daily-task-manager-cs.exe
    │       └── tasks.json
    └── Release/
        └── net472/
            ├── daily-task-manager-cs.exe
            └── tasks.json
```

### Key Files Description
- **Program.cs**: Orchestrates the application flow and manages the main loop
- **Models/**: Contains all data structure definitions
- **Managers/**: Implements business logic and operations
- **Utils/**: Provides helper functions and utilities
- **tasks.json**: Stores all application data (users, tasks, passwords)
- **bin/**: Contains compiled executables for different configurations

## 6. Quick Start Guide

### First-Time Setup
1. **Launch the Application**:
   ```cmd
   .\daily-task-manager-cs.exe
   ```

2. **Create Your Account**:
   - Select option 1: "Create new account"
   - Enter a unique username
   - Create and confirm a secure password

3. **Add Your First Task**:
   - After login, select option 1: "Add a new task"
   - Enter task title (e.g., "Complete project documentation")
   - Enter due date (YYYY-MM-DD format) or press Enter to skip
   - Select priority level (1-High, 2-Medium, 3-Low)

4. **View Your Tasks**:
   - Select option 4: "List all tasks"
   - View your task with color-coded priority and status

### Daily Usage Example
```cmd
# Start the application
daily-task-manager-cs

# Login with your credentials
Username: johndoe
Password: ********

# Add today's important task
1. Select "Add a new task"
2. Enter: "Submit weekly report"
3. Due date: (today's date in YYYY-MM-DD format)
4. Priority: 1 (High)

# View tasks due today
8. Select "View today's tasks"

# Mark a task as complete
3. Select "Mark a task as complete"
   Enter task ID to complete

# Logout when done
9. Select "Logout"
```

## 7. Detailed Usage Instructions

### Authentication Menu
```
===================================
AUTHENTICATION
===================================

1. Create new account
2. Login to existing account
3. Exit application

Enter your choice (1-3):
```

#### Creating an Account
1. Select option 1
2. Enter a unique username (case-sensitive)
3. Enter a secure password (minimum 6 characters recommended)
4. Confirm the password
5. Account is created and you are automatically logged in

#### Logging In
1. Select option 2
2. Enter your username
3. Enter your password
4. If credentials are correct, you proceed to the main menu

### Main Task Menu
```
===================================
DAILY TASK MANAGER
Welcome, [Username]!
Today's Date: 2025-12-23
===================================
1. Add a new task
2. Remove a task
3. Mark a task as complete
4. List all tasks
5. Edit a task
6. Search tasks
7. View overdue tasks
8. View today's tasks
9. Logout
10. Exit application

Enter your choice (1-10):
```

#### Adding a Task (Option 1)
1. **Task Title**: Required descriptive title
2. **Due Date**: Optional, format YYYY-MM-DD (e.g., 2024-12-31)
3. **Priority**: 
   - 1 = High (displayed in red/magenta)
   - 2 = Medium (displayed in cyan, default)
   - 3 = Low (displayed in green)

Example task creation:
```
Enter task title: Complete project documentation
Enter due date (YYYY-MM-DD) OR press Enter to skip: 2026-01-20
Select priority level:
1. High
2. Medium (default)
3. Low
Enter choice (1-3): 1
```

#### Listing Tasks (Option 4)
Tasks are displayed with the following color coding:
- **Red**: Overdue tasks (due before today)
- **Yellow**: Tasks due today
- **Magenta**: High priority tasks
- **Cyan**: Medium priority tasks
- **Green**: Low priority tasks
- **Dark Gray**: Completed tasks

Sorting order:
1. Incomplete tasks first
2. By due date (closest first)
3. By priority (highest first)

#### Marking Tasks Complete (Option 3)
1. View the list of tasks with their IDs
2. Enter the ID of the task to mark as complete
3. Task status updates immediately and saves to file

#### Removing Tasks (Option 2)
1. View the list of tasks with their IDs
2. Enter the ID of the task to remove
3. Task is permanently deleted (action cannot be undone)

#### Editing Tasks (Option 5)
1. View the list of tasks with their IDs
2. Enter the ID of the task to edit
3. Update individual fields:
   - Title: Enter new title or press Enter to keep current
   - Due Date: Enter new date or press Enter to clear
   - Priority: Select new priority or press Enter to keep current

#### Searching Tasks (Option 6)
1. Enter search term (case-insensitive)
2. View all tasks containing the search term in their title
3. Results maintain the same color coding as main list

#### Special Views
- **Overdue Tasks (Option 7)**: Shows all tasks due before today that are not completed
- **Today's Tasks (Option 8)**: Shows all tasks due today that are not completed

## 8. Data Storage and Security

### Data File Location
The application stores all data in `tasks.json` in the same directory as the executable.

### JSON Structure
```json
{
  "users": [
    {
      "username": "johndoe",
      "passwordHash": "BASE64_ENCODED_SALTED_HASH",
      "tasks": [
        {
          "id": 1,
          "title": "Complete project",
          "dueDate": "2024-01-20",
          "priority": "High",
          "isComplete": false
        }
      ]
    }
  ]
}
```

### Security Implementation
1. **Password Hashing**: Uses SHA256 with 10,000 iterations
2. **Salt Generation**: Unique 16-byte salt for each password
3. **Storage**: Combined salt and hash stored as base64 string
4. **Verification**: Constant-time comparison to prevent timing attacks

### Backup Recommendations
1. **Manual Backup**:
   ```cmd
   copy tasks.json tasks_backup_$(date +%Y%m%d).json
   ```

2. **Regular Backups**: Recommended weekly or before major changes

3. **Restore Procedure**:
   ```cmd
   copy tasks_backup_20240115.json tasks.json
   ```

## 9. Building from Source

### Prerequisites for Building
- Visual Studio 2019 or later, OR
- .NET SDK for .NET Framework 4.7.2
- Git (for version control)

### Building with Visual Studio
1. **Open the Solution**:
   - Open `daily-task-manager-cs.sln` in Visual Studio
   - Or create new project from existing code

2. **Restore NuGet Packages**:
   - Right-click solution → "Restore NuGet Packages"

3. **Build the Solution**:
   - Press F6 or Build → Build Solution

4. **Locate Output**:
   - Executable will be in `bin\Debug\net472\` or `bin\Release\net472\`

### Building with Command Line
```cmd
# Clone repository
git clone https://github.com/Naoyuki-Christopher-H/daily-task-manager-cs.git
cd daily-task-manager-cs

# Restore dependencies
dotnet restore

# Build in Debug mode
dotnet build -c Debug

# Build in Release mode
dotnet build -c Release

# Run the application
dotnet run
```

### Building for Distribution
```cmd
# Publish self-contained executable
dotnet publish -c Release -r win-x86 --self-contained true

# Output will be in: bin\Release\net472\win-x86\publish\
```

## 10. Testing

### Unit Testing
To test individual components:

1. **Authentication Test**:
   ```cmd
   # Test password hashing
   # Use the PasswordHasher class methods directly
   ```

2. **JSON Serialization Test**:
   ```cmd
   # Create test data, serialize, deserialize, compare
   ```

### Integration Testing
Test scenarios to verify:

1. **Full User Workflow**:
   - Create account → Add tasks → Logout → Login → Verify tasks

2. **Data Persistence**:
   - Add tasks → Close application → Restart → Verify tasks exist

3. **Error Handling**:
   - Test invalid inputs
   - Test file permission errors
   - Test corrupted JSON file

### Manual Testing Checklist
- [ ] First run displays ASCII art
- [ ] New user registration works
- [ ] Login with credentials works
- [ ] Task creation with all fields works
- [ ] Task listing shows proper colors and sorting
- [ ] Task completion marking works
- [ ] Task deletion works
- [ ] Task editing works
- [ ] Search functionality works
- [ ] Overdue tasks view works
- [ ] Today's tasks view works
- [ ] Data persists after restart
- [ ] Error messages are user-friendly

## 11. Troubleshooting

### Common Issues and Solutions

#### Issue: Application Won't Start
**Symptoms**: Nothing happens when clicking executable
**Solutions**:
1. Check .NET Framework installation
   ```cmd
   reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release
   ```
2. Run from command prompt to see error messages
   ```cmd
   cd "C:\Programs\DailyTaskManager"
   daily-task-manager-cs.exe
   ```
3. Check file permissions on executable

#### Issue: Login Fails with Correct Credentials
**Symptoms**: "Invalid username or password" error
**Solutions**:
1. Verify username is case-sensitive
2. Check for accidental spaces in username
3. Reset password by creating new account
4. Check `tasks.json` for corruption

#### Issue: Tasks Not Saving
**Symptoms**: Tasks disappear after restart
**Solutions**:
1. Check write permissions in application directory
2. Verify `tasks.json` file exists
3. Check disk space availability
4. Look for error messages during save operations

#### Issue: Colors Not Displaying
**Symptoms**: All text appears in single color
**Solutions**:
1. Use standard Windows Command Prompt
2. Check terminal color support
3. Colors are enhancement; application works without them

### Diagnostic Commands
```cmd
# Check application directory permissions
icacls .

# Check disk space
wmic logicaldisk get size,freespace,caption

# Check running processes
tasklist | findstr "daily-task"

# Clear corrupted data file (last resort)
del tasks.json
```

### Recovery Procedures

#### Data Recovery from Backup
1. Locate backup file (e.g., `tasks_backup_20240115.json`)
2. Stop the application if running
3. Rename corrupted file:
   ```cmd
   ren tasks.json tasks_corrupted.json
   ```
4. Copy backup:
   ```cmd
   copy tasks_backup_20240115.json tasks.json
   ```
5. Restart application

#### Complete Reset
If all else fails:
1. Delete `tasks.json`
2. Restart application
3. Create new account
4. Recreate tasks

## 12. Contributing

### Development Setup
1. Fork the repository
2. Clone your fork locally
3. Create a feature branch
4. Make changes and commit
5. Push to your fork
6. Create pull request

### Coding Standards
- Use Allman style (braces on new lines)
- Include XML documentation for public members
- Follow C# naming conventions
- Add comments for complex logic
- Ensure backward compatibility for data format

### Areas for Contribution
1. **Feature Development**:
   - Additional task properties
   - Export functionality
   - Enhanced reporting

2. **Code Improvements**:
   - Performance optimization
   - Refactoring for clarity
   - Additional unit tests

3. **Documentation**:
   - User guides
   - API documentation
   - Tutorials

## 13. License

MIT license

## 14. Support and Contact

### Getting Help
- **GitHub Issues**: Report bugs or request features
- **Documentation**: Refer to Documentation.md for detailed information
- **Email**: Contact repository owner for direct support

### Repository Information
- **GitHub**: https://github.com/Naoyuki-Christopher-H/daily-task-manager-cs
- **Primary Language**: C# 4.7.2
- **Project Type**: Console Application
- **Status**: Actively maintained

### Reporting Issues
When reporting issues, include:
1. Application version
2. Operating system and .NET Framework version
3. Steps to reproduce the issue
4. Expected vs actual behavior
5. Error messages (if any)

### Feature Requests
Submit feature requests through GitHub Issues with:
1. Description of requested feature
2. Use case and benefits
3. Proposed implementation approach
4. Any relevant references or examples

---
