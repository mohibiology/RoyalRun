using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    void Start()
    {
        Time.timeScale = 1f; // Reset time scale just in case
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = finalScore.ToString();
    }
}
