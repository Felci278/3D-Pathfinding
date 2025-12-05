using UnityEngine;

public class GridCell3D : MonoBehaviour
{
    public int x;
    public int z;
    public bool isWalkable = true;
    
    private Renderer cellRenderer;
    
    // Materials for different cell states
    [Header("Materials")]
    public Material floorMaterial;
    public Material obstacleMaterial;
    public Material pathMaterial;
    public Material startMaterial;
    public Material endMaterial;
    public Material visitedMaterial;
    
    private Material currentMaterial;
    
    void Awake()
    {
        cellRenderer = GetComponent<Renderer>();
        if (cellRenderer != null && floorMaterial != null)
        {
            cellRenderer.material = floorMaterial;
            currentMaterial = floorMaterial;
        }
    }
    
    public void SetWalkable(bool walkable)
    {
        isWalkable = walkable;
        if (cellRenderer != null)
        {
            Material newMat = walkable ? floorMaterial : obstacleMaterial;
            cellRenderer.material = newMat;
            currentMaterial = newMat;
        }
    }
    
    public void SetMaterial(Material mat)
    {
        if (cellRenderer != null && mat != null)
        {
            cellRenderer.material = mat;
            currentMaterial = mat;
        }
    }
    
    public bool IsStartOrEnd()
    {
        return currentMaterial == startMaterial || currentMaterial == endMaterial;
    }
    
    void OnMouseDown()
    {
        // Toggle obstacle when clicked (not on start/end)
        if (!IsStartOrEnd())
        {
            SetWalkable(!isWalkable);
            Debug.Log($"Toggled cell at ({x}, {z}) - Walkable: {isWalkable}");
        }
    }
}