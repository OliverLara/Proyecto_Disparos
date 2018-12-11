using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour
{
    public static int playerHealth = 100;
    public static int score = 0;
    public GameObject mira;
    public GameObject currentWeapon;
    public GameObject followWeapon;
    GameObject npc1;
    Camera theCamera;

    public static bool fight = false;
    bool over = true;
    bool over2 = true;

    public Text healthText;
    public Text scoreText;
    public Text gameOver;
    public Text victoryText;
    public Text playAgainText;
    public Button playAgain;

    [Header("Max and Min values for mouse position")]
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    RaycastHit hit;


    void Start()
    {
        fight = false;
        playerHealth = 100;
        score = 0;
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        StartCoroutine("Cambio");
        AnimationControl.instance.ChangePlayebleTimeline(0);
        gameOver.GetComponent<Text>().enabled = false;
        victoryText.GetComponent<Text>().enabled = false;
        playAgain.enabled = false;
        playAgain.GetComponent<Image>().enabled = false;
        playAgainText.GetComponent<Text>().enabled = false;
        npc1 = GameObject.FindGameObjectWithTag("NPC");
        over = true;
        over2 = true;
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
                    if (hit.collider.tag == "NPC")
                    {
                        Destroy(hit.collider.gameObject);
                        score -= 20;
                    }
                    if (hit.collider.tag == "Boss")
                    {
                        Boss.bossHealth -= 2;
                        if (Boss.bossHealth <= 0)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    if (hit.collider.tag == "Car")
                    {
                        Debug.Log("Wow you hit a car");
                    }

                }
            }
        }
        //Metodo para mostrar el text GAME OVER
        if (playerHealth <= 0 && over == true)
        {
            fight = false;
            Enemy1.follow = false;
            Enemy2.follow = false;
            GameOver();
        }
        //Cambio de Stage 1 a Stage 2
        if (npc1 == null && over2 == true)
        {
            Stage2();
        }
        if (Boss.bossHealth <= 0)
        {
            victoryText.GetComponent<Text>().enabled = true;
            playAgain.enabled = true;
            playAgain.GetComponent<Image>().enabled = true;
            playAgainText.GetComponent<Text>().enabled = true;
            fight = false;
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

    IEnumerator Cambio2()
    {
        fight = false; //Fight = false para que el jugador no se pueda mover en el transcurso de la animacion
        yield return new WaitForSeconds(1);
        AnimationControl.instance.ChangePlayebleTimeline(2); //Se carga la animacion 2
        yield return new WaitForSeconds(10);
        fight = true; // El Jugaror recupero el control del juego
        StopCoroutine("Cambio2");

    }
    void Stage2()
    {
        over2 = false; // Para que no se repita cada frame en el update
        StartCoroutine("Cambio2");
    }
    IEnumerator GAMEOVER()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Shooter"); // Se carga de nuevo la escena 
        StopCoroutine("Wait");
    }
    // Se activa el texto GAME OVER y se empieza la corutina para cargar de nuevo la escena 
    void GameOver()
    {
        over = false;
        Boss.flag = false;
        gameOver.GetComponent<Text>().enabled = true;
        StartCoroutine("GAMEOVER");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Shooter");
    }

}
