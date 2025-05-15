using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaders : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("ss");
    }
}
