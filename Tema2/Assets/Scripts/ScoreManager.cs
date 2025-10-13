using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalScoreText;

    [Header("Score Settings")]
    public float displayDuration = 2f;

    private int totalScore = 0;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "";
        }

        UpdateTotalScore();
    }

    public void AddScore(int score)
    {
        totalScore += score;

        // Display the score obtained
        if (scoreText != null)
        {
            scoreText.text = $"+{score} points!";
            StopAllCoroutines();
            StartCoroutine(HideScoreAfterDelay());
        }

        // Update total score
        UpdateTotalScore();

        Debug.Log($"Score added: {score}. Total Score: {totalScore}");
    }

    void UpdateTotalScore()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = $"Total Score: {totalScore}";
        }
    }

    System.Collections.IEnumerator HideScoreAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (scoreText != null)
        {
            scoreText.text = "";
        }
    }

    public void ResetScore()
    {
        totalScore = 0;
        UpdateTotalScore();
        if (scoreText != null)
        {
            scoreText.text = "";
        }
    }
}