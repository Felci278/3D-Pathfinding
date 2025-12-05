using UnityEngine;
using TMPro;

public class PathfindingStats : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI comparisonText;

    [Header("Current Run Stats")]
    public string lastAlgorithm = "None";
    public int cellsVisited = 0;
    public int pathLength = 0;
    public float timeMs = 0f;

    [Header("Comparison Stats")]
    public int dijkstraCellsVisited = 0;
    public int dijkstraPathLength = 0;
    public float dijkstraTimeMs = 0f;

    public int astarCellsVisited = 0;
    public int astarPathLength = 0;
    public float astarTimeMs = 0f;

    public void RecordRun(string algorithmName, int cells, int path, float time)
    {
        lastAlgorithm = algorithmName;
        cellsVisited = cells;
        pathLength = path;
        timeMs = time;

        // Store for comparison
        if (algorithmName == "Dijkstra")
        {
            dijkstraCellsVisited = cells;
            dijkstraPathLength = path;
            dijkstraTimeMs = time;
        }
        else if (algorithmName == "A*")
        {
            astarCellsVisited = cells;
            astarPathLength = path;
            astarTimeMs = time;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Update current stats with industrial formatting
        if (statsText != null)
        {
            string statusIcon = lastAlgorithm == "Dijkstra" ? "⬢" : lastAlgorithm == "A*" ? "⬡" : "○";

            statsText.text =
                $"<size=18><b>{statusIcon} {lastAlgorithm.ToUpper()}</b></size>\n" +
                $"<color=#B3B3B3>━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n" +
                $"<b>CELLS EXPLORED</b>\n" +
                $"<size=22><color=#33CC4D>{cellsVisited:D4}</color></size> nodes\n\n" +
                $"<b>PATH LENGTH</b>\n" +
                $"<size=22><color=#3399E6>{pathLength:D4}</color></size> cells\n\n" +
                $"<b>COMPUTATION TIME</b>\n" +
                $"<size=22><color=#FF9919>{timeMs:F2}</color></size> ms\n";
        }

        // Update comparison with better formatting
        if (comparisonText != null && dijkstraTimeMs > 0 && astarTimeMs > 0)
        {
            int cellsSaved = dijkstraCellsVisited - astarCellsVisited;
            float timeSaved = dijkstraTimeMs - astarTimeMs;
            float efficiency = (1f - (float)astarCellsVisited / dijkstraCellsVisited) * 100f;

            string efficiencyBar = GenerateEfficiencyBar(efficiency);

            comparisonText.text =
                $"<size=16><b>⚡ ALGORITHM COMPARISON</b></size>\n" +
                $"<color=#B3B3B3>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n" +
                $"<b>⬢ DIJKSTRA'S ALGORITHM</b>\n" +
                $"<color=#B3B3B3>├─</color> Explored: <color=#FF9919>{dijkstraCellsVisited}</color> cells\n" +
                $"<color=#B3B3B3>├─</color> Path Len: <color=#3399E6>{dijkstraPathLength}</color> cells\n" +
                $"<color=#B3B3B3>└─</color> Time: <color=#FFCC19>{dijkstraTimeMs:F2}</color> ms\n\n" +
                $"<b>⬡ A* ALGORITHM</b>\n" +
                $"<color=#B3B3B3>├─</color> Explored: <color=#FF9919>{astarCellsVisited}</color> cells\n" +
                $"<color=#B3B3B3>├─</color> Path Len: <color=#3399E6>{astarPathLength}</color> cells\n" +
                $"<color=#B3B3B3>└─</color> Time: <color=#FFCC19>{astarTimeMs:F2}</color> ms\n\n" +
                $"<size=14><b><color=#33CC4D>✓ EFFICIENCY GAIN</color></b></size>\n" +
                $"{efficiencyBar}\n" +
                $"<color=#B3B3B3>├─</color> Reduction: <color=#33CC4D>{efficiency:F1}%</color> fewer cells\n" +
                $"<color=#B3B3B3>├─</color> Saved: <color=#33CC4D>{cellsSaved}</color> cell explorations\n" +
                $"<color=#B3B3B3>└─</color> Faster by: <color=#33CC4D>{timeSaved:F2}</color> ms\n";
        }
        else
        {
            if (comparisonText != null)
            {
                comparisonText.text =
                    $"<size=16><b>⚡ ALGORITHM COMPARISON</b></size>\n" +
                    $"<color=#B3B3B3>━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━</color>\n\n" +
                    $"<color=#FFCC19>⚠ AWAITING DATA</color>\n\n" +
                    $"<color=#B3B3B3>Run both Dijkstra (D) and\n" +
                    $"A* (A) algorithms to generate\n" +
                    $"comparative performance metrics.</color>";
            }
        }
    }

    string GenerateEfficiencyBar(float percentage)
    {
        int barLength = 20;
        int filled = Mathf.RoundToInt((percentage / 100f) * barLength);

        string bar = "<color=#33CC4D>";
        for (int i = 0; i < filled; i++)
            bar += "█";

        bar += "</color><color=#40444D>";

        for (int i = filled; i < barLength; i++)
            bar += "█";

        bar += "</color>";

        return bar;
    }

    public void ClearStats()
    {
        lastAlgorithm = "None";
        cellsVisited = 0;
        pathLength = 0;
        timeMs = 0f;
        UpdateUI();
    }
}
