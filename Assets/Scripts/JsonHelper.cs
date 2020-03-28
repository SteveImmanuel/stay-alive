using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] fromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string fixJson(string text)
    {
        return "{\"items\":" + text + "}";
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
