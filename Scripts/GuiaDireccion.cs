using UnityEngine;

public class ApuntarHaciaObjetivo : MonoBehaviour
{
    public Transform objetivo;

    void Update()
    {
        // Verificar si el objetivo está asignado
        if (objetivo != null)
        {
            // Utilizar LookAt para hacer que el objeto mire hacia el objetivo
            transform.LookAt(objetivo);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un objetivo.");
        }
    }
}
