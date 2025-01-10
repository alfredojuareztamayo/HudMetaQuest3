using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoReceiver : MonoBehaviour
{
    private MqttClient client;
    public string mqttBrokerAddress = "127.0.0.1";  // Dirección del broker MQTT
    public string topic = "video/topic";  // Tema de MQTT
    public RawImage videoScreen;  // El RawImage donde se mostrará el video
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Conexión al broker MQTT
        client = new MqttClient(mqttBrokerAddress);
        client.Connect("UnityReceiver");

        if (client.IsConnected)
        {
            Debug.Log("Conectado al broker MQTT");
        }
        else
        {
            Debug.LogError("No se pudo conectar al broker MQTT");
        }

        // Suscripción al tema donde se publican los videos
        client.MqttMsgPublishReceived += OnVideoReceived;
        client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        // Configurar el VideoPlayer
        videoPlayer = videoScreen.gameObject.AddComponent<VideoPlayer>();
        videoPlayer.renderMode = VideoRenderMode.APIOnly;  // Modo de renderizado para trabajar con frames
    }

    private void OnVideoReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // Crear un archivo temporal para guardar el video recibido
        string tempFilePath = Path.Combine(Application.persistentDataPath, "received_video.mp4");
        File.WriteAllBytes(tempFilePath, e.Message);
        Debug.Log("Video recibido y guardado en: " + tempFilePath);

        // Reproducir el video
        PlayVideo(tempFilePath);
    }

    private void PlayVideo(string filePath)
    {
        if (videoPlayer != null)
        {
            videoPlayer.url = filePath;
            videoPlayer.Play();
            Debug.Log("Reproduciendo video desde: " + filePath);
        }
    }

    void OnDestroy()
    {
        // Desconectar del broker MQTT
        if (client.IsConnected)
        {
            client.Disconnect();
            Debug.Log("Desconectado del broker MQTT");
        }
    }
}
