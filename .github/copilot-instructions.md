# Copilot Instructions for SerialCommunication

## Project Overview

SerialCommunication is a Windows Forms application (.NET Framework 4.7.2) that provides a user interface for configuring and communicating with Arduino devices over serial ports. The application allows users to set serial port parameters, establish connections, and interact with Arduino through various exercises involving digital I/O, PWM, analog input, and temperature control.

## Build & Development

### Building the Project

This project requires **Visual Studio 2019** or later (or Visual Studio Build Tools for .NET Framework 4.7.2).

```powershell
# Using Visual Studio (open the .sln or .slnx file directly)
# Or use MSBuild via Visual Studio Developer Command Prompt

# The project file is located at:
SerialCommunication\SerialCommunication.csproj

# Build configurations available: Debug and Release
# Output: bin\Debug\ or bin\Release\
```

### Running the Application

Open Visual Studio and press **F5** to build and run, or:

```powershell
# After building with Visual Studio, the executable is located at:
SerialCommunication\bin\Debug\SerialCommunication.exe
SerialCommunication\bin\Release\SerialCommunication.exe
```

## Architecture

### Key Components

- **Form1Jurre.cs** - Main application window
  - Serial port management and connection logic
  - UI event handlers for port configuration (baud rate, parity, stop bits, handshake)
  - Communication with Arduino (reading/writing commands)

- **Form1Jurre.Designer.cs** - Auto-generated Windows Forms designer code
  - UI control initialization and layout
  - Do NOT manually edit this file (use Visual Studio Designer instead)

- **Program.cs** - Application entry point
  - STAThread configuration for Windows Forms
  - Creates and runs the main form

### UI Structure

The application uses a **TabControl** with multiple tabs:

1. **Instellingen (Settings)** - Serial port configuration
2. **Oefening1-5 (Exercises 1-5)** - Different Arduino interaction scenarios:
   - Digital I/O controls
   - PWM (Pulse Width Modulation) control with track bars
   - Analog input display
   - Temperature control (with desired/current temp display)

### Serial Communication Flow

1. User selects a serial port from dropdown (populated from `SerialPort.GetPortNames()`)
2. User configures port parameters (baud rate: 115200 default, data bits, parity, stop bits, handshake)
3. User clicks "Connect" button to establish connection
4. Application sends "ping" command and waits for response
5. Subsequent interactions send/receive data via `serialPortArduino.WriteLine()` and `serialPortArduino.ReadLine()`

## Key Conventions

### Naming Conventions

- **Controls**: Use descriptive prefixes
  - `comboBoxPoort` - Serial port selection combo box (uses Dutch naming: "Poort" = Port)
  - `radioButtonParityEven`, `checkBoxRtsEnable` - Other control types
  - `labelStatus` - Status display label
  - `serialPortArduino` - SerialPort component

- **Event Handlers**: Follow `Control_EventName` pattern
  - `buttonConnect_Click` - Button click handler
  - `cboPoort_DropDown` - Dropdown event handler
  - `Form1_Load` - Form initialization

### Code Style

- **Exception Handling**: Wrapped try-catch blocks are used extensively (often with silent catches - review if appropriate for your changes)
- **Distinct Values**: Avoid duplicate serial port names with `.Distinct().ToArray()`
- **Control State**: Check `serialPortArduino.IsOpen` before operations
- **Radio Buttons**: Use `if-else if` chains to check which option is selected (common pattern for mutually exclusive options)

### Language Mix

Code contains Dutch comments and variable names (legacy development artifact). When adding new code:
- Use English for new comments and variable names
- Maintain consistency with existing code style

### Embedded Resources

The application includes embedded images in the Resources folder:
- `digital in.png`
- `digital out.png`
- `analog in.png`
- `analog out.png`
- `thermostat.png`

These are referenced through the Resources.Designer.cs file and used by PictureBox controls.

## Arduino Communication Protocol

The application expects Arduino responses to specific commands:
- **Ping**: Sends "ping" on connection to verify communication
- **Response Format**: Expects newline-terminated strings via `ReadLine()`

When modifying communication logic, ensure proper string encoding and line termination handling.

## Common Tasks

### Adding a New Exercise Tab

1. Add a new TabPage in Form1Jurre.Designer.cs (via Visual Studio Designer)
2. Name it following the pattern: `tabPageOefening[N]`
3. Add controls (checkboxes, track bars, etc.) to the tab
4. Add corresponding event handlers in Form1Jurre.cs
5. Implement serial communication logic in the event handlers

### Modifying Serial Port Parameters

All serial port configuration is handled in the `buttonConnect_Click` method. The pattern is:
1. Read UI control values
2. Set corresponding `serialPortArduino` properties
3. Call `serialPortArduino.Open()`

### Updating Port Dropdown

The port dropdown is refreshed in two places:
- `Form1_Load` - Initial population (default baud rate to 115200)
- `cboPoort_DropDown` - Refresh on dropdown to detect hot-plugged devices
