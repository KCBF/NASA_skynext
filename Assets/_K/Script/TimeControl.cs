using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour
{
    public float timeScaleIncrement = 0.25f;
    public float maxTimeScale = 200f;
    public float minTimeScale = 0.0f;
    public TextMeshProUGUI timeScaleText; // Drag and drop TextMeshPro field in Inspector

    void Update()
    {
        HandleTimeScaleChange();
        UpdateTimeScaleText();
    }

    void HandleTimeScaleChange()
    {
        // Increase time scale with the X key
        if (Input.GetKeyDown(KeyCode.X) && Time.timeScale < maxTimeScale)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + timeScaleIncrement, minTimeScale, maxTimeScale);
        }

        // Decrease time scale with the Z key, but ensure it doesn't go below minTimeScale
        if (Input.GetKeyDown(KeyCode.Z) && Time.timeScale > minTimeScale)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - timeScaleIncrement, minTimeScale, maxTimeScale);
        }
    }

    public void UpdateTimeScaleText()
    {
        if (timeScaleText != null)
        {
            timeScaleText.text = Time.timeScale.ToString("F2");
        }
    }
}