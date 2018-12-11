using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public Text itemText;

	// Use this for initialization
	void Start () {
        itemText.GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerShoot.playerHealth < 50 && PlayerShoot.playerHealth > 0)
        {
            itemText.GetComponent<Text>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerShoot.playerHealth += 30;
            }
        }

        if (PlayerShoot.playerHealth >= 50)
        {
            itemText.GetComponent<Text>().enabled = false;
        }
	}

}
