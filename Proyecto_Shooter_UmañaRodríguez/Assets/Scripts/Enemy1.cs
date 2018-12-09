using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {
    public static Enemy1 instance;
    private NavMeshAgent agent;
    public GameObject player;
    public float playerDetected = 5.0f;
    bool follow = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
        if (follow == true)
        {
            //Se asigna la posision del jugador a playerpos
            Vector3 playerpos = player.transform.position;
            //Se asigna la distancia entre el jugador y el enemigo a distance
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < playerDetected)
            {
                agent.SetDestination(playerpos);
            }

        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(15);
        follow = true;
    }
}
