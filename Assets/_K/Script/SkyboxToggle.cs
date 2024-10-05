using UnityEngine;

public class SkyboxToggle : MonoBehaviour
{
    // Store the original skybox material
    private Material originalSkybox;

    // White skybox material
    public Material whiteSkyboxMaterial;

    // Flag to track whether the skybox is white or normal
    private bool isWhiteSkybox = false;

    void Start()
    {
        // Store the current skybox as the original
        originalSkybox = RenderSettings.skybox;

        // Create a white skybox material if it's not assigned
        if (whiteSkyboxMaterial == null)
        {
            whiteSkyboxMaterial = new Material(Shader.Find("RenderFX/Skybox"));
            whiteSkyboxMaterial.SetColor("_Tint", Color.white);
        }
    }

    void Update()
    {
        // Toggle the skybox when the B key is pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isWhiteSkybox)
            {
                // Set the skybox back to the original
                RenderSettings.skybox = originalSkybox;
            }
            else
            {
                // Set the skybox to white
                RenderSettings.skybox = whiteSkyboxMaterial;
            }

            // Toggle the flag
            isWhiteSkybox = !isWhiteSkybox;
        }
    }
}
