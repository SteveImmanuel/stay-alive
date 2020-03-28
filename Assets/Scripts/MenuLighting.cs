using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLighting : MonoBehaviour
{
    public GameObject brokenLight;
    public float minWaitTime = 0.05f;
    public float maxWaitTime = 0.3f;

    private void Start()
    {
        StartCoroutine(flicker());
    }

    IEnumerator flicker()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                brokenLight.SetActive(false);
                yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
                brokenLight.SetActive(true);
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
