using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimientoController : MonoBehaviour
{

     // Referencia al componente Rigidbody del jugador
    private new Rigidbody rb;

    // Velocidad de movimiento del jugador
    public float movementSpeed;
    // Fuerza del salto
    public float fuerzaDeSalto = 10f; 
     // Controla si el personaje puede saltar
    private bool puedeSaltar = true;

 
   // Start se llama antes del primer frame update
    void Start()
    {     // Obtener el componente Rigidbody del jugador
        rb = GetComponent<Rigidbody>();
        
        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

     // Update se llama una vez por frame
    void Update()
    {
        // Obtener la entrada de los ejes horizontal y vertical
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
          // Calcular la velocidad del  movimiento inicial del jugador
        Vector3 velocity = Vector3.zero;

         
        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
/*La dirección se calcula sumando el vector hacia adelante del objeto (multiplicado por la entrada vertical) 
y el vector hacia la derecha del objeto (multiplicado por la entrada horizontal). Luego, se normaliza el resultado para obtener una dirección unitaria.
Finalmente, se calcula la velocidad del movimiento del jugador multiplicando la dirección por la velocidad de movimiento del jugador:*/

            velocity = direction * movementSpeed;
        }


        // Comprobar si se ha pulsado la tecla de espacio para saltar
 if (Input.GetKeyDown(KeyCode.Space))
        {
            // Comprobar si el personaje puede saltar
            if (puedeSaltar)
            {
                // Aplicar una fuerza hacia arriba para el salto
                rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.Impulse);
                puedeSaltar = false; // Evitar múltiples saltos con una sola pulsación de espacio
            }
        }

        // Verificar si se toca la pantalla (para dispositivos móviles)
        if (Input.touchCount > 0)
        {
            // Obtener el primer toque
            Touch primerToque = Input.GetTouch(0);

            // Verificar si el toque está en la mitad inferior de la pantalla (opcional)
            if (primerToque.position.y < Screen.height / 2)
            {
                // Comprobar si el personaje puede saltar y si es un toque nuevo
                if (puedeSaltar && primerToque.phase == TouchPhase.Began)
                {
                    // Aplicar una fuerza hacia arriba para el salto
                    rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.Impulse);
                    puedeSaltar = false; // Evitar múltiples saltos con un solo toque
                }
            }
        }


        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Permitir saltar nuevamente cuando el personaje toca el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            puedeSaltar = true;
        }
    }
}


















