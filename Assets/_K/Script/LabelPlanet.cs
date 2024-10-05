using UnityEngine;
using UnityEngine.UI;

public class PlanetLabel : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;
    public Canvas uiCanvas;
    public Font labelFont;
    public int textSize = 24;
    public Color labelColor = Color.white;

    void Start()
    {
        CreateLabel(mercury);
        CreateLabel(venus);
        CreateLabel(earth);
        CreateLabel(mars);
        CreateLabel(jupiter);
        CreateLabel(saturn);
        CreateLabel(uranus);
        CreateLabel(neptune);
    }

    void CreateLabel(GameObject planet)
    {
        if (planet == null) return;
        
        GameObject labelObject = new GameObject(planet.name + "Label");
        labelObject.transform.SetParent(uiCanvas.transform);

        Text labelText = labelObject.AddComponent<Text>();
        labelText.text = planet.name;
        labelText.font = labelFont;
        labelText.fontSize = textSize;
        labelText.color = labelColor;
        labelText.alignment = TextAnchor.MiddleCenter;

        RectTransform rectTransform = labelObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(150, 50);
    }

    void Update()
    {
        UpdateLabelPosition(mercury);
        UpdateLabelPosition(venus);
        UpdateLabelPosition(earth);
        UpdateLabelPosition(mars);
        UpdateLabelPosition(jupiter);
        UpdateLabelPosition(saturn);
        UpdateLabelPosition(uranus);
        UpdateLabelPosition(neptune);
    }

    void UpdateLabelPosition(GameObject planet)
    {
        if (planet == null) return;
        
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(planet.transform.position);
        Transform labelTransform = uiCanvas.transform.Find(planet.name + "Label");
        if (labelTransform != null)
        {
            labelTransform.position = screenPosition;
            labelTransform.rotation = Quaternion.identity; // Ensure label always faces the camera without rotating
        }
    }
}