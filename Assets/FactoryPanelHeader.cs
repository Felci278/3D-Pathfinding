using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactoryPanelHeader : MonoBehaviour
{
    public string headerText = "SYSTEM";
    public Color headerColor = new Color(1f, 0.6f, 0.1f, 1f);
    public Color backgroundColor = new Color(0.15f, 0.15f, 0.18f, 1f);

    void Start()
    {
        CreateHeader();
    }

    void CreateHeader()
    {
        // Create header background
        GameObject headerBG = new GameObject("HeaderBackground");
        headerBG.transform.SetParent(transform, false);

        RectTransform bgRect = headerBG.AddComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0, 1);
        bgRect.anchorMax = new Vector2(1, 1);
        bgRect.pivot = new Vector2(0.5f, 1);
        bgRect.sizeDelta = new Vector2(0, 35);
        bgRect.anchoredPosition = Vector2.zero;

        Image bgImage = headerBG.AddComponent<Image>();
        bgImage.color = backgroundColor;

        // Create header accent line
        GameObject accentLine = new GameObject("AccentLine");
        accentLine.transform.SetParent(headerBG.transform, false);

        RectTransform lineRect = accentLine.AddComponent<RectTransform>();
        lineRect.anchorMin = new Vector2(0, 0);
        lineRect.anchorMax = new Vector2(1, 0);
        lineRect.pivot = new Vector2(0.5f, 0);
        lineRect.sizeDelta = new Vector2(0, 3);

        Image lineImage = accentLine.AddComponent<Image>();
        lineImage.color = headerColor;

        // Create header text
        GameObject headerTextObj = new GameObject("HeaderText");
        headerTextObj.transform.SetParent(headerBG.transform, false);

        RectTransform textRect = headerTextObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        textRect.anchoredPosition = Vector2.zero;

        TextMeshProUGUI textComp = headerTextObj.AddComponent<TextMeshProUGUI>();
        textComp.text = headerText;
        textComp.fontSize = 16;
        textComp.fontStyle = FontStyles.Bold;
        textComp.alignment = TextAlignmentOptions.Center;
        textComp.color = Color.white;
    }
}