using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    
    public float velocidad = 6f; // Velocidad con la que se mueve el jugador.

    private Vector3 movimiento; // Movimiento que queremos aplicar al jugador.
    private Animator anim; // Referencia al componente Animator para trabajar con las animaciones.
    private Rigidbody rgbd; // Referencia al componente Rigidbody para trabajar con las físicas.
    private int sueloMask; // Usamos un Layermask que almacena un entero para poder utilizar un RayCast hacia el Quad que creamos en el suelo.
    private float camRayLongitud = 100f;// Longitud del rayo desde la cámara hasta la escena.


    // El método Awake es similar al Start con la diferencia de que se llama independientemente de si el script está activo o no.
    void Awake ()
    {
        // Creamos la Layermask para la capa Floor.
        sueloMask = LayerMask.GetMask ("Floor");

        // Obtenemos las referencias.
        anim = GetComponent <Animator> ();
        rgbd = GetComponent <Rigidbody> ();
    }


    void FixedUpdate ()
    {
        // Almacenamos los ejes de movimiento usando el método GetAxisRaw. El jugador siempre irá con la misma velocidad.
        // GetAxis obtiene valores entre -1 y 1 pero GetAxisRaw solo devuelve -1, 0 o 1. 
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical"); // Realmente nos moveremos por el eje Z.

        // Movemos el jugador por la escena.
        Mover (h, v);

        // Giramos el jugador en función del cursor del ratón.
        Girar ();

        // Animamos el jugador.
        Animar (h, v);
    }

    // Método para mover el jugador por la escena.
    void Mover (float h, float v)
    {
        // Asinamos los valores al Vector3 movimiento. Sólo tenemos componente X y Z la Y es 0.
        movimiento.Set (h, 0f, v);
        
        // Normalizamos el vector ya que podemos movernos por el eje X y Z a la vez y lo multiplicamos por 
        // nuestra velocidad y por cada segundo (deltaTime).
        movimiento = movimiento.normalized * velocidad * Time.deltaTime;

        // Aplicamos el movimiento al jugador pasándole la posición actual más a la que hay que moverlo.
        rgbd.MovePosition (transform.position + movimiento);
    }

    // Método para girar el jugador sobre el eje Y usando el ratón,
    // de tal forma que el jugador siempre mire hacia el cursor del ratón
    void Girar ()
    {
        // Creamos un rayo desde el cursor del ratón en la dirección de la cámara.
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

        // La variable RaycastHit almacena información sobre dónde ha "golpeado" el rayo.
        RaycastHit sueloHit;

        // Comprobamos si el rayo a "golpeado" a algo en el suelo pasándole al método Raycast
        // el origen del rayo (camRay), el destino (sueloHit) usando una variable out para obtener información
        // desde fuera de este método, longitud del rayo (camRayLongitud) y el LayerMask donde está el suelo (sueloMask).
        if(Physics.Raycast (camRay, out sueloHit, camRayLongitud, sueloMask))
        {
            // Creamos un vector desde el jugador hasta el punto donde el rayo ha golpeado desde el ratón.
            Vector3 jugadorAlRaton = sueloHit.point - transform.position;

            // Nos aseguramos que la componente Y sigue siendo 0.
            jugadorAlRaton.y = 0f;

            // Creamos un Quaternion (rotación) del jugador.
            Quaternion nuevaRotacion = Quaternion.LookRotation (jugadorAlRaton);

            // Rotamos al jugador con la nueva rotación obtenida.
            rgbd.MoveRotation (nuevaRotacion);
        }
    }

    // Método para animar el jugador si está andando o no.
    void Animar (float h, float v)
    {
        // Si h o v no es 0 es porque se está moviendo.
        bool andando = h != 0f || v != 0f;

        // Indicamos al "Animator" si se está moviendo o no usando el parámetro "Andando".
        anim.SetBool ("Andando", andando);
    }
}
