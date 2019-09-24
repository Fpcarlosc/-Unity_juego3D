using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{
    public float tiempoEntreAtaques = 0.5f; // Tiempo en segundos que transcurre entre cada ataque.
    public int dañoAtaque = 10; // Cantidad de vida que le quita el ataque al jugador .

    private Animator anim; // Referencia al componente Animator.
    private GameObject player; // Referencia al GameObject player.
    private JugadorVida jugadorVida; // Referencia al script JugadorVida.cs.
    private bool jugadorEnRango; // Si el jugador está en rango (los Collider se solapan) y puede ser atacado.
    private float timer; // Timer para controlar el siguiente ataque.
    private EnemigoVida enemigoVida; // Referencia al script EnemigoVida.cs.


    void Awake ()
    {
        // Obtenemos las referencias.
        player = GameObject.FindGameObjectWithTag ("Player"); // Buscamos el GameObject etiquetado como "Player".
        jugadorVida = player.GetComponent <JugadorVida> (); // y a partir de él accedemos al script JugadorVida.cs.
        anim = GetComponent <Animator> ();
        enemigoVida = GetComponent<EnemigoVida>();
    }

    // El método OnTriggerEnter es llamado cuando el Collider asociado y otro se tocan.
    void OnTriggerEnter (Collider other)
    {
        // Comprobamos si el Collider es el del jugador.
        if(other.gameObject == player)
        {
            // Si es así, el jugador está en el rango de ataque.
            jugadorEnRango = true;
        }
    }

    // El método OnTriggerExit es llamado cuando el Collider asociado y otro dejan de tocarse.
    void OnTriggerExit (Collider other)
    {
        // Comprobamos si el Collider es el del jugador.
        if(other.gameObject == player)
        {
            // Si es así, el jugador está fuera del rango de ataque.
            jugadorEnRango = false;
        }
    }

    // En el método Update es donde se realiza el ataque.
    void Update ()
    {
        // Actualizamos el tiempo desde la última llamada.
        timer += Time.deltaTime;

        // Comprobamos si el timer excede o es igual al tiempo entre ataques y además el jugador está en rango y
        // la vida del enemigo es mayor que 0. 
        if(timer >= tiempoEntreAtaques && jugadorEnRango && enemigoVida.vidaActual > 0)
        {
            // Si es así, atacamos.
            Attack ();
        }

        // Comprobamos si después del ataque el jugador tiene 0 o menos nivel de vida
        if(jugadorVida.vidaActual <= 0)
        {
            // Si es así, indicamos al animator que el jugador está muerto.
            anim.SetTrigger ("JugadorMuerto");
        }
    }

    // Método que realiza el ataque del enemigo.
    void Attack ()
    {
        // Reseteamos el timer.
        timer = 0f;

        // Comprobamos si el jugador tiene vida
        if(jugadorVida.vidaActual > 0)
        {
            // Si es así, le quitamos vida.
            jugadorVida.RecibirDaño (dañoAtaque);
        }
    }
}
