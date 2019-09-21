using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    Transform jugador; // Referencia a la posición del jugador.
    NavMeshAgent nav; // Referencia a la malla de navegación del agente.


    void Awake ()
    {
        // Buscamos el GameObject con el tag "Player".
        jugador = GameObject.FindGameObjectWithTag ("Player").transform;

        // Obtenemos la referencia del componente NavMeshagent.
        nav = GetComponent <NavMeshAgent> ();
    }

    // Usamos el método Update ya que no hacemos uso de físicas.
    void Update ()
    {
        // Establecemos que el destino del agente es la posición donde está el jugador.
        nav.SetDestination (jugador.position);

    } 
}
