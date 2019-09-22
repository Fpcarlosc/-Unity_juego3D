using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorVida : MonoBehaviour
{
    public int vidaInicial = 100; // Cuánta vida tiene el jugador al comenzar el juego.
    public int vidaActual; // La vida actual del jugador.
    public Slider vidaSlider; // Referencia al Slider con la vida del jugador (BarraVida).
    public Image imagenDaño; // Referencia a la imagen del flash rojo cuando el jugador es golpeado (ImagenDaño).
    public float velocidadFlash = 5f; // Velocidad con la que aparece imagenDaño en pantalla.
    public Color colorFlash = new Color(1f, 0f, 0f, 0.1f); // Color de imagenDaño (rojo con un 10% de opacidad).
    public AudioClip audioMuerto; // Audio para cuando el jugador muere.

    private Animator anim; // Referencia al componente Animator.
    private AudioSource playerAudio; // Referencia al componente AudioSource.
    private MovimientoJugador movimientoJugador; // Referencia al script MovimientoJugador.cs.
    private bool muerto; // Si el jugador está muerto o no.
    private bool dañado; // Si el jugador recibe daño o no.


    void Awake ()
    {
        // Obtenemos las referencias.
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        movimientoJugador = GetComponent <MovimientoJugador> (); // Para acceder al script símplemente escribimos su nombre.

        // Inicializamos la vida actual del jugador.
        vidaActual = vidaInicial;
    }


    void Update ()
    {
        // Comprobamos si el jugador es dañado.
        if(dañado)
        {
            // Establecemos el color de imagenDaño al color definido en colorFlash.
            imagenDaño.color = colorFlash; 
        }
        else
        {
            // Realizamos una transición suave del color de imagenDaño a transparente (clear) a la velocidad definida en velocidadFlash.
            imagenDaño.color = Color.Lerp (imagenDaño.color, Color.clear, velocidadFlash * Time.deltaTime);
        }

        // Reseteamos la variable.
        dañado = false;
    }

    // Método que se llamará cuando un enemigo golpee al jugador.
    public void RecibirDaño (int cantidad)
    {
        // Indicamos que se ha dañado al jugador y así en el método Update mostraremos la imagen imagenDaño.
        dañado = true;

        // Reducimos la vida actual según la cantidad de daño recibida.
        vidaActual -= cantidad;

        // Establecemos el valor de la barra de vida a la vida actual.
        vidaSlider.value = vidaActual;

        // Reproducimos el sonido de daño.
        playerAudio.Play ();

        // Comprobamos si la vida del jugador es menor o igual a 0 y éste no está muerto todavía.
        if(vidaActual <= 0 && !muerto)
        {
            // Si es así, muere.
            Muerte ();
        }
    }

    // Método que se llamará cuando el jugador muera. 
    void Muerte ()
    {
        // Indicamos que el jugador está muerto y así no se volverá a llamar el método.
        muerto = true;

        // Indicamos al animator que el jugador está muerto.
        anim.SetTrigger ("Muerto");

        // Indicamos el sonido a reproducir cuando el jugador muere y lo reproducimos.
        playerAudio.clip = audioMuerto;
        playerAudio.Play ();

        // Desabilitamos el script MovimientoJugador.cs.
        movimientoJugador.enabled = false;
    }       
}
