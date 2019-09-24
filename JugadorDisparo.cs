using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorDisparo : MonoBehaviour
{
    public int dañoPorTiro = 20; // Daño que inflige cada disparo.
    public float tiempoEntreDisparos = 0.15f; // Tiempo entre cada disparo.
    public float rango = 100f; // Distancia a la que llega el disparo.

    private float timer;  // Timer para determinar cuándo disparar.
    private Ray rayoDisparo; // Rayo desde el final del arma.
    private RaycastHit disparoHit; // Obtiene información sobre que ha golpeado el disparo.
    private int shootableMask; // El rayo solo golpeará a elementos que estén en la capa shootable.
    private ParticleSystem armaParticles; // Referencia al sistema de partículas.
    private LineRenderer armaLine; // Referencia al componente Line Renderer.
    private AudioSource armaAudio; // Referencia al componente Audio Source.
    private Light armaLight; // Referencia al componente Light.
    private float tiempoMostrarEfectos = 0.2f; // Tiempo en el que se mostrarán los efectos.

    void Awake ()
    {
        // Creamos el Layer Mask para la capa Shootable.
        shootableMask = LayerMask.GetMask ("Shootable");

        // Obtenemos las referencias.
        armaParticles = GetComponent<ParticleSystem> ();
        armaLine = GetComponent <LineRenderer> ();
        armaAudio = GetComponent<AudioSource> ();
        armaLight = GetComponent<Light> ();
    }

    // En el método Update controlamos su podemos disparar o no.
    void Update ()
    {
        // Actualizamos el tiempo desde la última llamada.
        timer += Time.deltaTime;

        // Comprobamos si se ha pulsado el botón Fire1 y si podemos disparar.
        if(Input.GetButton ("Fire1") && timer >= tiempoEntreDisparos)
        {
            // Si es así, disparamos.
            Disparar ();
        }

        // Comprobamos si el timer ha excedido el tiempo entre disparos en el que se habrán mostrado los efectos.
        if(timer >= tiempoEntreDisparos * tiempoMostrarEfectos)
        {
            // si es así, deshabilitamos los efectos.
            DeshabilitarEfectos ();
        }
    }

    // Método para deshabilitar los efectos (Line Render y Light).
    public void DeshabilitarEfectos ()
    {
        armaLine.enabled = false;
        armaLight.enabled = false;
    }

    // Método en el que se aplican las físicas del disparo.
    void Disparar ()
    {
        // Reseteamos el timer.
        timer = 0f;

        // Reproducimos el clip de disparo.
        armaAudio.Play ();

        // Activamos la luz del arma.
        armaLight.enabled = true;

        // Paramos las partículas del arma si se estaban reproduciendo y las reproducimos de nuevo.
        armaParticles.Stop ();
        armaParticles.Play ();

        // Activamos el Line Renderer y establecemos su primera posición (índice 0) en el final del arma (GunBarrelEnd).
        armaLine.enabled = true;
        armaLine.SetPosition (0, transform.position);

        // A partir de aquí calculamos el segundo punto del Line Renderer.

        // Establecemos el origen del rayo y la dirección que tiene que seguir. 
        rayoDisparo.origin = transform.position;
        rayoDisparo.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        // Comprobamos si el rayo ha "golpeado" a algo en la capa Shootable pasándole al método Raycast
        // el origen del rayo (rayoDisparo), el destino (disparoHit) usando una variable out para obtener información
        // desde fuera de este método, longitud del rayo (rango) y el LayerMask donde está algún elemento Shootable (shootableMask).
        if(Physics.Raycast (rayoDisparo, out disparoHit, rango, shootableMask))
        {
            // Buscamos si en el elemento que ha golpeado el rayo (disparoHit) existe el script EnemigoVida.
            EnemigoVida enemigoVida = disparoHit.collider.GetComponent <EnemigoVida> ();

            // Si el script existe.
            if(enemigoVida != null)
            {
                // Es un enemigo y recibe su daño pasándole la cantidad del daño y el punto donde le ha dado.
                enemigoVida.RecibirDaño (dañoPorTiro, disparoHit.point);
            }

            // Establecemos la segunda posición (índice 1) del Line Renderer en el que golpea el rayo.
            armaLine.SetPosition (1, disparoHit.point);
        }
        // Si el rayo no ha golpeado nada asociado a la capa Shootable
        else
        {
            // Establecemos la segunda posición del Line Renderer como el rango completo del rayo.
            armaLine.SetPosition (1, rayoDisparo.origin + rayoDisparo.direction * rango);
        }
    }
}
