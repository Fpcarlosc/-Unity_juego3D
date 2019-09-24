using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoVida : MonoBehaviour
{
    public int vidaInicial = 100; // Cuánta vida tiene el enemigo al comenzar el juego.
    public int vidaActual; // La vida actual del enemigo.
    public float velocidadHundir = 2.5f; // Velocidad con la que el enemigo se hunde en el suelo cuando muere.
    public int puntuacion = 10; // Puntuación que se obtiene cuando muere cuando este enemigo muere.
    public AudioClip audioMuerto; // Audio para cuando el enemigo muere.

    private Animator anim; // Referencia al componente Animator.
    private AudioSource enemigoAudio; // Referencia al componente AudioSource.
    private ParticleSystem hitParticles; // Referencia al sistema de partículas asociado al enemigo que se reproduce cuando éste es dañado.
    private CapsuleCollider capsuleCollider; // Referencia al componente Capsule Collider.
    private bool muerto; // Si el enemigo está muerto o no.
    private bool hundiendose; // Si el enemigo ha empezado a hundirse en el suelo o no.


    void Awake ()
    {
        // Obtenemos las referencias.
        anim = GetComponent <Animator> ();
        enemigoAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> (); // Buscará entre todos los componentes hijos.
        capsuleCollider = GetComponent <CapsuleCollider> ();

        // Inicializamos la vida actual del enemigo.
        vidaActual = vidaInicial;
    }

    void Update ()
    {
        // Comprobamos si el enemigo debe empezar a hundirse en el suelo.
        if(hundiendose)
        {
            // Si es así, lo movemos debajo del suelo a la velocidad definida en velocidadHundir por segundo.
            transform.Translate (-Vector3.up * velocidadHundir * Time.deltaTime);
        }
    }

    // Método que se llamará cuando el jugador golpee al enemigo.
    // No solo se le pasa la cantidad de daño recibida sino el punto donde se le ha golpeado.
    public void RecibirDaño (int cantidad, Vector3 puntoGolpeado)
    {
        // Comprobamos si el enemigo está muerto
        if(muerto)
            // Si es así, no necesitamos hacer nada.
            return;

        // Reproducimos el sonido de daño.
        enemigoAudio.Play ();

        // Reducimos la vida actual según la cantidad de daño recibida.
        vidaActual -= cantidad;
            
        // Establecemos la posición del sistema de partículas donde ha sido golpeado.
        hitParticles.transform.position = puntoGolpeado;

        // Reproducimos las partículas.
        hitParticles.Play();

        // Comprobamos si la vida del enemigo es menor o igual a 0.
        if(vidaActual <= 0)
        {
            // Si es así, muere.
            Muerte ();
        }
    }

    // Método que se llamará cuando el enemigo muera. 
    void Muerte ()
    {
        // Indicamos que el enemigo está muerto y así no se volverá a llamar al método.
        muerto = true;

        // Convertimos el collider en un trigger así se podrá pasar a través de él.
        capsuleCollider.isTrigger = true;

        // Indicamos al animator que el enemigo está muerto.
        anim.SetTrigger ("Muerto");

        //  // Indicamos el sonido a reproducir cuando el enemigo muere y lo reproducimos.
        enemigoAudio.clip = audioMuerto;
        enemigoAudio.Play ();
    }

    // Método para que el enemigo se hunda en el suelo al morirse.
    public void EmpezarHundimiento ()
    {
        // Obtenemos la referencia al componente NavMeshAgent y lo desactivamos.
        GetComponent <NavMeshAgent> ().enabled = false;

        // Obtenemos la referencia al componente Rigidbody y lo convertimos en kinematic.
        // Esto es necesario ya que en el método Update usamos el método Translate y vamos a mover un Collider
        // si lo convertimos en kinematic Unity no recalculará la geometría de nuevo.
        GetComponent <Rigidbody> ().isKinematic = true;

        // Indicamos que el enemigo se está hundiendo.
        hundiendose = true;

        // Incrementamos los puntos obtenidos al matar al enemigo.
        GestionPuntos.puntos += puntuacion;

        // Después de 2 segundos, destruimos el GameObject del enemigo.
        Destroy (gameObject, 2f);
    }
}
