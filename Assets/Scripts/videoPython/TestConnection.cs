using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class TestConnection : MonoBehaviour
{
    public TMP_Text debug;
    void Start()
    {
        StartCoroutine(CheckConnection());
        debug.text += "\n inicie rutina";
    }

    IEnumerator CheckConnection()
    {
        debug.text += "\n generando solucitud";
        UnityWebRequest request = UnityWebRequest.Get("http://192.168.1.83:5000/video/video1.mp4");
        yield return request.SendWebRequest();
        debug.text += "\n " + request.result;
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + request.error);
            debug.text += "Error: " + request.error;
        }
        else
        {
            Debug.Log("Video reachable!");
            debug.text += "Video reachable!";
        }
    }
}
