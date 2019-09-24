using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    Transform jugador; // Referencia a la posción del jugador.
    NavMeshAgent nav; // Referencia a la malla de navegación del agente.
    JugadorVida jugadorVida; // Referencia al script JugadorVida.cs.
    EnemigoVida enemigoVida; // Referencia al script EnemigoVida.cs.


    void Awake ()
    {
        // Buscamos el GameObject con el tag "Player".
        jugador = GameObject.FindGameObjectWithTag ("Player").transform;
        jugadorVida = jugador.GetComponent <JugadorVida> ();
        enemigoVida = GetComponent<EnemigoVida> ();

        // Obtenemos la referencia del componente NavMeshagent.
        nav = GetComponent <NavMeshAgent> ();
    }

    // Usamos el método Update ya que no hacemos uso de físicas.
    void Update ()
    {
        // Comprobamos si el enemigo y el jugador están vivos.
        if(enemigoVida.vidaActual > 0 && jugadorVida.vidaActual > 0)
        {
            // Si es así, establecemos que el destino del agente es la posición donde está el jugador.
            nav.SetDestination (jugador.position);
        }
        else
        {
            // Deshabilitamos la malla de navegación del agente.
            nav.enabled = false;
        }

    } 
}
