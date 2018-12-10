using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float bulletSpeed = 15f;
    Rigidbody rb;
    GameObject target;
    Vector3 moveDirection;
    float playerHit = 0.5f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Tail");
        moveDirection = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        //Destroy(gameObject, 10f);
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < playerHit)
        {
            PlayerShoot.playerHealth -= 20;
            Destroy(gameObject);
        }
	}
}
