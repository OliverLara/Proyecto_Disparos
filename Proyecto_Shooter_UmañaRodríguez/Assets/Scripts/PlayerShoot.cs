using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int playerHealth = 100;
    

    public GameObject mira;
    public GameObject currentWeapon;
    public GameObject followWeapon;
    Camera theCamera;

    bool fight = false;

    [Header("Max and Min values for mouse position")]
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    RaycastHit hit;


    void Start()
    {

        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        AnimationControl.instance.ChangePlayebleTimeline(0);
        StartCoroutine("Cambio");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Comportamiento de la cámara
        if (fight == true) {

            if (theCamera.ScreenToViewportPoint(Input.mousePosition).x > minX &&
                theCamera.ScreenToViewportPoint(Input.mousePosition).x < maxX &&
                theCamera.ScreenToViewportPoint(Input.mousePosition).x > minY &&
                theCamera.ScreenToViewportPoint(Input.mousePosition).x < maxY)
            {
                mira.transform.position = Input.mousePosition;
            }

            followWeapon.transform.position = theCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.6f));

            currentWeapon.transform.LookAt(followWeapon.transform.position);

            //Si se presiona el boton izquierdo del mouse
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(theCamera.ScreenPointToRay(Input.mousePosition), out hit, 20f))
                {
                    //Destruir al enemigo si el reycast detecta el tag "Enemy"
                    if (hit.collider.tag == "Enemy")
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
        
    }

    //Corutina para hacer el cambio de animacion entre la primera y la segunda y activar el control del mouse.
    IEnumerator Cambio()
    {
        yield return new WaitForSeconds(10);
        AnimationControl.instance.ChangePlayebleTimeline(1);
        yield return new WaitForSeconds(4);
        fight = true;
    }

    public void enemyAttack()
    {
        if (Enemy1.instance.player.transform.position == Enemy1.instance.transform.position)
        {
            playerHealth = 0;
            Debug.Log(playerHealth);
        }
    }
}
