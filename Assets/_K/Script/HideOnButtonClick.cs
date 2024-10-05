using UnityEngine;
using UnityEngine.UI;

public class HideOnButtonClick : MonoBehaviour
{
    [Header("Assign Button and Target Object in Inspector")]
    public Button targetButton; // Button to be clicked
    public GameObject targetObject; // Object to be toggled

    void Start()
    {
        if (targetButton != null && targetObject != null)
        {
            // Add listener to the button's onClick event
            targetButton.onClick.AddListener(ToggleTargetObject);
        }
        else
        {
            Debug.LogWarning("Please assign both Target Button and Target Object in the inspector.");
        }
    }

    void ToggleTargetObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf); // Toggle the target object's visibility
        }
    }
}