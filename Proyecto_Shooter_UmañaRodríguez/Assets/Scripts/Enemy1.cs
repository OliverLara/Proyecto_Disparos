using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject player;
    public float playerDetected = 5.0f;
    float playerInRange = 0.5f;
    public static bool follow = false;
    bool over = true;
    bool over2 = true;
    



    // Use this for initialization
    void Start () {

        follow = false;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Follow");
    }
	
	// Update is called once per frame
	void Update () {
        if (follow == true)
        {
            //Se asigna la posision del jugador a playerpos
            Vector3 playerpos = player.transform.position;

            //Se asigna la distancia entre el jugador y el enemigo a distance
            float distance = Vector3.Distance(transform.position, player.transform.position);

            //Se determina si la distancia entre el player y el enemigo es menor que 5
            if (distance < playerDetected)
            {
                agent.SetDestination(playerpos);
            }
            //Se determina si la distancia entre el player y el enemigo es menor que 0.5
            if (distance < playerInRange && over == true)
            {
                Health();
            }
        }
        if (follow == false && over == true)
        {
            Wait();
        }
    }

    IEnumerator Follow()
    {
        yield return new WaitForSeconds(14);
        follow = true;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.5f);
        over = true;
    }

    //Metodo para bajar la vida del jugador cada 2.5 segundos si la posicion del enemigo es la misma que la del player
    public void Health()
    {
        over = false;
        PlayerShoot.playerHealth -= 10;
        StartCoroutine("Attack");
        Debug.Log(PlayerShoot.playerHealth);
        if (PlayerShoot.playerHealth <= 0)
        {
            StopCoroutine("Attack");
            Debug.Log("Player is dead");
        }
    }
    IEnumerator Flag()
    {
        yield return new WaitForSeconds(14);
    }

    void Wait()
    {
        over2 = false;
        StartCoroutine("Flag");
    }

}
