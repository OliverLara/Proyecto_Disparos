using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOrder : MonoBehaviour {

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;

    bool over ;
    // Use this for initialization
    void Start () {
        over = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null && enemy5 == null && enemy6 == null && enemy7 == null && over == true)
        {
            change();
        }

	}

    void change()
    {
        over = false;
        PlayerShoot.fight = false;
        AnimationControl.instance.ChangePlayebleTimeline(3);
        Boss.flag = true;
    }
}
