using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionEnemigos : MonoBehaviour
{
    public JugadorVida jugadorVida; // Referencia al script JugadorVida.cs.
    public GameObject enemigo; // Referencia al Prefab del enemigo para ser generado.
    public float tiempogeneracion = 3f; // Tiempo entre generación de un enemigo y otro (3 segundos).
    public Transform lugargeneracion; // Lugar de la escena donde queremos generar los enemigos.


    void Start ()
    {
        // Llamada al método GenerarEnemigo después del tiempo de generación definidos y a partir de la primera llamada, 
        // cada tiempogeneracion volverá a realiazar una nueva llamada.
        InvokeRepeating ("GenerarEnemigo", tiempogeneracion, tiempogeneracion);
    }

    // Método para generar los enemigos en una posición de la escena especificada.
    void GenerarEnemigo ()
    {
        // Comprobamos si el jugador no tiene vida.
        if(jugadorVida.vidaActual <= 0f)
        {
            //Si es así, salimos del método.
            return;
        }

        // Creamos una instancia del Prefab del enemigo en la posición y con la rotación indicadas.
        Instantiate (enemigo, lugargeneracion.position, lugargeneracion.rotation);
    }
}
