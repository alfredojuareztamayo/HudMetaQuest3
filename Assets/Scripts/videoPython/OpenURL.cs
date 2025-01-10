using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // URL que quieres abrir
    private string url = "http://192.168.1.83:5000/video/video1.mp4";
    // Referencia al material del botón
    private Renderer buttonRenderer;

    // Colores para hover y selección
    public Color defaultColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;
    void Start()
    {
        // Obtén el renderer del botón (debes tener un material asignado)
        buttonRenderer = GetComponent<Renderer>();
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = defaultColor;
        }
    }

    public void ButtonOpenUrl()
    {
        // Abre la URL en el navegador
        Application.OpenURL(url);

        // Cambia el color al ser seleccionado
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = selectedColor;
        }
    }
    public void OnHoverEnter()
    {
        // Cambia el color al pasar el rayo
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = hoverColor;
        }
    }
    public void OnHoverExit()
    {
        // Restaura el color original al salir el rayo
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = defaultColor;
        }
    }
}
