using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int score = 0;

    public void increaseScore(int amount)
    {
        score+=amount;
        scoreText.text=score.ToString();
    }
}
