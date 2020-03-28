using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APICall
{
    private static string URL = "http://134.209.97.218:5051/scoreboards/13517039";

    public static IEnumerator getAllScore(Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            callback(request.downloadHandler.text);
        }
    }

    public static IEnumerator Upload(string name, int score, Action<int> callback)
    {
        WWWForm body = new WWWForm();
        body.AddField("username", name);
        body.AddField("score", score);

        Debug.Log("uploading data with username:" + name + " and score:" + score);

        UnityWebRequest www = UnityWebRequest.Post(URL, body);
        yield return www.SendWebRequest();
        

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            callback(400);
        }
        else
        {
            callback(200);
        }
    }

}
