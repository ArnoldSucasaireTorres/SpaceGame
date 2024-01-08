using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersonajePrincipal_mov : MonoBehaviour
{
    public int nroEscena;
    private AudioSource[] misAudioSources;

    //public float velocidadRotacion = 2.0f;
    public Image barraDeVida;
    public float vidaActual;
    public float vidaMaxima;
    public float velCorrer = 0.0f;
    public float velMovTemp = 0.0f;
    public float velocidadMov = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x,y;
    public GameObject rifleD;
    public GameObject rifleI;
    public Rigidbody rb;
    public float fuerzaSalto = 0f;
    public bool puedoSaltar;
    // Start is called before the first frame update
    void Start()
    {
        nroEscena = PlayerPrefs.GetInt("nroEscena", 0);
        misAudioSources = GetComponents<AudioSource>();
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        velMovTemp = velocidadMov;
    }
    void FixedUpdate(){
        transform.Translate(x*Time.deltaTime*velocidadMov,0,y*Time.deltaTime * velocidadMov);

    }
    // Update is called once per frame
    void Update()
    {
        barraDeVida.fillAmount = vidaActual /vidaMaxima;
        if(vidaActual>0){
            if(Input.GetKey(KeyCode.LeftShift) && puedoSaltar){
                velocidadMov = velCorrer;
                if(y>0){
                    anim.SetBool("Correr",true);
                }
            }
            else{
                velocidadMov = velMovTemp;
                anim.SetBool("Correr",false);
            }

            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            if(Input.GetKey(KeyCode.W)){
                if (!misAudioSources[1].isPlaying)
                {
                    misAudioSources[1].loop = true;
                    misAudioSources[1].Play();
                }             

                activarDI(true,false);
            }else{
                misAudioSources[1].loop = false;
                misAudioSources[1].Stop();
            }
            if (Input.GetKey(KeyCode.A))
            {
                activarDI(true,false);
            }
            if (Input.GetKey(KeyCode.D))
            {
                activarDI(true,false);
            }
            anim.SetFloat("VelX",x);
            anim.SetFloat("VelY",y);
            if(puedoSaltar){
                if(Input.GetKeyDown(KeyCode.Space)){
                    activarDI(true,false);
                    anim.SetBool("Salte",true);
                    rb.AddForce(new Vector3(0,fuerzaSalto,0),ForceMode.Impulse);
                }
                anim.SetBool("TocoSuelo",true);
            }
            else{
                EstoyCayendo();
            }
        }
        else{

            anim.SetFloat("VelX",0.0f);
            anim.SetFloat("VelY", 0.0f);
            anim.SetBool("TocoSuelo",true);
            anim.SetBool("Correr",false);
            anim.SetBool("Morir",true);
            print("reiniciar: "+nroEscena);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Preambulo");
            //SceneManager.LoadScene("Reintento");
            //SceneManager.LoadScene("Nivel_2");

        }

    }

    public void EstoyCayendo(){
        anim.SetBool("TocoSuelo",false);
        anim.SetBool("Salte",false);
    }

    public void activarDI(bool derecha, bool izquierda){
        rifleD.SetActive(derecha);
        rifleI.SetActive(izquierda); 
    }
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene un determinado tag
        if (collision.gameObject.CompareTag("Satelite"))
        {
            nroEscena++;
            PlayerPrefs.SetInt("nroEscena", nroEscena);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Preambulo");
            //SceneManager.LoadScene("Nivel_"+nroEscena);

        }
    }
}
