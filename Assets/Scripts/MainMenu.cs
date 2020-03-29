using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        LevelLoader.instance.swapScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void HighScore()
    {
        LevelLoader.instance.swapScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        LevelLoader.instance.swapScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
