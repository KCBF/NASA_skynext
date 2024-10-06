using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SpaceGraphicsToolkit;

public class ButtonKeyTrigger : MonoBehaviour
{
    public Button targetButton; // Drag and drop your TextMeshPro button here
    public KeyCode triggerKey; // Dropdown to select the key in the Inspector
    public TimeControl timeControlScript; // Drag and drop the TimeControl script here
    public SgtCameraMove cameraMoveScript; // Drag and drop the CameraMove script here

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(() => HandleButtonClick(triggerKey));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey) && targetButton != null)
        {
            targetButton.onClick.Invoke();
        }
    }

    void HandleButtonClick(KeyCode key)
    {
        Debug.Log($"Button {targetButton.name} clicked via key {key}");

        // Call appropriate methods based on the key pressed
        if (key == KeyCode.Z || key == KeyCode.X)
        {
            if (timeControlScript != null)
            {
                if (key == KeyCode.X)
                {
                    IncreaseTimeScale();
                }
                else if (key == KeyCode.Z)
                {
                    DecreaseTimeScale();
                }
            }
        }
        else if (key == KeyCode.W || key == KeyCode.A || key == KeyCode.S || key == KeyCode.D)
        {
            if (cameraMoveScript != null)
            {
                MoveCamera(key);
            }
        }
    }

    void IncreaseTimeScale()
    {
        if (Time.timeScale < timeControlScript.maxTimeScale)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + timeControlScript.timeScaleIncrement, timeControlScript.minTimeScale, timeControlScript.maxTimeScale);
            timeControlScript.UpdateTimeScaleText();
        }
    }

    void DecreaseTimeScale()
    {
        if (Time.timeScale > timeControlScript.minTimeScale)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - timeControlScript.timeScaleIncrement, timeControlScript.minTimeScale, timeControlScript.maxTimeScale);
            timeControlScript.UpdateTimeScaleText();
        }
    }

    void MoveCamera(KeyCode key)
    {
        Vector3 delta = Vector3.zero;
        float speedMultiplier = Mathf.Lerp(cameraMoveScript.SpeedMin, cameraMoveScript.SpeedMax, cameraMoveScript.SpeedRange);

        switch (key)
        {
            case KeyCode.W:
                delta.z += cameraMoveScript.DepthControls.GetValue(Time.deltaTime);
                break;
            case KeyCode.S:
                delta.z -= cameraMoveScript.DepthControls.GetValue(Time.deltaTime);
                break;
            case KeyCode.A:
                delta.x -= cameraMoveScript.HorizontalControls.GetValue(Time.deltaTime);
                break;
            case KeyCode.D:
                delta.x += cameraMoveScript.HorizontalControls.GetValue(Time.deltaTime);
                break;
        }

        cameraMoveScript.transform.Translate(delta * speedMultiplier, Space.Self);
    }
}