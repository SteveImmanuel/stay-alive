using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMuzzle : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    public void onInstantiate()
    {
        Invoke("disableSelf", .65f);
    }

    private void disableSelf()
    {
        gameObject.SetActive(false);
    }
}
