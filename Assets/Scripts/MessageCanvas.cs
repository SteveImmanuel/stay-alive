using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageCanvas : MonoBehaviour
{
    public GameObject opening;
    public GameObject gameOver;

    private Animator animator;
    public Text scoreFinalText;
    public Text waveSurvivedText;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void displayOpening()
    {
        opening.SetActive(true);
        gameOver.SetActive(false);
    }

    public void displayGameOver()
    {
        opening.SetActive(false);
        gameOver.SetActive(true);
    }

    public void hideCanvas()
    {
        animator.SetTrigger("Change");
    }


}
