using UnityEngine;
using UnityEngine.Events;

public class TargetHit : MonoBehaviour
{
    [Header("Visual Feedback")]
    public Color hitColor = Color.green;
    public float colorDuration = 0.5f;

    [Header("Events")]
    public UnityEvent<int> onTargetHit;

    private Renderer targetRenderer;
    private Color originalColor;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that hit the target has the "Throwable" tag
        if (other.CompareTag("Throwable"))
        {
            ThrowableObject throwable = other.GetComponent<ThrowableObject>();

            if (throwable != null)
            {
                // Calculate score based on distance
                int score = throwable.CalculateScore(transform.position);

                Debug.Log($"Target Hit! Score: {score}");

                // Trigger event for score
                onTargetHit?.Invoke(score);

                // Visual feedback
                StartCoroutine(HitFeedback());
            }
        }
    }

    System.Collections.IEnumerator HitFeedback()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = hitColor;
            yield return new WaitForSeconds(colorDuration);
            targetRenderer.material.color = originalColor;
        }
    }
}