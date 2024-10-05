using UnityEngine;

public class WhiteToggle : MonoBehaviour
{
    // Store the Camera reference
    private Camera mainCamera;

    // Flag to track if the background is white or black
    private bool isWhiteBackground = false;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Set the initial background color to black (if not already set)
        mainCamera.backgroundColor = Color.black;
    }

    void Update()
    {
        // Check if the B key is pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isWhiteBackground)
            {
                // Change background to black
                mainCamera.backgroundColor = Color.black;
            }
            else
            {
                // Change background to white
                mainCamera.backgroundColor = Color.white;
            }

            // Toggle the flag
            isWhiteBackground = !isWhiteBackground;
        }
    }
}
