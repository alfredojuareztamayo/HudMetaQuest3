using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public string[] messages;
    public List<string> messagesList = new List<string>();
   
    public void FillMessage(string toAdd)
    {
        Debug.Log("Entre al llenad");
        messagesList.Add(toAdd);
        ShowMessages();
    }

    public void ShowMessages()
    {
        // Limpiar el texto antes de mostrar los nuevos mensajes
        textMeshPro.text = "";

        // Recorrer la lista y concatenar los mensajes con un salto de línea
        foreach (var message in messagesList)
        {
            textMeshPro.text += message + "\n";  // Agrega el salto de línea después de cada mensaje
        }
    }
}
