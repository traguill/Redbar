using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    bool isHome = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.GetComponent<Player>() != null)
        {
            player.gameObject.GetComponent<Player>().canPortal = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.GetComponent<Player>() != null)
        {
            player.gameObject.GetComponent<Player>().canPortal = false;
        }
    }
}
