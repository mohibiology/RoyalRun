using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHome : MonoBehaviour
{
    public string start = "start"; // Make sure your scene is named exactly "start"

    public void GoToHome()
    {
        SceneManager.LoadScene(start);
    }
}
