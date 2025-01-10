using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUser : MonoBehaviour
{
    public Transform userHead;  // Asigna la c�mara principal del VR al editor
    public float distance;

    void Update()
    {
        // Calcula la posici�n del bot�n delante del usuario
        Vector3 targetPosition = userHead.position + userHead.forward * distance;
        targetPosition.y = userHead.position.y;  // Mant�n la misma altura

        // Mueve el bot�n suavemente hacia la posici�n objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

        // Ajusta la rotaci�n para evitar la inversi�n del bot�n
        Vector3 lookDirection = transform.position - userHead.position;
        lookDirection.y = 0;  // Mant�n la rotaci�n horizontal fija
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
