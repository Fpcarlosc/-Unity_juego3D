# Unity_juego3D
En las diferentes ramas del repositorio podréis encontrar los scripts necesarios para el juego 3D del curso. 
## Rama Parte1

+ _MovimientoJugador.cs_: Configuración del movimiento del jugador mediante el teclado para desplazarse por el escenario y el ratón para girarse sobre sí mismo.
## Rama Parte2

+ _CamaraSeguimiento.cs_: Configuración de la cámara para que siga al jugador.
+ _MovimientoEnemigo.cs_: Configuración del movimiento del enemigo para que siga al jugador haciendo uso del componente  _NavMeshAgent_.
## Rama Parte3

+ _JugadorVida.cs_: Actualización de la vida del jugador al recibir daño y comprobación si éste muere.
+ _AtaqueEnemigo.cs_: Configuración del ataque del enemigo.
## Rama Parte4

+ _EnemigoVida.cs_: Actualización de la vida del enemigo al recibir daño, comprobación si éste muere y obtención de puntos asociados.
+ _AtaqueEnemigo.cs_: Modificación del script de la _Parte 3_ para adaptar el ataque en función de la vida del enemigo.
+ _JugadorDisparo.cs_: Configuración del disparo del jugador reduciendo la vida del enemigo si éste es alcanzado.
+ _MovimientoEnemigo.cs_: Modificación del script de la _Parte 2_ para subsanar el error en la navegación cuando el enemigo muere.
+ _JugadorVida.cs_: Modificación del script de la _Parte 3_ para evitar que si el jugador muere no pueda disparar más.
+ _GestionPuntos.cs_: Configuración del texto con los puntos obtenidos que aparece por pantalla.
