using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour
{
    [System.Serializable]
    private class UserData
    {
        public string _id="";
        public string nim = "";
        public string username = "";
        public int score=-1;
    }

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI loadingText;

    void Start()
    {
        getData();
    }

    public void getData()
    {
        nameText.enabled = false;
        scoreText.enabled = false;
        loadingText.enabled = true;
        StartCoroutine(APICall.getAllScore(callback));
    }

    private void callback(string result){
        UserData[] listUserData = JsonHelper.fromJson<UserData>(JsonHelper.fixJson(result));

        StringBuilder usernames = new StringBuilder();
        StringBuilder scores = new StringBuilder();

        int length = listUserData.Length <= 5 ? listUserData.Length : 5;

        for(int i=0;i< length; i++)
        {
            if (i == 0)
            {
                usernames.Append(listUserData[i].username);
                scores.Append(listUserData[i].score);
            }
            else
            {
                usernames.AppendFormat("\n{0}", listUserData[i].username);
                scores.AppendFormat("\n{0}", listUserData[i].score);
            }
        }

        nameText.text = usernames.ToString();
        scoreText.text = scores.ToString();
        nameText.enabled = true;
        scoreText.enabled = true;
        loadingText.enabled = false;
    }
}
