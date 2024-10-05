using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float timeScaleIncrement = 0.25f;
    public float maxTimeScale = 5f;
    public float minTimeScale = 0.25f;

    void Update()
    {
        HandleTimeScaleChange();
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
}