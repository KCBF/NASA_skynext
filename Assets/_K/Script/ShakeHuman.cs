using UnityEngine;

public class ShakeHuman : MonoBehaviour
{
    public float shakeSpeed = 2.0f;    // Speed of the shaking motion
    public float shakeAmount = 0.1f;   // Amount of shaking (how far it moves left and right)

    private Vector3 originalLocalPosition;
    private float shakeOffset;

    void Start()
    {
        // Store the original local position of the human object
        originalLocalPosition = transform.localPosition;
        // Randomize the shakeOffset to make multiple objects shake out of sync
        shakeOffset = Random.Range(0.0f, Mathf.PI * 2);
    }

    void Update()
    {
        // Calculate the new local position based on a sine wave
        float offset = Mathf.Sin(Time.time * shakeSpeed + shakeOffset) * shakeAmount;
        transform.localPosition = originalLocalPosition + new Vector3(offset, 0, 0);
    }
}