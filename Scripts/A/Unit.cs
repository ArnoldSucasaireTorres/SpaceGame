using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public Image barraDeVida;
    public float vidaActual;
    public float vidaMaxima;
    public PersonajePrincipal_mov objetivo;
    public GameObject[] waypoints;
    public Transform objetivoWaypoint;
    public Transform goal;
    public float ataque = 5.0f;
    public float velocidad = 0.1f;
    public float radioVision = 0.01f;
    public float radioMaximo = 1f;
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;
    Vector3[] path;
    int targetIndex;
    int currentWP = 0;

    private bool isPaused = false;
    private Coroutine unitCoroutine;

    void Start()
    {
        anim = GetComponent<Animator>();
        unitCoroutine = StartCoroutine(UpdateUnitCoroutine());
    }

    IEnumerator UpdateUnitCoroutine()
    {
        while (vidaActual > 0)
        {
            barraDeVida.fillAmount = vidaActual / vidaMaxima;

            if (!isPaused)
            {
                UpdateUnitLogic();
            }

            yield return null;
        }

        anim.SetFloat("VelX", 0.0f);
        anim.SetFloat("VelY", 0.0f);
        anim.SetBool("Atacar", false);
        anim.SetBool("Morir", true);
        Destroy(gameObject);
    }

    void UpdateUnitLogic()
    {
        Vector3 direction = verificarDistancia();
        Debug.DrawRay(this.transform.position, direction, Color.red);
        transform.LookAt(goal);
		if(direction.magnitude < 20){
			if (direction.magnitude < radioVision && direction.magnitude > radioMaximo)
        {
            anim.SetFloat("VelX", this.transform.position.x);
            anim.SetBool("Atacar", false);
            anim.SetFloat("VelY", this.transform.position.y);
            if (direction.magnitude > 2.60)
            {
                PathRequestManager.RequestPath(transform.position, goal.position, OnPathFound);
            }
            else
            {
                this.transform.Translate(direction.normalized * velocidad * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (direction.magnitude > radioMaximo)
            {
                anim.SetBool("Atacar", false);

                if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 3)
                    currentWP++;

                if (currentWP >= waypoints.Length)
                    currentWP = 0;

                objetivoWaypoint = waypoints[currentWP].transform;
                Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, velocidadRotacion * Time.deltaTime);

                anim.SetFloat("VelX", this.transform.position.x);
                anim.SetFloat("VelY", this.transform.position.y);
                PathRequestManager.RequestPath(transform.position, objetivoWaypoint.position, OnPathFound);
            }
            else
            {
                anim.SetBool("Atacar", true);
            }
        }
		}
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
{
    if (this == null)  // Agrega esta comprobación
    {
        // La unidad ha sido destruida, no realices ninguna operación adicional
        return;
    }

    if (pathSuccessful)
    {
        path = newPath;
        targetIndex = 0;
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
    }
}

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = new Vector3();
        if (path.Length != 0)
        {
            currentWaypoint = path[0];
        }
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            if (verificarDistancia().magnitude < 2.0f)
            {
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, velocidad * Time.deltaTime);
            yield return null;
        }
    }

    public Vector3 verificarDistancia()
    {
        Vector3 direction = goal.position - this.transform.position;
        return direction;
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Viajero"))
        {
            objetivo.vidaActual -= ataque;
        }
    }

    private void OnApplicationQuit()
    {
        StopCoroutine(unitCoroutine);
    }

    // Pausa y reanudación de la unidad
    public void PauseUnit()
    {
        isPaused = true;
    }

    public void ResumeUnit()
    {
        isPaused = false;
    }
}
