using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionPuntos : MonoBehaviour
{
    public static int puntos; // Puntos del jugador.

    private Text text;  // Referencia al componente Text.

    void Awake ()
    {
        // Obtenemos la referencia.
        text = GetComponent <Text> ();

        // Reseteamos los puntos.
        puntos = 0;
    }

    void Update ()
    {
        // Establecemos el texto a mostrar.
        text.text = "Puntos: " + puntos;
    }
}
