using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("GameOver");

    }
}
