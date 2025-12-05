using UnityEngine;
using TMPro;

public class GridLabels : MonoBehaviour
{
    public GridManager3D gridManager;
    public GameObject labelPrefab;
    public bool showLabels = true;

    void Start()
    {
        if (showLabels)
            CreateLabels();
    }

    void CreateLabels()
    {
        // Create X-axis labels
        for (int x = 0; x < gridManager.gridWidth; x += 5)
        {
            CreateLabel(x.ToString(), new Vector3(x - gridManager.gridWidth / 2f + 0.5f, 0.1f, -gridManager.gridDepth / 2f - 1f));
        }

        // Create Z-axis labels
        for (int z = 0; z < gridManager.gridDepth; z += 5)
        {
            CreateLabel(z.ToString(), new Vector3(-gridManager.gridWidth / 2f - 1f, 0.1f, z - gridManager.gridDepth / 2f + 0.5f));
        }
    }

    void CreateLabel(string text, Vector3 position)
    {
        GameObject labelObj = new GameObject($"Label_{text}");
        labelObj.transform.SetParent(transform);
        labelObj.transform.position = position;
        labelObj.transform.rotation = Quaternion.Euler(90, 0, 0);

        TextMesh textMesh = labelObj.AddComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = 30;
        textMesh.characterSize = 0.1f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
    }
}
