using UnityEngine;

public class CustomColorToggle : MonoBehaviour
{
    // Store the Camera reference
    private Camera mainCamera;

    // Flag to track if the background is using the primary or secondary color
    private bool isPrimaryColor = false;

    // Colors to be set from the Inspector
    [SerializeField]
    private Color primaryColor = Color.white;

    [SerializeField]
    private Color secondaryColor = Color.black;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Set the initial background color to the secondary color
        mainCamera.backgroundColor = secondaryColor;
    }

    void Update()
    {
        // Check if the B key is pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isPrimaryColor)
            {
                // Change background to secondary color
                mainCamera.backgroundColor = secondaryColor;
            }
            else
            {
                // Change background to primary color
                mainCamera.backgroundColor = primaryColor;
            }

            // Toggle the flag
            isPrimaryColor = !isPrimaryColor;
        }
    }
}