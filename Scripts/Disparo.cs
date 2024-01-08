using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoverBala : MonoBehaviour
{
    private AudioSource[] misAudioSources;

    public GameObject balaPrefab;
    public Camera camara; // Puedes asignar la cámara desde el Inspector de Unity
    public float fuerzaBala = 1.0f; // Puedes ajustar la fuerza según tus necesidades
    public int cantProyectilesReserva = 200;
    public int cantProyectiles = 200;
    public int cantProyectilesEstatico = 200;
    void Start(){
        misAudioSources = GetComponents<AudioSource>();


        GameObject objetoEncontrado = GameObject.Find("BalasContador");
        TextMeshProUGUI puntaje = objetoEncontrado.GetComponent<TextMeshProUGUI>();
        puntaje.text=""+(cantProyectiles);

        GameObject objetoEncontrado2 = GameObject.Find("BalasContadorEstatico");
        TextMeshProUGUI puntaje2 = objetoEncontrado2.GetComponent<TextMeshProUGUI>();
        //int cantTemp = int.Parse(puntaje2.text);
        puntaje2.text="/"+(cantProyectilesReserva);
        puntaje2.alignment = TextAlignmentOptions.Left;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Cuando se hace clic con el botón izquierdo del mouse
        {
            if(cantProyectiles > 0){
                misAudioSources[0].Play();
                Disparar();
            }
            
        }
        if(cantProyectiles < cantProyectilesEstatico && cantProyectiles >=0){
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                int proyectilesreservatemp=cantProyectilesEstatico - cantProyectiles;
                if(proyectilesreservatemp > cantProyectilesReserva){
                    proyectilesreservatemp = cantProyectilesReserva;
                }
                cantProyectiles += proyectilesreservatemp;
                GameObject objetoEncontrado = GameObject.Find("BalasContador");
                TextMeshProUGUI puntaje = objetoEncontrado.GetComponent<TextMeshProUGUI>();
                //int cantTemp = int.Parse(puntaje.text);
                puntaje.text=""+(cantProyectiles);
                puntaje.alignment = TextAlignmentOptions.Right;
                
                cantProyectilesReserva-=proyectilesreservatemp;
                GameObject objetoEncontrado2 = GameObject.Find("BalasContadorEstatico");
                TextMeshProUGUI puntaje2 = objetoEncontrado2.GetComponent<TextMeshProUGUI>();
                //int cantTemp = int.Parse(puntaje2.text);
                puntaje2.text="/"+(cantProyectilesReserva);
                puntaje2.alignment = TextAlignmentOptions.Left;
            }
        }
        
    }

    void Disparar()
    {
        if (camara == null)
        {
            Debug.LogError("La cámara no está asignada. Asigne una cámara en el Inspector.");
            return;
        }
        
        // Obtén la posición y rotación de la cámara
        Transform camaraTransform = camara.transform;

        // Calcula la posición adelante de la cámara
        Vector3 posicionDisparo = camaraTransform.position + camaraTransform.forward;

        // Instancia una nueva bala en la posición calculada y con la rotación de la cámara
        GameObject bala = null;
        if(cantProyectiles > 0){
            bala = Instantiate(balaPrefab, posicionDisparo, camaraTransform.rotation);
            cantProyectiles--;
            
            GameObject objetoEncontrado = GameObject.Find("BalasContador");
			TextMeshProUGUI puntaje = objetoEncontrado.GetComponent<TextMeshProUGUI>();
			int cantTemp = int.Parse(puntaje.text);
            puntaje.text=""+(--cantTemp);
            puntaje.alignment = TextAlignmentOptions.Right;

        }
        if(bala != null){
            // Ajusta la rotación en el eje X de la bala a 90 grados (para que se mueva hacia adelante)
            bala.transform.Rotate(Vector3.right, 90.0f);

            // Obtén o agrega el componente Rigidbody
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bala.AddComponent<Rigidbody>();
            }
            // Aplica una fuerza hacia adelante en el eje Y
            rb.AddForce((bala.GetComponent<Transform>()).up * fuerzaBala, ForceMode.Impulse);
        }
    }
}
