using UnityEngine;
using TMPro;  // Ensure this is included for TextMeshPro

public class GameManager : MonoBehaviour
{
    public int playerScore = 0; // Player's score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element that displays the score

    // Method to add points to the score
public void AddPoints(int points)
{
    playerScore += points;
    Debug.Log("Points added: " + points);  // Log the points added
    UpdateScoreText();
}

private void UpdateScoreText()
{
    scoreText.text = "Score: " + playerScore.ToString();
    Debug.Log("Updated score text: " + scoreText.text);  // Log the updated score
}

}