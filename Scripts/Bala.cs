using UnityEngine;
using TMPro;
public class DestruirEnColision : MonoBehaviour
{
    // Este método se llama cuando ocurre una colisión física con otro collider
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CabezaEnemigo"))
        {
        
        }
        if (other.gameObject.CompareTag("CuerpoEnemigo"))
        {
        
        other.gameObject.GetComponent<Unit>().vidaActual -=20.0f;
       
        }
        Destroy(gameObject);
           
    }
}
