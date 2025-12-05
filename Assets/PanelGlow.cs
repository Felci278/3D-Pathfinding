using UnityEngine;
using UnityEngine.UI;

public class PanelGlow : MonoBehaviour
{
    public Color glowColor = new Color(0.2f, 0.6f, 0.9f, 0.3f);
    public float glowSpeed = 2f;
    public float minAlpha = 0.1f;
    public float maxAlpha = 0.4f;

    private Image glowImage;

    void Start()
    {
        CreateGlow();
    }

    void CreateGlow()
    {
        GameObject glowObj = new GameObject("Glow");
        glowObj.transform.SetParent(transform, false);
        glowObj.transform.SetAsFirstSibling(); // Behind everything

        RectTransform rect = glowObj.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = new Vector2(4, 4); // Slightly larger
        rect.anchoredPosition = Vector2.zero;

        glowImage = glowObj.AddComponent<Image>();
        glowImage.color = glowColor;
    }

    void Update()
    {
        if (glowImage != null)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * glowSpeed) + 1f) / 2f);
            Color newColor = glowColor;
            newColor.a = alpha;
            glowImage.color = newColor;
        }
    }
}