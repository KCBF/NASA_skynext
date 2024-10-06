using UnityEngine;
using UnityEngine.UI;

public class CameraVisit : MonoBehaviour
{
    public Camera mainCamera; // Drag and drop the main camera here
    public Button[] buttons; // Assign buttons in the inspector
    public Transform[] targetObjects; // Assign corresponding target objects in the inspector
    public float followDistance = 10f; // Distance to keep from the target
    public float followHeight = 5f; // Height to keep above the target
    public float followSpeed = 5f; // Speed at which the camera follows

    private Transform currentTarget;

    void Start()
    {
        if (buttons.Length != targetObjects.Length)
        {
            Debug.LogError("Buttons and target objects arrays must have the same length.");
            return;
        }

        // Assign click listeners to buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Local copy for use in the lambda
            buttons[i].onClick.AddListener(() => SetTargetObject(targetObjects[index]));
        }
    }

    void SetTargetObject(Transform target)
    {
        currentTarget = target;
    }

    void LateUpdate()
    {
        if (currentTarget == null || mainCamera == null) return;

        // Calculate the desired position behind the target object
        Vector3 desiredPosition = currentTarget.position - currentTarget.forward * followDistance;
        desiredPosition.y += followHeight;

        // Smoothly move the camera to the desired position
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Look at the target object
        mainCamera.transform.LookAt(currentTarget);
    }
}