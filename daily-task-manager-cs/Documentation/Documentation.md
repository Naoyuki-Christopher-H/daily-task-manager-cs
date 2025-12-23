# Daily Task Manager - Project Documentation

## Table of Contents
1. Introduction
2. System Architecture
3. Installation and Setup
4. User Guide
5. Technical Specifications
6. Data Storage Format
7. Security Implementation
8. Error Handling
9. Testing Procedures
10. Troubleshooting Guide
11. Future Enhancements
12. Conclusion

## 1. Introduction

### 1.1 Project Overview
The Daily Task Manager is a console-based application developed 
in C# 4.7.2 that provides users with a comprehensive tool for 
managing daily tasks. The application enables users to create 
accounts, log in securely, and manage tasks with due dates and 
priority levels. All data persists between sessions using JSON 
file storage, ensuring user information and tasks remain available 
upon application restart.

### 1.2 Objectives
The primary objectives of this project are to demonstrate proficiency 
in C# programming, implement secure authentication mechanisms, develop 
persistent data storage solutions, and create an intuitive user interface 
for task management. The application serves as a practical demonstration 
of software engineering principles including modular design, data persistence, 
security implementation, and user experience design.

### 1.3 Target Audience
This application is designed for individual users who need a simple, efficient 
tool for daily task management. The console-based interface makes it accessible 
for users comfortable with command-line applications. Developers and students 
can also use this project as a learning resource for understanding C# application 
development, JSON serialization, and secure authentication implementation.

## 2. System Architecture

### 2.1 Application Structure
The Daily Task Manager follows a modular architecture with clear separation of concerns. 
The application is organized into three main layers: presentation layer, business logic 
layer, and data access layer. This structure promotes maintainability, testability, and 
scalability.

### 2.2 Class Diagram
The application consists of the following key classes organized in a logical hierarchy:

1. **Program Class**: Serves as the main entry point and orchestrates application flow.

2. **Model Classes** (Models namespace):
   - `User`: Represents a user account with username, hashed password, and task list.
   - `TaskItem`: Represents an individual task with properties for title, due date, priority, and completion status.
   - `Priority`: Enumeration defining task priority levels (Low, Medium, High).
   - `AppData`: Container class for all application data including user collection.

3. **Manager Classes** (Managers namespace):
   - `TaskManager`: Handles all task-related operations including creation, modification, deletion, and display.
   - `AuthenticationManager`: Manages user authentication including registration, login, and password validation.
   - `JsonDataManager`: Handles all JSON serialization and file I/O operations.

4. **Utility Classes** (Utils namespace):
   - `ConsoleHelper`: Provides helper methods for console input/output operations and validation.
   - `PasswordHasher`: Implements secure password hashing and verification using SHA256.

### 2.3 Data Flow
The application follows a unidirectional data flow pattern. User input is captured through 
the console interface, processed by the appropriate manager classes, validated, and then 
persisted to the JSON file. Data flows from the user interface through the business logic 
layer to the data storage layer. When loading data, the flow reverses, with data retrieved 
from storage, processed by business logic, and presented to the user.

## 3. Installation and Setup

### 3.1 Prerequisites
To run the Daily Task Manager application, the following prerequisites must be met:
- Windows operating system (Windows 7 or later)
- .NET Framework 4.7.2 or later installed
- Sufficient disk space for application files and data storage
- Read/write permissions in the application directory

### 3.2 Installation Steps
Follow these steps to set up the Daily Task Manager:

1. **Extract the Application**: Extract the provided ZIP archive to a directory of your choice.

2. **Verify Dependencies**: Ensure .NET Framework 4.7.2 is installed by checking Windows Features or using the command: `reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release`.

3. **Directory Structure**: Verify the following directory structure exists:
   ```
   daily-task-manager-cs/
   ├── daily-task-manager-cs.exe
   ├── tasks.json (will be created on first run)
   └── Documentation/
       ├── Documentation.md
       └── Project_Plan.md
   ```

4. **First Run**: Execute `daily-task-manager-cs.exe` from the command prompt or by double-clicking the executable.

### 3.3 Configuration
The application requires no manual configuration. All settings are managed internally. 
The JSON data file (`tasks.json`) is automatically created in the same directory as the 
executable on first run. Users with specific security requirements should ensure the 
application directory has appropriate file permissions.

## 4. User Guide

### 4.1 Getting Started
Upon first launch, the application displays ASCII art and welcomes new users. 
Since no users exist initially, you must create an account:

1. **Create Account**: Select option 1 from the authentication menu.
2. **Enter Username**: Choose a unique username (case-sensitive).
3. **Enter Password**: Create a secure password.
4. **Confirm Password**: Re-enter the password to confirm.

After successful registration, you are automatically logged in and directed to 
the main task management menu.

### 4.2 Task Management

#### 4.2.1 Adding Tasks
To add a new task:
1. Select option 1 from the main menu.
2. Enter task title (required).
3. Enter due date in YYYY-MM-DD format or press Enter to skip.
4. Select priority level (1-High, 2-Medium, 3-Low).
5. The task is created with a unique ID and saved automatically.

#### 4.2.2 Viewing Tasks
To view all tasks, select option 4 from the main menu. Tasks are displayed with color coding:
- **Red**: Overdue tasks (due before today)
- **Yellow**: Tasks due today
- **Magenta**: High priority tasks
- **Cyan**: Medium priority tasks
- **Green**: Low priority tasks
- **Dark Gray**: Completed tasks

Tasks are sorted with incomplete tasks first, then by due date (closest first), 
then by priority (highest first).

#### 4.2.3 Completing Tasks
To mark a task as complete:
1. Select option 3 from the main menu.
2. View the list of tasks with their IDs.
3. Enter the ID of the task to mark as complete.
4. The task status updates and saves automatically.

#### 4.2.4 Removing Tasks
To remove a task:
1. Select option 2 from the main menu.
2. View the list of tasks with their IDs.
3. Enter the ID of the task to remove.
4. The task is permanently deleted and the change saves automatically.

#### 4.2.5 Editing Tasks
To edit an existing task:
1. Select option 5 from the main menu.
2. View the list of tasks with their IDs.
3. Enter the ID of the task to edit.
4. Update individual fields or press Enter to keep current values.
5. Changes save automatically.

#### 4.2.6 Searching Tasks
To search for tasks by title:
1. Select option 6 from the main menu.
2. Enter a search term (case-insensitive).
3. View matching tasks with the same color coding as the main list.

### 4.3 Special Views

#### 4.3.1 Overdue Tasks
Select option 7 to view all tasks that are overdue (due before today and not completed). 
The display shows how many days each task is overdue.

#### 4.3.2 Today's Tasks
Select option 8 to view all tasks due today (current date) that are not completed.

### 4.4 Session Management

#### 4.4.1 Logging Out
Select option 9 from the main menu to log out. You return to the authentication menu 
where you can log in with a different account or create a new account.

#### 4.4.2 Exiting the Application
Select option 10 from the main menu to exit the application. You are prompted for 
confirmation before the application closes.

## 5. Technical Specifications

### 5.1 Development Environment
- **Programming Language**: C# 4.7.2
- **Framework**: .NET Framework 4.7.2
- **IDE**: Visual Studio 2019 or compatible
- **Target Platform**: Windows Console Application
- **Code Style**: Allman style with brackets on new lines

### 5.2 File Specifications
- **Source Code**: Approximately 1,200 lines of C# code
- **Data File**: JSON format with UTF-8 encoding
- **Executable**: Console application compiled for .NET Framework 4.7.2

### 5.3 Memory Requirements
- **Minimum RAM**: 512 MB
- **Disk Space**: 5 MB for application and data
- **Processor**: 1 GHz or faster

### 5.4 Performance Characteristics
- **Startup Time**: Less than 2 seconds on typical hardware
- **Task Operations**: Near-instantaneous for typical task lists
- **Data Persistence**: Automatic save after each modification
- **Memory Usage**: Minimal overhead for typical usage scenarios

## 6. Data Storage Format

### 6.1 JSON Structure
The application stores all data in a single JSON file named `tasks.json`. 
The file uses UTF-8 encoding and follows this structure:

```json
{
  "users": [
    {
      "username": "exampleUser",
      "passwordHash": "base64EncodedHash",
      "tasks": [
        {
          "id": 1,
          "title": "Sample Task",
          "dueDate": "2024-12-31",
          "priority": "High",
          "isComplete": false
        }
      ]
    }
  ]
}
```

### 6.2 Field Descriptions

#### 6.2.1 User Object
- **username**: String representing the user's login name (case-sensitive, unique).
- **passwordHash**: Base64-encoded string containing the salted SHA256 hash of the user's password.
- **tasks**: Array of task objects belonging to the user.

#### 6.2.2 Task Object
- **id**: Integer serving as the unique identifier for the task within the user's task list.
- **title**: String containing the task description or title.
- **dueDate**: String in YYYY-MM-DD format representing the task due date, or null if no due date set.
- **priority**: String representing task priority: "Low", "Medium", or "High".
- **isComplete**: Boolean indicating whether the task is completed (true) or pending (false).

### 6.3 Data Integrity
The application implements several data integrity measures:
- Automatic ID generation ensures unique task identifiers
- Date validation prevents invalid date formats
- Reference integrity maintains user-task relationships
- File locking prevents concurrent access issues

### 6.4 Backup and Recovery
While the application does not include automatic backup features, users can 
manually backup the `tasks.json` file. In case of corruption, removing or 
renaming the file will cause the application to create a new empty data file 
on next launch.

## 7. Security Implementation

### 7.1 Password Security
The application implements industry-standard password security practices:

1. **Hashing Algorithm**: SHA256 with 10,000 iterations
2. **Salt Generation**: 16-byte cryptographically random salt for each password
3. **Storage Format**: Salt and hash combined and base64-encoded
4. **Verification**: Secure comparison using constant-time algorithm

### 7.2 Authentication Flow
The authentication process follows these steps:
1. User provides username and password
2. System retrieves stored hash for the username
3. System extracts salt from stored hash
4. System hashes provided password with extracted salt
5. System compares computed hash with stored hash
6. Access granted only if hashes match exactly

### 7.3 Security Considerations
- No plain-text passwords stored or transmitted
- Protection against timing attacks in password comparison
- Input validation to prevent injection attacks
- File system permissions respected for data file

### 7.4 Limitations
- Single-factor authentication only
- No password strength enforcement
- No account lockout for failed attempts
- Local storage only (no network security considerations)

## 8. Error Handling

### 8.1 User Input Errors
The application handles various user input errors gracefully:
- **Invalid Menu Choices**: Display error message and return to menu
- **Invalid Date Formats**: Prompt for correct format or allow skipping
- **Non-numeric IDs**: Display error and prompt again
- **Empty Required Fields**: Prevent submission until valid input provided

### 8.2 File System Errors
The application includes robust file system error handling:
- **Missing JSON File**: Create new file with empty structure
- **Permission Denied**: Display clear error message and exit gracefully
- **File Corruption**: Attempt recovery or create new file with warning
- **Disk Full**: Display error message before attempting write operations

### 8.3 Data Validation Errors
Data validation occurs at multiple levels:
- **JSON Parsing**: Validate structure and data types
- **Date Validation**: Ensure dates are valid and not in the past
- **User Input**: Validate all user-provided data before processing
- **Data Consistency**: Verify relationships between data elements

### 8.4 Exception Handling Strategy
The application employs a layered exception handling approach:
1. **Low-level Operations**: Try-catch blocks around file I/O operations
2. **Business Logic**: Validation before processing to prevent exceptions
3. **Presentation Layer**: User-friendly error messages for recoverable errors
4. **Application Level**: Graceful shutdown for unrecoverable errors

## 9. Testing Procedures

### 9.1 Unit Testing Areas
The following areas require thorough unit testing:

1. **PasswordHasher**: Verify hashing and verification functions correctly
2. **JsonDataManager**: Test serialization and deserialization of all data types
3. **TaskManager**: Validate all CRUD operations and business logic
4. **ConsoleHelper**: Test input validation and formatting functions

### 9.2 Integration Testing Scenarios
Integration testing should cover these scenarios:

1. **Full Authentication Flow**: Registration, login, and session management
2. **Task Lifecycle**: Create, edit, complete, and delete tasks
3. **Data Persistence**: Save data, restart application, verify data loaded correctly
4. **Error Recovery**: Simulate file errors and verify graceful handling

### 9.3 User Acceptance Testing
Test the following user scenarios:
1. **New User Experience**: First-time setup and account creation
2. **Daily Usage**: Typical task management operations
3. **Edge Cases**: Empty task lists, duplicate usernames, invalid inputs
4. **Long Session**: Extended usage to identify memory or performance issues

### 9.4 Performance Testing
Validate performance under these conditions:
1. **Large Data Sets**: 100+ tasks per user
2. **Multiple Users**: 10+ user accounts
3. **Rapid Operations**: Sequential task operations without delays
4. **Concurrent Access**: Simulated multi-user scenarios

## 10. Troubleshooting Guide

### 10.1 Common Issues

#### 10.1.1 Application Won't Start
- **Symptom**: Nothing happens when executing the application
- **Possible Cause**: Missing .NET Framework 4.7.2
- **Solution**: Install required .NET Framework version
- **Verification**: Run `dotnet --info` to check installed versions

#### 10.1.2 Login Failures
- **Symptom**: Cannot login with correct credentials
- **Possible Cause**: Corrupted user data or incorrect password
- **Solution**: Reset password by creating new account or manual JSON edit
- **Prevention**: Regular backups of tasks.json file

#### 10.1.3 Data Loss
- **Symptom**: Tasks or users missing after restart
- **Possible Cause**: File corruption or permission issues
- **Solution**: Restore from backup or create new data file
- **Prevention**: Ensure proper file permissions and avoid manual editing

#### 10.1.4 Display Issues
- **Symptom**: Colors not displaying correctly
- **Possible Cause**: Console compatibility or terminal settings
- **Solution**: Run in standard Windows Command Prompt
- **Workaround**: Colors are non-essential; application functions without them

### 10.2 Diagnostic Steps
When encountering issues, follow these diagnostic steps:

1. **Check Application Directory**: Verify `tasks.json` exists and is accessible
2. **Review Error Messages**: Note exact error text for reference
3. **Test Basic Functions**: Attempt to create new user and task
4. **Check File Permissions**: Ensure read/write access to application directory
5. **Verify System Requirements**: Confirm .NET Framework 4.7.2 installed

### 10.3 Recovery Procedures

#### 10.3.1 Data Recovery
If data becomes corrupted:
1. Rename `tasks.json` to `tasks.json.backup`
2. Restart application to create new data file
3. Recreate users and tasks as needed
4. Contact support if backup needed from corrupted file

#### 10.3.2 Configuration Recovery
If application configuration issues occur:
1. Delete `tasks.json` to reset to default state
2. Restart application for fresh initialization
3. Recreate user accounts and tasks

## 11. Future Enhancements

### 11.1 Planned Features
The following features are identified for future implementation:

1. **Task Categories**: Organize tasks by category or project
2. **Recurring Tasks**: Support for daily, weekly, or monthly tasks
3. **Export Functionality**: Export tasks to CSV or other formats
4. **Notifications**: System tray notifications for upcoming tasks
5. **Web Interface**: Browser-based access to tasks

### 11.2 Technical Improvements
Potential technical enhancements include:

1. **Database Backend**: Replace JSON file with SQL database
2. **Cloud Sync**: Synchronize tasks across multiple devices
3. **API Access**: REST API for third-party integration
4. **Plugin System**: Extensible architecture for custom features
5. **Performance Optimization**: Improved algorithms for large task lists

### 11.3 User Experience Enhancements
User interface improvements under consideration:

1. **Graphical Interface**: Windows Forms or WPF interface
2. **Keyboard Shortcuts**: Quick access to common functions
3. **Custom Themes**: User-selectable color schemes
4. **Advanced Sorting**: Multiple sorting and filtering options
5. **Statistics Dashboard**: Visual representation of task completion

## 12. Conclusion

The Daily Task Manager represents a complete, functional implementation of a console-based 
task management system. The application successfully demonstrates key software engineering 
principles including modular design, secure authentication, data persistence, and 
user-friendly interface design. By following the documentation provided, users can effectively 
manage their daily tasks while developers can understand the implementation details and 
architecture decisions.

The project meets all specified requirements including user authentication with secure 
password hashing, comprehensive task management features, JSON data persistence, and 
robust error handling. The application serves as both a practical tool for task management 
and an educational resource for C# development best practices.

For ongoing maintenance and enhancement, refer to the technical specifications and architecture 
documentation provided. The modular design facilitates future improvements while maintaining 
backward compatibility with existing data formats.

---