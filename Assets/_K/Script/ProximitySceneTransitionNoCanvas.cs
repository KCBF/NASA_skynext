using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // <-- Add this for IEnumerator
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProximitySceneTransitionNoCanvas : MonoBehaviour
{
    public Transform targetObject; // The object to check proximity to
    public float proximityDistance = 10f; // The distance threshold for the transition
    public SceneAsset sceneToLoad; // Drag and drop the scene in Inspector
    public float fadeDuration = 1.0f; // Duration of the fade effect

    private string sceneName; // The name of the scene to load
    private bool isFading = false; // Track if the fading process has started
    private Color originalColor; // Store the original camera background color
    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        // Cache the main camera and its background color
        mainCamera = GetComponent<Camera>();
        if (mainCamera != null)
        {
            originalColor = mainCamera.backgroundColor;
        }

        // Ensure we have the correct scene name from the SceneAsset
        #if UNITY_EDITOR
        if (sceneToLoad != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneToLoad);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath); // Get only the scene name
        }
        else
        {
            Debug.LogError("Scene to load has not been set.");
        }
        #endif
    }

    private void Update()
    {
        // Check the distance between the camera and the target object
        if (targetObject != null && !isFading)
        {
            float distance = Vector3.Distance(transform.position, targetObject.position);

            // If the distance is less than the proximity distance, start fading and change scene
            if (distance < proximityDistance)
            {
                StartCoroutine(FadeAndLoadScene());
            }
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;
        
        // Fade to black by adjusting the camera's background color
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float fadeValue = Mathf.Lerp(0, 1, timer / fadeDuration);
            mainCamera.backgroundColor = Color.Lerp(originalColor, Color.black, fadeValue); // Fade from original color to black
            yield return null;
        }

        mainCamera.backgroundColor = Color.black; // Ensure it's fully black at the end of the fade

        // Load the target scene if we have a valid scene name
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not valid.");
        }
    }
}
