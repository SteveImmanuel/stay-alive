using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Upload()
    {
    }

    public void BackToMenu()
    {
        LevelLoader.instance.ChangeScene(0);
    }
}
