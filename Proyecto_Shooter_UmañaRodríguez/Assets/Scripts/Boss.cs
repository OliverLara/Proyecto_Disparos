using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    private NavMeshAgent agent;

    public static int bossHealth = 100;

    public GameObject player;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject bullet;

    public Text bossHealthText;
    
    float playerInRange = 0.5f;
    float waypointInRange = 0.3f;
    float fireRate;
    float nextFire;

    public static bool flag = false;
    bool over = true;
    bool flag2;

	// Use this for initialization
	void Start () {

        bossHealth = 100;
        fireRate = 1f;
        nextFire = Time.time;
        flag2 = true;
        flag = false;
        agent = GetComponent<NavMeshAgent>();
        bossHealthText.GetComponent<Text>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerShoot.playerHealth > 0)
        {
            //calcular la posicion del jugador 
            Vector3 playerpos = player.transform.position;

            //Distancia ente el boss y el player, el boss y el primer waypoint y el boss y el segundo waypoint
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float distanceToWaypoint1 = Vector3.Distance(transform.position, waypoint1.transform.position);
            float distanceToWaypoint2 = Vector3.Distance(transform.position, waypoint2.transform.position);

            //Si la animacion ya termino el boss va hacia el jugador 
            if (AnimationControl.playableDirector.state != UnityEngine.Playables.PlayState.Playing && flag == true)
            {
                PlayerShoot.fight = true;
                agent.SetDestination(playerpos);
                bossHealthText.GetComponent<Text>().enabled = true;
            }

            //Si el jugador está en rango, ataca al jugador
            if (distance < playerInRange && over == true)
            {
                Attack();
            }

            //Si la vida del boss es menor a 50, empieza la segunda fase
            //El boss va al primer waypoint y luego al segundo y asi sucesivamente mientras dispara al jugador cada segundo
            if (bossHealth <= 50 && flag2 == true)
            {
                agent.SetDestination(waypoint1.transform.position);
                agent.speed = 10f;
                if (distanceToWaypoint1 < waypointInRange)
                {
                    PlayerShoot.fight = true;
                    flag2 = false;
                }

                if (Time.time > nextFire)
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }

            }
            if (bossHealth <= 50 && flag2 == false)
            {
                agent.SetDestination(waypoint2.transform.position);
                agent.speed = 10f;
                if (distanceToWaypoint2 < waypointInRange)
                {
                    PlayerShoot.fight = true;
                    flag2 = true;
                }

                if (Time.time > nextFire)
                {
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }
            //Si la vida del boss es = 0 el boss se destruye
            if (bossHealth <= -1)
            {
                Destroy(this);
            }


            bossHealthText.text = "Boss Health: " + bossHealth;
        }
    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(1.5f);
        over = true;
    }


    void Attack()
    {
        over = false;
        PlayerShoot.playerHealth -= 20;
        StartCoroutine("Damage");
  
        if (PlayerShoot.playerHealth <= 0)
        {
            StopCoroutine("Damage");
        }
    }
}
