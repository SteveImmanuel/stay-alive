using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator animator;
    public static LevelLoader instance;
    public float waitingTime = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 1) // not in main menu or highscore board
        {
            AudioManager.instance.stopMainTheme();
        }
        else
        {
            AudioManager.instance.playMainTheme();
        }
        animator = GetComponentInChildren<Animator>();
    }

    public void ChangeScene(int index)
    {
        StartCoroutine(changeScene(index));
    }

    IEnumerator changeScene(int index)
    {
        animator.SetTrigger("Change");
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene(index);
    }
}
