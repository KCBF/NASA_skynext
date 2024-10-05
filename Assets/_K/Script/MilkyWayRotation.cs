using UnityEngine;

public class MilkyWayRotation : MonoBehaviour
{
    // Rotation speed of the Milky Way object (adjustable in the inspector)
    public float rotationSpeed = 1f;

    // Whether the rotation is enabled or not (toggle in the inspector)
    public bool isRotationEnabled = true;

    // Increment step for time scale adjustment (adjustable in the inspector)
    public float timeScaleIncrement = 1f;

    void Update()
    {
        // Handle the rotation of the Milky Way
        HandleMilkyWayRotation();

        // Handle time scale changes via key input (Z to slow down, X to speed up)
        HandleTimeScaleChange();
    }

    void HandleMilkyWayRotation()
    {
        // Only rotate if the rotation is enabled
        if (isRotationEnabled)
        {
            // Rotate around the Y axis (up) at the defined speed (adjust based on Time.deltaTime)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleTimeScaleChange()
    {
        // Increase time scale with the X key
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale += timeScaleIncrement;  // Increase the time scale by the increment set in the inspector
        }

        // Decrease time scale with the Z key, but ensure it doesn't go below 0
        if (Input.GetKeyDown(KeyCode.Z) && Time.timeScale > timeScaleIncrement)
        {
            Time.timeScale -= timeScaleIncrement;  // Decrease the time scale by the increment set in the inspector
        }
    }
}
