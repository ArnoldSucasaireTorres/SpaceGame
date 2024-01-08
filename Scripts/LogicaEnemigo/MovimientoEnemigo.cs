using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform goal;
    public float velocidad = 0.1f;
    public float radioVision = 0.01f;
    public float radioMaximo = 1f;
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x,y;
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = goal.position - this.transform.position;
        Debug.DrawRay(this.transform.position,direction,Color.red);
        transform.LookAt(goal);
        
        if(direction.magnitude < radioVision && direction.magnitude > radioMaximo){
            anim.SetFloat("VelX", this.transform.position.x);
            anim.SetBool("Atacar",false);
            anim.SetFloat("VelY", this.transform.position.y);
            this.transform.Translate(direction.normalized * velocidad * Time.deltaTime, Space.World);
            
        }
        else{
            if(direction.magnitude > radioMaximo){
                anim.SetBool("Atacar",false);
                
            }
            else{
                anim.SetBool("Atacar",true);
            }
            anim.SetFloat("VelX",0.0f);
            
            anim.SetFloat("VelY",0.0f);
        }
        //x = Input.GetAxis("Horizontal");
        //y = Input.GetAxis("Vertical");
        //transform.Rotate(0,x*Time.deltaTime*velocidadRotacion,0);
        //transform.Translate(0,0,y*Time.deltaTime*velocidadMovimiento);
    } 
}
