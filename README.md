# 3D Factory Pathfinding

A Unity 3D project demonstrating **pathfinding algorithms (Dijkstra & A*)** in a dynamic factory environment with interactive obstacles and a 3D robot.

## Overview

This project transforms a classic 2D grid-based pathfinding demo into a **fully interactive 3D factory simulation**. Users can place obstacles, visualize algorithms in action, and watch a robot navigate the environment in real-time.

Key features include:

- **3D Factory Environment**
    - Grid-based factory floor with tiles and obstacles
    - Customizable factory layout with machines, walls, and storage areas
- **Interactive Tiles**
    - Click on tiles to toggle obstacles dynamically
    - Tiles change color to indicate their state (walkable, obstacle, start/end, visited, path)
- **3D Robot Navigation**
    - Robot model with body and head
    - Hover animation while moving
    - Follows the computed path in real-time
- **Pathfinding Algorithms**
    - **Dijkstra's Algorithm**: Explores all reachable nodes to find the shortest path
    - **A* Algorithm**: Optimized exploration using heuristics
    - Visual feedback for visited tiles and final path
- **UI & Instructions**
    - Instructions panel with controls
    - Algorithm stats panel for visual comparison

## Controls

- **Click tiles** → Toggle obstacles
- **D** → Run Dijkstra's Algorithm
- **A** → Run A* Algorithm
- **C** → Clear path

**Tile Colors:**

- **Blue** → Start
- **Orange** → End
- **Grey** → Obstacles/Machines
- **Lavender** → Visited
- **Green** → Path

## Project Structure

The **3D-Pathfinding** project is organized to separate core Unity elements, assets, scripts, and settings for maintainability and clarity.

- **Assets/** – Contains all the game assets including scenes, prefabs, scripts, materials, UI elements, and fonts. This is the main folder for all content used in the project.
- **ProjectSettings/** – Stores Unity project configuration files.
- **Packages/** – Contains Unity package dependencies and manifests.

Here is a **clean and professional “How to Run” section** you can paste directly into your GitHub README.
It includes **all required Unity steps**, including the **Active Input Handling = Both** setting.


## How to Run the Project

Follow these steps to open and run the **3D Factory Pathfinding** project on your own machine.


 **1. Install Requirements**

Before starting, make sure you have:

* **Unity Hub** installed
* **Unity 2022.3 LTS (or newer)**
* **3D Core Template** available
* Internet connection for first-time TextMeshPro import (Unity will prompt automatically)


**2. Download or Clone the Repository**

*Option A — Clone via Git*

```bash
git clone https://github.com/Felci278/3D-Pathfinding.git
```

 *Option B — Download ZIP*

* Click **Code → Download ZIP**
* Extract it to a folder of your choice


 **3. Open the Project in Unity**

1. Open **Unity Hub**
2. Click **Add**
3. Select the project folder
4. Make sure Unity opens it with **2022.3 LTS or later**

The project will load into the Unity Editor.


**4. IMPORTANT: Change Input Settings**

This project uses Unity’s **Old Input System**, so you MUST enable either old or both input handlers.

 *Do this step before pressing Play:*

**Go to:**
`Edit → Project Settings → Player`
**Under:** *Other Settings*
**Find:** *Active Input Handling* under other settings section
**Set it to:** **"Both"**

This ensures the Input System Actions work correctly (D, A, C keys and click interactions).


**5. Enter Play Mode**

Press the **Play** button in the Unity Editor. You will now the see the grid and panel instructions for running the game.


**6. Optional — Modify Prefabs or Scripts**

If you want to customize the project:

* Floor tiles → `Assets/PreFabs/`
* Materials → `Assets/Materials/`
* Robot logic → `RobotController.cs`
* Pathfinding algorithms → `Pathfinding3D.cs`
* Grid generation → `GridManager3D.cs`


