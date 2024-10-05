using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraSceneTransition : MonoBehaviour
{
    public float triggerZPosition = 600f; // The z position to trigger the scene change
    public SceneAsset sceneToLoad; // Drag and drop the scene in Inspector

    private string sceneName; // The actual name of the scene to load

    private void Start()
    {
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
        // Check if the camera's z position is greater than the trigger position
        if (transform.position.z > triggerZPosition && !string.IsNullOrEmpty(sceneName))
        {
            // Load the target scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
