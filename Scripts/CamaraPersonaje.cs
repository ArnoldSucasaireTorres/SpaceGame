using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarPersonajeConMouse : MonoBehaviour
{
    public float velocidadRotacionX = 2.0f; // Velocidad de rotación en el eje X
    public float velocidadRotacionY = 2.0f; // Velocidad de rotación en el eje Y
    public Transform elementoExcluido; // Asigna el Transform del elemento a excluir en el Inspector
    
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float maxYRotation = 45.0f; // Límite máximo en grados

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * velocidadRotacionX;
        rotationY -= Input.GetAxis("Mouse Y") * velocidadRotacionY;

        rotationY = Mathf.Clamp(rotationY, -maxYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Si tienes un elemento a excluir, desactívalo para que no rote
        if (elementoExcluido != null)
        {
            elementoExcluido.rotation = Quaternion.identity;         
        }
    }
}
