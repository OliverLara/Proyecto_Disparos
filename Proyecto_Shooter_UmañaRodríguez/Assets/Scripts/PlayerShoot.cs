using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject mira;
    public GameObject currentWeapon;
    public GameObject followWeapon;
    Camera theCamera;

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

    }

    // Update is called once per frame
    void Update()
    {

        if (theCamera.ScreenToViewportPoint(Input.mousePosition).x > minX &&
            theCamera.ScreenToViewportPoint(Input.mousePosition).x < maxX &&
            theCamera.ScreenToViewportPoint(Input.mousePosition).x > minY &&
            theCamera.ScreenToViewportPoint(Input.mousePosition).x < maxY)
        {
            mira.transform.position = Input.mousePosition;
        }

        followWeapon.transform.position = theCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.6f));

        currentWeapon.transform.LookAt(followWeapon.transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(theCamera.ScreenPointToRay(Input.mousePosition), out hit, 20f))
            {
                Debug.Log(hit.collider.name);
            }
        }

    }
}
