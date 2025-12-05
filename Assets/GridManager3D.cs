using UnityEngine;
using System.Collections.Generic;

public class GridManager3D : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 20;
    public int gridDepth = 15;
    public GameObject floorTilePrefab;
    
    [Header("Pathfinding Points")]
    public Vector2Int startPoint = new Vector2Int(2, 2);
    public Vector2Int endPoint = new Vector2Int(17, 12);
    
    [Header("Factory Obstacles")]
    public bool generateFactoryObstacles = true;
    
    private GridCell3D[,] grid;
    
    void Start()
    {
        CreateFactory();
        if (generateFactoryObstacles)
        {
            GenerateFactoryLayout();
        }
        MarkStartAndEnd();
    }
    
    void CreateFactory()
    {
        grid = new GridCell3D[gridWidth, gridDepth];
        
        // Center the grid
        float offsetX = gridWidth / 2f - 0.5f;
        float offsetZ = gridDepth / 2f - 0.5f;
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridDepth; z++)
            {
                Vector3 position = new Vector3(x - offsetX, 0, z - offsetZ);
                GameObject tileObject = Instantiate(floorTilePrefab, position, Quaternion.identity);
                tileObject.transform.parent = transform;
                tileObject.name = $"Tile_{x}_{z}";
                
                GridCell3D cell = tileObject.GetComponent<GridCell3D>();
                cell.x = x;
                cell.z = z;
                grid[x, z] = cell;
            }
        }
    }
    
    void GenerateFactoryLayout()
    {
        // Create factory "machines" and "walls"
        
        // Assembly line 1 (horizontal)
        for (int x = 4; x < 8; x++)
        {
            SetObstacle(x, 5);
        }
        
        // Assembly line 2 (horizontal)
        for (int x = 12; x < 16; x++)
        {
            SetObstacle(x, 9);
        }
        
        // Storage area (vertical wall)
        for (int z = 2; z < 7; z++)
        {
            SetObstacle(10, z);
        }
        
        // Large machine in corner
        for (int x = 15; x < 18; x++)
        {
            for (int z = 3; z < 6; z++)
            {
                SetObstacle(x, z);
            }
        }
        
        // Border walls
        for (int x = 0; x < gridWidth; x++)
        {
            SetObstacle(x, 0);
            SetObstacle(x, gridDepth - 1);
        }
        for (int z = 0; z < gridDepth; z++)
        {
            SetObstacle(0, z);
            SetObstacle(gridWidth - 1, z);
        }
    }
    
    void SetObstacle(int x, int z)
    {
        if (IsValid(x, z))
        {
            grid[x, z].SetWalkable(false);
            // Make obstacles taller to look like machines
            grid[x, z].transform.localScale = new Vector3(0.9f, Random.Range(0.3f, 1.5f), 0.9f);
        }
    }
    
    void MarkStartAndEnd()
    {
        grid[startPoint.x, startPoint.y].SetMaterial(grid[startPoint.x, startPoint.y].startMaterial);
        grid[endPoint.x, endPoint.y].SetMaterial(grid[endPoint.x, endPoint.y].endMaterial);
    }
    
    public GridCell3D GetCell(int x, int z)
    {
        if (IsValid(x, z))
            return grid[x, z];
        return null;
    }
    
    bool IsValid(int x, int z)
    {
        return x >= 0 && x < gridWidth && z >= 0 && z < gridDepth;
    }
    
    public List<GridCell3D> GetNeighbors(GridCell3D cell)
    {
        List<GridCell3D> neighbors = new List<GridCell3D>();
        
        // 4-directional movement
        int[] dx = { 0, 0, -1, 1 };
        int[] dz = { 1, -1, 0, 0 };
        
        for (int i = 0; i < 4; i++)
        {
            int newX = cell.x + dx[i];
            int newZ = cell.z + dz[i];
            
            GridCell3D neighbor = GetCell(newX, newZ);
            if (neighbor != null && neighbor.isWalkable)
            {
                neighbors.Add(neighbor);
            }
        }
        
        return neighbors;
    }
}