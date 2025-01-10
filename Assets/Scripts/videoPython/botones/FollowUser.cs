using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUser : MonoBehaviour
{
    public Transform userHead;  // Asigna la cámara principal del VR al editor
    public float distance;

    void Update()
    {
        // Calcula la posición del botón delante del usuario
        Vector3 targetPosition = userHead.position + userHead.forward * distance;
        targetPosition.y = userHead.position.y;  // Mantén la misma altura

        // Mueve el botón suavemente hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        // Ajusta la rotación para evitar la inversión del botón
        Vector3 lookDirection = transform.position - userHead.position;
        lookDirection.y = 0;  // Mantén la rotación horizontal fija
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
