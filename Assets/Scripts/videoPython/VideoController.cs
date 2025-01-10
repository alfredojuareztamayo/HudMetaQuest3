using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Collections.Concurrent;
using TMPro;


public class VideoController : MonoBehaviour
{
    public string videoUrl = "http://192.168.1.83:5000/video/video1.mp4"; // URL del video
    public VideoPlayer videoPlayer;
    //public TMP_Text debug;

    private string localPath;

  

        private static ConcurrentQueue<System.Action> mainThreadActions = new ConcurrentQueue<System.Action>();

        void Start()
        {
            Debug.Log("Inicio de la aplicación");
        //debug.text += "Inicio de la aplicación";
            localPath = Path.Combine(Application.persistentDataPath, "downloadedVideo.mp4");

            if (System.IO.File.Exists(localPath))
            {
                Debug.Log("Eliminando archivo existente: " + localPath);
                System.IO.File.Delete(localPath);
            }
        }

        void Update()
        {
            // Ejecuta acciones en el hilo principal
            while (mainThreadActions.TryDequeue(out var action))
            {
                action();
            }
        }

        public void StartVideo()
        {
            StartCoroutine(DownloadAndPlayVideo());
        //debug.text += "\n inicie corutina";
        }

        private void PlayVideo(string path)
        {
            videoPlayer.url = path;
            videoPlayer.time = 0;
            videoPlayer.Play();
        }

        private IEnumerator DownloadAndPlayVideo()
        {
            UnityWebRequest request = UnityWebRequest.Get(videoUrl);
            request.downloadHandler = new DownloadHandlerFile(localPath);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Video descargado en: " + localPath);
            //debug.text += "\n Video descargado en:  "+ localPath;
            PlayVideo(localPath);
            }
            else
            {
            //debug.text += "\n Error al descargar el video: " + request.error;
            Debug.LogError("Error al descargar el video: " + request.error);
            }
        }

        public static void EnqueueMainThreadAction(System.Action action)
        {
            mainThreadActions.Enqueue(action);
        }
    }
