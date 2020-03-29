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
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            AudioManager.instance.play("MainTheme", false);
        }
        else
        {
            //play looping sound game
            AudioManager.instance.play("BackgroundGame", true);
            StartCoroutine(AudioManager.instance.playRandomSound());
        }
        animator = GetComponentInChildren<Animator>();
    }

    public void swapScene(int index)
    {
        if(index > 1) // change to main game
        {
            AudioManager.instance.stop("MainTheme", true);
        }

        if(SceneManager.GetActiveScene().buildIndex > 1) // change from main game
        {
            StopCoroutine(AudioManager.instance.playRandomSound());
            AudioManager.instance.stopAllSound();
        }

        StartCoroutine(changeSceneSequence(index));
    }

    IEnumerator changeSceneSequence(int index)
    {
        animator.SetTrigger("Change");
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene(index);
    }
}
