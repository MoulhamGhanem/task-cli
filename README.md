# Task Tracker CLI (`task-cli`) 🚀

A robust, lightweight Command Line Interface (CLI) application built with C# and .NET. This tool allows developers and users to manage their tasks directly from the terminal, with all data securely serialized and stored in a local JSON file.

## 📖 Overview

This project was built to master CLI application architecture, focusing on the **Single Responsibility Principle (SRP)** and **Clean Code** practices. 

Unlike interactive console applications that rely on constant `while` loops and `Console.ReadLine()`, `task-cli` operates as a native system tool. It parses arguments directly from the operating system (`args`), executes the command, reads/writes to the JSON storage, and exits instantly.

## ✨ Key Features

- **No External Databases:** Completely standalone. Uses `System.Text.Json` to manage a local `Tasks.json` file.
- **Smart ID Generation:** Automatically calculates the next available ID by parsing existing records.
- **Defensive Programming:** Includes robust error handling to prevent crashes from missing files, empty arguments, or invalid IDs.
- **State Management:** Tasks track their own lifecycle with specific properties: `ID`, `Description`, `Status` (Todo, In-Progress, Done), `CreatedAt`, and `UpdatedAt`.

## 🏗️ Architecture & Project Structure

The codebase is divided logically to separate data management from user interface routing:

- `clsTask.cs`: The core engine. Handles object instantiation, JSON serialization/deserialization, file I/O operations, and business logic (Add, Update, Delete, Change Status).
- `Program.cs`: The entry point and router. Validates OS-level arguments (`args`), parses inputs, and directs commands to the appropriate methods in `clsTask`.

## 💻 Installation & Setup

### For Developers (Run from Source)
1. Clone the repository:
   git clone [https://github.com/MoulhamGhanem/task-cli.git](https://github.com/MoulhamGhanem/task-cli.git)
   cd task-cli
Build the project:

dotnet build
Run using the .NET CLI:

dotnet run -- add "Test task"

### For End Users (Install globally on Windows)
To use task-cli natively from any terminal window without the dotnet run prefix:

Publish the project to a standalone executable:

dotnet publish -c Release -o ./publish
Navigate to the publish folder and rename the executable to task-cli.exe.

Add the publish folder path to your Windows Environment Variables (System Path).

Restart your terminal. You can now use the tool natively!

## 🛠️ Complete Usage Guide
Once configured, use the following commands from any terminal directory.

1. Add a New Task
task-cli add "Buy groceries"
task-cli add "Finish C# project"

2. Update an Existing Task
Requires the task ID and the New Description.
task-cli update 1 "Buy groceries and cook dinner"

3. Change Task Status
Update the progress of your task seamlessly.
task-cli mark-in-progress 1
task-cli mark-done 1

4. Delete a Task
Permanently removes the task from the JSON file.
task-cli delete 1

5. List Tasks
View your tasks in a clean console output. You can list all of them, or filter by status.

List all:
task-cli list

List by status:
task-cli list done
task-cli list todo
task-cli list in-progress
