using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionFinJuego : MonoBehaviour
{
    public JugadorVida jugadorVida; // Referencia al script JugadorVida.cs.

    private Animator anim; // Referencia al componente Animator.


    void Awake ()
    {
        // Obtenemos la referencia.
        anim = GetComponent <Animator> ();
    }


    void Update ()
    {
        // Comprobamos si el jugador tiene vida.
        if(jugadorVida.vidaActual <= 0)
        {
            // Si no es así, activamos el trigger FinJuego.
            anim.SetTrigger ("FinJuego");
        }
        
    }
}
