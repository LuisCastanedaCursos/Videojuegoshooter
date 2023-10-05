using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMiraController : MonoBehaviour
{

    // Referencia a la cámara del jugador

    public new Transform camera;
    // Sensibilidad de la mira

    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()

    {

        // Bloquear el cursor en la pantalla

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        // Obtener la entrada del mouse en los ejes X e Y

        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        // Si el mouse se movió en el eje X, rotar el jugador en ese sentido

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }
        // Si el mouse se movió en el eje Y, rotar la cámara en ese sentido

        if (ver != 0)
        {               // Obtener la rotación actual de la cámara

            Vector3 rotation = camera.localEulerAngles;
            // Restar la entrada del mouse en el eje Y y sumar 360 para evitar números negativos

            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;



            // Limitar la rotación de la cámara en el eje X entre 80 y 180 grados y entre 280 y 360 grados

            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; }
            else
            if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }
            // Aplicar la rotación actualizada a la cámara

            camera.localEulerAngles = rotation;
        }
    }
}
