using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System.Collections;
using System.Collections.Generic;

public class MQTTReceiver : MonoBehaviour
{
    private MqttClient client;
    public MessageManager messageManager;
    public string Camara;
    public string Camara2;

    void Start()
    {
        try
        {
            string brokerAddress = "broker.emqx.io";
            client = new MqttClient(brokerAddress);

            // Usar protocolo por defecto o especificar si es necesario
            client.ProtocolVersion = MqttProtocolVersion.Version_3_1_1;

            string clientId = System.Guid.NewGuid().ToString();
            client.Connect(clientId);

            Debug.Log("Conexión exitosa a MQTT");

            string[] topics = { "esp32/hola", "esp32/adios" };
            foreach (var topic in topics)
            {
                try
                {
                    client.Subscribe(new string[] { topic },
                                     new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                    Debug.Log($"Suscripción a {topic} exitosa");

                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error al suscribirse a {topic}: {ex.Message}");
                }
            }

            client.MqttMsgPublishReceived += OnMessageReceived;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error de conexión: {ex.Message}");
        }
    }

    void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = Encoding.UTF8.GetString(e.Message);
        Debug.Log($"Mensaje recibido en {e.Topic}: {message}");
        SwitchString(message);
        // Enviar la llamada a UnityMainThreadDispatcher
        UnityMainThreadDispatcher.Enqueue(() => messageManager.FillMessage(message));
    }
    void SwitchString(string str)
    {
        switch (str)
        {
            case "Chechar camara 1":
                Camara = str;
            break;
            case "Chechar camara 2":
                Camara = str;
                break;
        }

    }
    IEnumerator UpdateUI(string message)
    {
        // Asegúrate de que esta función se ejecute en el hilo principal
        yield return null;  // Permite que Unity termine el ciclo de actualización del frame actual

        // Luego actualizas el UI (por ejemplo, agregas el mensaje a tu TextMeshPro)
        messageManager.FillMessage(message);
    }
    void OnDestroy()
    {
        if (client != null && client.IsConnected)
        {
            client.Disconnect();
        }
    }
}