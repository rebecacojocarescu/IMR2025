using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowableObject : MonoBehaviour
{
    [Header("Scoring Settings")]
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public int minScore = 10;
    public int maxScore = 100;

    private Vector3 throwPosition;
    private bool hasBeenThrown = false;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to select event (when grabbing the object)
            grabInteractable.selectEntered.AddListener(OnGrab);
            // Subscribe to release event (when throwing the object)
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Save the position where you grab the dart
        throwPosition = transform.position;
        hasBeenThrown = false;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Mark that the object has been thrown
        hasBeenThrown = true;
        Debug.Log($"Object thrown from position: {throwPosition}");
    }

    public int CalculateScore(Vector3 targetPosition)
    {
        if (!hasBeenThrown)
        {
            return 0;
        }

        // Calculate distance between throw point and target
        float distance = Vector3.Distance(throwPosition, targetPosition);

        Debug.Log($"Distance: {distance}m from throw position to target");

        // Normalize distance between 0 and 1
        float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, distance);

        // Calculate score: greater distance = higher score
        int score = Mathf.RoundToInt(Mathf.Lerp(minScore, maxScore, normalizedDistance));

        // Make sure score is within the correct range
        score = Mathf.Clamp(score, minScore, maxScore);

        return score;
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}