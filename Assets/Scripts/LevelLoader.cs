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
