using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform objetivo; // Posición del objetivo al que va a seguir la cámara.
    public float velocidadSeguimiento = 5f; // Velocidad con la que la cámara va a seguir al objetivo.

    private Vector3 distancia; // Distancia de la cámara al objetivo.

    // Start es llamado antes de actualizarse el primer frame
    void Start()
    {
        // Distancia inicial
        distancia = transform.position - objetivo.position;
    }

    // FixedUpdate será el método encargado de mover la cámara calculando una nueva ditancia en cada frame.
    void FixedUpdate ()
    {
        // Calcula la posición de la cámara en función de la posición del objetivo más la distancia inicial.
        Vector3 PosCamaraAObjetivo = objetivo.position + distancia;

        // El método Lerp realiza movimientos suaves entre dos puntos. 
        // En este caso entre la posición de la cámara (transform.position) y la del objetivo (PosCamaraAObjetivo) 
        // especificando la velocidad (velocidadSeguimiento * Time.deltaTime).
        transform.position = Vector3.Lerp (transform.position, PosCamaraAObjetivo, velocidadSeguimiento * Time.deltaTime);
    }
}
