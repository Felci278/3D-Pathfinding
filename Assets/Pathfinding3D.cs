using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinding3D : MonoBehaviour
{
    private GridManager3D gridManager;
    public RobotController robot;

    [Header("Visualization")]
    public float visualizationDelay = 0.05f;

    private bool isPathfinding = false;

    [Header("Statistics")]
    public PathfindingStats statsTracker;
    private int cellsExplored = 0;

    void Start()
    {
        gridManager = GetComponent<GridManager3D>();

        // Position robot at start
        if (robot != null)
        {
            GridCell3D startCell = gridManager.GetCell(gridManager.startPoint.x, gridManager.startPoint.y);
            if (startCell != null)
            {
                robot.transform.position = startCell.transform.position + Vector3.up * robot.hoverHeight;
            }
        }
    }

    void Update()
    {
        if (isPathfinding) return; // Don't start new pathfinding while one is running

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(FindPathDijkstra());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(FindPathAStar());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearPath();
        }
    }

    IEnumerator FindPathDijkstra()
    {
        isPathfinding = true;
        Debug.Log("Starting Dijkstra's Algorithm...");

        cellsExplored = 0;
        float startTime = Time.realtimeSinceStartup;

        ClearPath();
        yield return new WaitForSeconds(0.1f);

        GridCell3D startCell = gridManager.GetCell(gridManager.startPoint.x, gridManager.startPoint.y);
        GridCell3D endCell = gridManager.GetCell(gridManager.endPoint.x, gridManager.endPoint.y);

        Dictionary<GridCell3D, float> distances = new Dictionary<GridCell3D, float>();
        Dictionary<GridCell3D, GridCell3D> previousCells = new Dictionary<GridCell3D, GridCell3D>();
        List<GridCell3D> unvisited = new List<GridCell3D>();

        // Initialize
        for (int x = 0; x < gridManager.gridWidth; x++)
        {
            for (int z = 0; z < gridManager.gridDepth; z++)
            {
                GridCell3D cell = gridManager.GetCell(x, z);
                if (cell != null && cell.isWalkable)
                {
                    distances[cell] = float.MaxValue;
                    unvisited.Add(cell);
                }
            }
        }

        distances[startCell] = 0;

        // Main algorithm
        while (unvisited.Count > 0)
        {
            GridCell3D current = unvisited.OrderBy(cell => distances[cell]).First();

            if (current == endCell)
                break;

            unvisited.Remove(current);

            // Visualize
            if (!current.IsStartOrEnd())
            {
                current.SetMaterial(current.visitedMaterial);
                cellsExplored++;
                yield return new WaitForSeconds(visualizationDelay);
            }

            foreach (GridCell3D neighbor in gridManager.GetNeighbors(current))
            {
                if (!unvisited.Contains(neighbor))
                    continue;

                float newDistance = distances[current] + 1;

                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    previousCells[neighbor] = current;
                }
            }
        }

        // Reconstruct path
        if (previousCells.ContainsKey(endCell))
        {
            List<GridCell3D> pathCells = ReconstructPath(previousCells, endCell, startCell);
            VisualizePath(pathCells);
            MoveRobotAlongPath(pathCells);

            float endTime = Time.realtimeSinceStartup;
            float totalTime = (endTime - startTime) * 1000f; // Convert to ms

            if (statsTracker != null)
            {
                statsTracker.RecordRun("Dijkstra", cellsExplored, pathCells.Count, totalTime);
            }
            Debug.Log($"Dijkstra - Path found! Length: {pathCells.Count} cells");
        }
        else
        {
            Debug.Log("No path found!");
        }

        isPathfinding = false;
    }

    IEnumerator FindPathAStar()
    {
        isPathfinding = true;
        Debug.Log("Starting A* Algorithm...");

        cellsExplored = 0;
        float startTime = Time.realtimeSinceStartup;

        ClearPath();
        yield return new WaitForSeconds(0.1f);

        GridCell3D startCell = gridManager.GetCell(gridManager.startPoint.x, gridManager.startPoint.y);
        GridCell3D endCell = gridManager.GetCell(gridManager.endPoint.x, gridManager.endPoint.y);

        List<GridCell3D> openSet = new List<GridCell3D> { startCell };
        HashSet<GridCell3D> closedSet = new HashSet<GridCell3D>();

        Dictionary<GridCell3D, float> gScore = new Dictionary<GridCell3D, float>();
        Dictionary<GridCell3D, float> fScore = new Dictionary<GridCell3D, float>();
        Dictionary<GridCell3D, GridCell3D> previousCells = new Dictionary<GridCell3D, GridCell3D>();

        gScore[startCell] = 0;
        fScore[startCell] = Heuristic(startCell, endCell);

        while (openSet.Count > 0)
        {
            GridCell3D current = openSet.OrderBy(cell => fScore.ContainsKey(cell) ? fScore[cell] : float.MaxValue).First();

            if (current == endCell)
            {
                List<GridCell3D> pathCells = ReconstructPath(previousCells, current, startCell);
                VisualizePath(pathCells);
                MoveRobotAlongPath(pathCells);

                float endTime = Time.realtimeSinceStartup;
                float totalTime = (endTime - startTime) * 1000f;

                if (statsTracker != null)
                {
                    statsTracker.RecordRun("A*", cellsExplored, pathCells.Count, totalTime);
                }
                Debug.Log($"A* - Path found! Length: {pathCells.Count} cells");
                isPathfinding = false;
                yield break;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            // Visualize
            if (!current.IsStartOrEnd())
            {
                current.SetMaterial(current.visitedMaterial);
                cellsExplored++;
                yield return new WaitForSeconds(visualizationDelay);
            }

            foreach (GridCell3D neighbor in gridManager.GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + 1;

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= (gScore.ContainsKey(neighbor) ? gScore[neighbor] : float.MaxValue))
                {
                    continue;
                }

                previousCells[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, endCell);
            }
        }

        Debug.Log("No path found!");
        isPathfinding = false;
    }

    float Heuristic(GridCell3D a, GridCell3D b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.z - b.z);
    }

    List<GridCell3D> ReconstructPath(Dictionary<GridCell3D, GridCell3D> previousCells, GridCell3D current, GridCell3D startCell)
    {
        List<GridCell3D> path = new List<GridCell3D>();

        while (current != startCell)
        {
            path.Add(current);
            current = previousCells[current];
        }
        path.Add(startCell);
        path.Reverse();

        return path;
    }

    void VisualizePath(List<GridCell3D> pathCells)
    {
        foreach (GridCell3D cell in pathCells)
        {
            if (!cell.IsStartOrEnd())
            {
                cell.SetMaterial(cell.pathMaterial);
            }
        }
    }

    void MoveRobotAlongPath(List<GridCell3D> pathCells)
    {
        if (robot != null)
        {
            List<Vector3> pathPositions = new List<Vector3>();
            foreach (GridCell3D cell in pathCells)
            {
                pathPositions.Add(cell.transform.position);
            }
            robot.SetPath(pathPositions);
        }
    }

    void ClearPath()
    {
        for (int x = 0; x < gridManager.gridWidth; x++)
        {
            for (int z = 0; z < gridManager.gridDepth; z++)
            {
                GridCell3D cell = gridManager.GetCell(x, z);
                if (cell != null && cell.isWalkable && !cell.IsStartOrEnd())
                {
                    cell.SetMaterial(cell.floorMaterial);
                }
            }
        }

        // Restore start and end
        GridCell3D startCell = gridManager.GetCell(gridManager.startPoint.x, gridManager.startPoint.y);
        GridCell3D endCell = gridManager.GetCell(gridManager.endPoint.x, gridManager.endPoint.y);
        startCell.SetMaterial(startCell.startMaterial);
        endCell.SetMaterial(endCell.endMaterial);

        if (robot != null)
        {
            robot.StopMoving();
        }

        if (statsTracker != null)
        {
            statsTracker.ClearStats();
        }
    }
}
