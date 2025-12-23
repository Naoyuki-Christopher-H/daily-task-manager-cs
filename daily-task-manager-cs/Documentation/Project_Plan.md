# Daily Task Manager - Project Plan

## Table of Contents
1. Project Overview
2. Project Scope
3. Technical Requirements
4. Development Phases
5. Timeline and Schedule
6. Risk Assessment and Mitigation
7. Testing Strategy
8. Documentation Requirements
9. Submission Checklist
10. Evaluation Criteria

## 1. Project Overview

The Daily Task Manager is a console-based application 
developed in C# 4.7.2 that allows users to manage 
their daily tasks. The application provides user 
authentication, task management with due dates and 
priorities, and persistent data storage using JSON. 
The project is designed to demonstrate proficiency 
in C# programming, data persistence, secure authentication, 
and console application development.

### Project Goals
- Develop a functional console-based task management application
- Implement secure user authentication with password hashing
- Create a persistent data storage system using JSON
- Provide intuitive task management features (CRUD operations)
- Ensure code quality through proper structure and documentation

## 2. Project Scope

### In Scope
- Console-based user interface with menu navigation
- User authentication (registration and login)
- Task management (create, read, update, delete)
- Task properties: title, due date, priority, completion status
- JSON data persistence for users and tasks
- Input validation and error handling
- ASCII art display on first use

### Out of Scope
- Web interface or GUI
- Multi-user concurrency support
- Task categories or tags
- Task sharing between users
- Email notifications
- Mobile application
- Advanced reporting features

## 3. Technical Requirements

### Development Environment
- **Framework**: .NET Framework 4.7.2
- **IDE**: Visual Studio 2019 or later
- **Language**: C# 4.7.2 compatible syntax
- **Target Platform**: Windows Console Application

### Required Features
1. **User Authentication**
   - Username and password registration
   - Secure password hashing using SHA256 with salt
   - Login functionality with credential validation

2. **Task Management**
   - Create new tasks with title, optional due date, and priority
   - View all tasks with sorting (incomplete first, then priority, then due date)
   - Mark tasks as complete
   - Delete existing tasks
   - Edit task details

3. **Data Persistence**
   - Single JSON file storage (`tasks.json`)
   - Automatic file creation if not exists
   - Save after every data modification
   - Load data on application startup

4. **User Interface**
   - ASCII art welcome on first use
   - Color-coded console output
   - Clear menu navigation
   - Input validation and error messages

### Technical Constraints
- No external libraries beyond .NET Framework 4.7.2
- Allman style code formatting
- XML documentation comments for all public members
- Proper exception handling
- Memory management considerations

## 4. Development Phases

### Phase 1: Planning and Setup (1 hour)
**Objectives**
- Define project structure and architecture
- Set up development environment
- Create initial project files and structure
- Review requirements and create detailed specifications

**Tasks**
1. Create Visual Studio project with proper configuration
2. Design class hierarchy and relationships
3. Plan JSON data structure
4. Create directory structure
5. Set up version control (Git)

**Deliverables**
- Project structure document
- Initial solution file
- Class diagram
- JSON schema design

### Phase 2: Initial Implementation (4 hours)
**Objectives**
- Implement core data models
- Create basic JSON persistence
- Develop authentication system
- Build main application structure

**Tasks**
1. **Hour 1: Core Models** (1 hour)
   - Implement `User`, `TaskItem`, `Priority` enum, `AppData` classes
   - Add proper properties and constructors
   - Implement `ToString()` methods for display

2. **Hour 2: JSON Data Manager** (1 hour)
   - Create `JsonDataManager` class
   - Implement file I/O operations
   - Develop JSON serialization/deserialization
   - Add error handling for file operations

3. **Hour 3: Authentication System** (1 hour)
   - Implement `AuthenticationManager` class
   - Develop password hashing with `PasswordHasher` utility
   - Create login and registration workflows
   - Add input validation for authentication

4. **Hour 4: Main Application Structure** (1 hour)
   - Implement `Program.cs` main entry point
   - Create application lifecycle management
   - Add ASCII art display
   - Set up main program loop

**Deliverables**
- Complete data model classes
- Working JSON persistence
- Functional authentication system
- Running console application skeleton

### Phase 3: Middle Development (3 hours)
**Objectives**
- Implement task management features
- Add user interface components
- Develop input validation
- Integrate all components

**Tasks**
1. **Hour 1: Task Manager Core** (1 hour)
   - Implement `TaskManager` class
   - Develop CRUD operations for tasks
   - Add task validation logic
   - Implement ID generation

2. **Hour 2: User Interface Enhancement** (1 hour)
   - Create `ConsoleHelper` utility class
   - Implement color-coded output
   - Develop menu navigation system
   - Add input validation helpers

3. **Hour 3: Feature Integration** (1 hour)
   - Integrate task manager with authentication
   - Connect all data persistence
   - Implement date validation and formatting
   - Add overdue task detection

**Deliverables**
- Complete task management functionality
- Enhanced user interface with colors
- Integrated application with all features
- Basic error handling and validation

### Phase 4: Conclusion and Refinement (3 hours)
**Objectives**
- Implement advanced features
- Add comprehensive error handling
- Perform testing and debugging
- Prepare final submission

**Tasks**
1. **Hour 1: Advanced Features** (1 hour)
   - Implement task search functionality
   - Add task editing capability
   - Develop statistics display
   - Create overdue and today's task views

2. **Hour 2: Error Handling and Validation** (1 hour)
   - Add comprehensive exception handling
   - Implement input validation for all user inputs
   - Add data integrity checks
   - Develop graceful error recovery

3. **Hour 3: Testing and Finalization** (1 hour)
   - Perform unit testing of all components
   - Conduct integration testing
   - Fix bugs and issues
   - Prepare final documentation
   - Create submission package

**Deliverables**
- Fully functional application with all features
- Comprehensive error handling
- Complete documentation
- Final project submission package

## 5. Timeline and Schedule

### Total Development Time: 11 hours

**Day 1: Foundation (5 hours)**
- 09:00-10:00: Planning and Setup (Phase 1)
- 10:00-14:00: Initial Implementation (Phase 2)

**Day 2: Development (3 hours)**
- 09:00-12:00: Middle Development (Phase 3)

**Day 3: Completion (3 hours)**
- 09:00-12:00: Conclusion and Refinement (Phase 4)

### Milestones
1. **Milestone 1** (End of Day 1): Basic application with authentication and data persistence
2. **Milestone 2** (End of Day 2): Complete task management with user interface
3. **Milestone 3** (End of Day 3): Final application with all features and error handling

## 6. Risk Assessment and Mitigation

### Technical Risks

**Risk 1: JSON Serialization Issues**
- **Probability**: Medium
- **Impact**: High
- **Mitigation**: Implement robust parsing with fallbacks, test with various data scenarios

**Risk 2: Password Hashing Security**
- **Probability**: Low
- **Impact**: High
- **Mitigation**: Use industry-standard SHA256 with salt, follow security best practices

**Risk 3: Data Corruption**
- **Probability**: Low
- **Impact**: High
- **Mitigation**: Implement data validation, create backup mechanism, handle file I/O errors

**Risk 4: Memory Management Issues**
- **Probability**: Low
- **Impact**: Medium
- **Mitigation**: Use proper disposal patterns, implement IDisposable where needed

### Project Risks

**Risk 1: Scope Creep**
- **Probability**: Medium
- **Impact**: Medium
- **Mitigation**: Strict adherence to defined requirements, prioritize core features

**Risk 2: Time Constraints**
- **Probability**: High
- **Impact**: Medium
- **Mitigation**: Follow phased approach, prioritize essential features first

**Risk 3: Testing Coverage**
- **Probability**: Medium
- **Impact**: High
- **Mitigation**: Allocate dedicated testing time, create test scenarios for all features

## 7. Testing Strategy

### Unit Testing
- Test each class in isolation
- Verify data model properties and methods
- Test JSON serialization/deserialization
- Validate password hashing and verification

### Integration Testing
- Test authentication flow end-to-end
- Verify task management operations
- Test data persistence across application restarts
- Validate user interface interactions

### Functional Testing Scenarios
1. **Authentication Testing**
   - New user registration
   - Existing user login
   - Invalid login attempts
   - Password validation

2. **Task Management Testing**
   - Create task with all properties
   - Edit existing task
   - Mark task as complete
   - Delete task
   - List tasks with sorting

3. **Data Persistence Testing**
   - Save and load user data
   - Handle missing JSON file
   - Test with corrupted JSON data
   - Verify data integrity

4. **Edge Case Testing**
   - Empty task lists
   - Invalid date formats
   - Duplicate usernames
   - Maximum task IDs

### Testing Tools
- Manual testing via console interface
- Visual Studio debugging tools
- JSON validation tools
- Memory profiling if needed

## 8. Documentation Requirements

### Code Documentation
- XML comments for all public classes and methods
- Inline comments for complex logic
- README file with setup instructions
- Code structure documentation

### User Documentation
- Application usage instructions
- Feature descriptions
- Input format specifications
- Troubleshooting guide

### Technical Documentation
- Architecture overview
- Data flow diagrams
- Class relationships
- JSON schema specification

### Submission Documentation
- Project plan document
- Implementation notes
- Testing results summary
- Known issues and limitations

## 9. Submission Checklist

### Required Files
- [ ] `Program.cs` - Main application entry point
- [ ] `Models/` - All data model classes
  - [ ] `User.cs`
  - [ ] `TaskItem.cs`
  - [ ] `Priority.cs`
  - [ ] `AppData.cs`
- [ ] `Managers/` - Business logic classes
  - [ ] `TaskManager.cs`
  - [ ] `AuthenticationManager.cs`
  - [ ] `JsonDataManager.cs`
- [ ] `Utils/` - Utility classes
  - [ ] `ConsoleHelper.cs`
  - [ ] `PasswordHasher.cs`
- [ ] Project files
  - [ ] `daily-task-manager-cs.csproj`
  - [ ] Solution file (if applicable)
- [ ] Documentation
  - [ ] `README.md`
  - [ ] `Documentation.md`
  - [ ] `Project_Plan.md`

### Quality Checks
- [ ] Code compiles without errors
- [ ] Allman style formatting applied
- [ ] XML documentation complete
- [ ] No hard-coded values
- [ ] Proper exception handling
- [ ] Input validation implemented
- [ ] Memory management considered
- [ ] JSON persistence working
- [ ] Authentication functional
- [ ] All requirements met

### Testing Verification
- [ ] Unit tests pass
- [ ] Integration tests successful
- [ ] Edge cases handled
- [ ] Error recovery tested
- [ ] Data persistence verified
- [ ] User interface functional

## 10. Evaluation Criteria

### Core Functionality (70 points)
- **Task Management (40 points)**
  - Add task: 10 points
  - Remove task: 10 points
  - Mark complete: 10 points
  - List tasks: 10 points

- **Application Structure (10 points)**
  - Proper class organization: 5 points
  - Main loop implementation: 5 points

- **Input Handling (10 points)**
  - Graceful invalid input handling: 5 points
  - Menu navigation: 5 points

- **ASCII Art (5 points)**
  - Display on first use: 5 points

- **Date and Priority Handling (5 points)**
  - Due date support: 3 points
  - Priority levels: 2 points

### Data Persistence (25 points)
- **JSON Implementation (15 points)**
  - File creation: 5 points
  - Data loading: 5 points
  - Data saving: 5 points

- **Data Structure (10 points)**
  - Proper JSON schema: 5 points
  - User-task association: 5 points

### User Authentication (20 points)
- **Account Management (10 points)**
  - User registration: 5 points
  - Login functionality: 5 points

- **Security (10 points)**
  - Password hashing: 7 points
  - Secure storage: 3 points

### Bonus Features (10 points)
- **Input Validation (3 points)**
  - Comprehensive validation: 3 points

- **Error Handling (2 points)**
  - Robust error handling: 2 points

- **Additional Features (5 points)**
  - Task editing: 2 points
  - Task search: 1 point
  - Color coding: 1 point
  - Statistics display: 1 point

### Total: 125 points

## Success Metrics
- Application meets all specified requirements
- Code is well-structured and documented
- Data persists correctly between sessions
- Authentication works securely
- User interface is intuitive
- Error handling is comprehensive
- Application is ready for submission

This project plan provides a comprehensive roadmap for developing 
the Daily Task Manager application within the specified timeframe. 
By following this structured approach, the project can be completed 
efficiently while maintaining code quality and meeting all requirements.

---