# Unity Exercise 

The project is made in Unity 2017.1.1

# Project Overview

- **Stimulus** - An event that a player has to respond to.
- **Session** - A session refers to an entire playthrough of a game.
- **Trial** - A trial is when a player has to respond to a stimulus, which becomes marked as a success or failure depending on the player's response.
- **TrialResult** - A result contains data for how the player responded during a Trial.
- **Session File** - A session file contains all the Trials that will be played during a session, as well as any additional variables that allow us to control and customize the game.
- **Session Log** - An xml log file generated at the end of a session, contains all the attributes defined in the source Session file as well as all the Trial results that were generated during the game session.
- **Trace Log** - A text log file generated using GUILog for debugging and analytical purposes. GUILog requires a SaveLog() function to be called at the end of a session in order for the log to be saved.
- **GameController** - Tracks all the possible game options and selects a defined game to be played at the start of the application.
- **InputController** - Checks for player input and sends an event to the Active game that may be assigned.
- **GUILog** - A trace file logging solution, similar to Unity's Debug.Log, except this one creates a unique log file in the application's starting location.
- **GameBase** - The base class for all games.
- **GameData** - A base class, used for storing game specific data.
- **GameType** - Used to distinguish to which game a Session file belongs to.

# HighStriker
The implemented game is a virtual version of the high striker carnival game. At the carnival, one would use a hammer strike to launch a metal ball as high as possible. Since keyboard input is binary however, we instead base the difficulty of the game off of timing instead of pure strength. Players must time their key presses correctly to score the highest points. These scores will be saved so you can keep trying to reach a new high score!

## Getting Started

Clone this repository on your computer. Make sure Visual Studio 2017 is installed along with the .NET Framework 4.5 and Unity3d 2017.1.1

### Prerequisites

- Visual Studio 2017
- .NET Framework 4.5.1
- Unity3d 2017.1.1

### Installing

Install Unity3d 2017.1.1 first.
Then install Visual Studio 2017 and when the installer pops up, choose '.NET desktop development' and 'Game development with Unity' under 'Mobile & Gaming'.

## Built With

* [Unity3d 2017.1.1](https://unity3d.com/) - The Game Engine
* [Visual Studio 2017](https://www.visualstudio.com/downloads/) - The IDE
* [.NET Framework 4.5.1](https://www.microsoft.com/en-us/download/details.aspx?id=40779) - Coding Framework
* [Git](https://git-scm.com/) - Version Control

## Authors

* **Dante Nardo**
