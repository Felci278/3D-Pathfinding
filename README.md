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

## How to Use

1. Open the project in Unity (3D Core template recommended)
2. Press **Play**
3. Click tiles to add/remove obstacles
4. Press **D** or **A** to start the corresponding pathfinding algorithm
5. Watch the robot navigate the computed path
6. Press **C** to reset the path
