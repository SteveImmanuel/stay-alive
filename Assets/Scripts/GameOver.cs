using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text inputName;
    public Text placeHolderName;
    public Button uploadButton;
    public InputField inputField;

    public void Upload()
    {
        string username;
        if (inputName.text == "")
        {
            username = "(noname)";
            placeHolderName.text = username;
        }
        else
        {
            username = inputName.text;
        }

        uploadButton.interactable = false;
        inputField.interactable = false;
        uploadButton.GetComponentInChildren<Text>().text = "UPLOADING";
        StartCoroutine(APICall.Upload(username, ScoreCounter.instance.getScore(), callback));
    }

    private void callback(int resultCode)
    {
        Debug.Log(resultCode);
        if (resultCode == 200)
        {
            uploadButton.GetComponentInChildren<Text>().text = "UPLOADED";
        }
        else
        {
            uploadButton.GetComponentInChildren<Text>().text = "TRY AGAIN";
            inputField.interactable = true;
            uploadButton.interactable = true;
        }
    }

    public void BackToMenu()
    {
        LevelLoader.instance.ChangeScene(0);
    }
}
