using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> dict;

    public static ObjectPooler instance;

    private void Awake()
    {
        if(instance == null)
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
        dict = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            dict.Add(pool.tag, objPool);
        }
    }

    public GameObject instantiateFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!dict.ContainsKey(tag))
        {
            Debug.LogWarning("Tag " + tag + " doesn't exist in pool dictionary");
            return null;
        }

        GameObject obj = dict[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        IPooledObject objInterface = obj.GetComponent<IPooledObject>();
        if (objInterface != null)
        {
            objInterface.onInstantiate();
        }

        dict[tag].Enqueue(obj);

        return obj;
    }
}
