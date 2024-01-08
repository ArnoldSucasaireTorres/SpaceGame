using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 using TMPro;
public class EscenaControlador : MonoBehaviour
{
    public void Awake(){
        int nroEscena = PlayerPrefs.GetInt("nroEscena");
        GameObject encabezadoEncontrado = GameObject.Find("Encabezado");
        TextMeshProUGUI valorEncabezado = encabezadoEncontrado.GetComponent<TextMeshProUGUI>();
        if(nroEscena == 4){
            valorEncabezado.text = "Feliciadades ganaste";
        }else{
            valorEncabezado.text = "Nivel "+nroEscena;
        }
        valorEncabezado.alignment = TextAlignmentOptions.Center;
        
    }
    public string nombreEscena;
     
    // Start is called before the first frame update
    public void LoadScene(string nombreEscena){
        int nroEscena = 1;
        PlayerPrefs.SetInt("nroEscena", nroEscena);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Preambulo");
    }
    public void Salir(){
        Application.Quit();
    }
    public void reiniciar(){
        int nroEscena = PlayerPrefs.GetInt("nroEscena");
        print("reiniciar: "+nroEscena);
        SceneManager.LoadScene("Nivel_"+nroEscena);

    }
    public void continuar(){
        int nroEscena = PlayerPrefs.GetInt("nroEscena");
        print("reiniciar: "+nroEscena);
        if(nroEscena > 3){
            SceneManager.LoadScene("Menu");
        }else{
            SceneManager.LoadScene("Nivel_"+nroEscena);
        }
        

    }
    public void MenuPrincipal(){
        SceneManager.LoadScene("Menu");

    }
}
