
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using System.IO;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Collections;
using System.Text;


public class TextReceiver : MonoBehaviour
{
    private MqttClient client;
    private string brokerAddress = "broker.emqx.io";
    public VideoController videoController;

    void Start()
    {
        client = new MqttClient(brokerAddress);
        client.Connect("UnityTextReceiver");
        client.MqttMsgPublishReceived += OnMessageReceived;
        client.Subscribe(new string[] { "text/topic" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        Debug.Log("Conectado al broker público MQTT y suscrito al tema.");
    }

    private void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string receivedMessage = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Mensaje recibido: {receivedMessage}");

        // Ejecutar en el hilo principal
        VideoController.EnqueueMainThreadAction(() => videoController.StartVideo());
    }
}
