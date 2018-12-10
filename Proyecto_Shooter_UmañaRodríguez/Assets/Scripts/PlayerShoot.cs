using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour
{
    public static int playerHealth = 100;
    int score = 0;
    public GameObject mira;
    public GameObject currentWeapon;
    public GameObject followWeapon;
    Camera theCamera;

    bool fight = false;
    bool over = true;

    public Text healthText;
    public Text scoreText;
    public Text gameOver;

    [Header("Max and Min values for mouse position")]
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    RaycastHit hit;


    void Start()
    {
        playerHealth = 100;
        score = 0;
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        StartCoroutine("Cambio");
        AnimationControl.instance.ChangePlayebleTimeline(0);
        gameOver.GetComponent<Text>().enabled = false;
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
                        score += 10;
                    }
                }
            }
        }

        if (playerHealth <= 0 && over == true)
        {
            fight = false;
            Enemy1.follow = false;
            Enemy2.follow = false;
            GameOver();
        }
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + score;
    }

    //Corutina para hacer el cambio de animacion entre la primera y la segunda y activar el control del mouse.
    IEnumerator Cambio()
    {
        yield return new WaitForSeconds(10);
        AnimationControl.instance.ChangePlayebleTimeline(1);
        yield return new WaitForSeconds(4);
        fight = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Shooter");
        StopCoroutine("Wait");
    }
    void GameOver()
    {
        over = false;
        gameOver.GetComponent<Text>().enabled = true;
        StartCoroutine("Wait");

    }

}
