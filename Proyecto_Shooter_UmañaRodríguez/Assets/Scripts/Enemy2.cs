using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    public GameObject bullet;
    public GameObject player;
    public float playerDetected = 30f;
    float fireRate;
    float nextFire;
    public static bool follow = false;
    bool over = true;
    


    // Use this for initialization
    void Start () {

        follow = false;
        fireRate = 5f;
        nextFire = Time.time;
        StartCoroutine("Follow");

	}

    // Update is called once per frame
    void Update()
    {
        if (follow == true)
        {
            CheckTimeToFire();
        }

        if (follow == false && over == true)
        {
            Wait();
        }
    }

    void CheckTimeToFire()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < playerDetected)
        {

            if (Time.time > nextFire)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
    }

    IEnumerator Follow()
    {
        yield return new WaitForSeconds(18);
        follow = true;
    }

    IEnumerator Flag()
    {
        yield return new WaitForSeconds(18);
    }

    void Wait()
    {
        over = false;
        StartCoroutine("Flag");
    }
}
