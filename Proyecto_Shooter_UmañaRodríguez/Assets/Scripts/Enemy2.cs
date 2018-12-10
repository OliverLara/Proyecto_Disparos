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


	// Use this for initialization
	void Start () {
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
}
