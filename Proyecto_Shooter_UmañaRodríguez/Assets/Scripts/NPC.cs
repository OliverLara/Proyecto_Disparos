using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject player;
    public float playerDetected = 5.0f;
    float playerInRange = 0.5f;
    public static bool follow;
    bool over;



    // Use this for initialization
    void Start()
    {
        follow = false;
        over = true;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Follow");
    }

    // Update is called once per frame
    void Update()
    {
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

            if (distance < playerInRange) 
            {
                Destroy(gameObject);
                PlayerShoot.score += 20;
            }
        }

        if (follow == false && over == true)
        {
            Wait();
        }
    }

    IEnumerator Follow()
    {
        yield return new WaitForSeconds(22);
        follow = true;
    }
    IEnumerator Flag()
    {
        yield return new WaitForSeconds(22);
    }

    void Wait()
    {
        over = false;
        StartCoroutine("Flag");
    }

}
