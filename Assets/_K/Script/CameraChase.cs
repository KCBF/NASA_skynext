using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public Camera mainCamera; // Drag and drop the main camera here
    public Transform targetObject; // Object to follow
    public float followDistance = 10f; // Distance to keep from the target
    public float followHeight = 5f; // Height to keep above the target
    public float followSpeed = 5f; // Speed at which the camera follows

    void Start()
    {
        if (mainCamera != null)
        {
            // Disable any existing components that might interfere with the chase functionality
            foreach (var component in mainCamera.GetComponents<MonoBehaviour>())
            {
                if (component != this)
                {
                    component.enabled = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (targetObject == null || mainCamera == null) return;

        // Calculate the desired position behind the target object
        Vector3 desiredPosition = targetObject.position - targetObject.forward * followDistance;
        desiredPosition.y += followHeight;

        // Smoothly move the camera to the desired position
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Look at the target object
        mainCamera.transform.LookAt(targetObject);
    }
}