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
            if (isHome)
            {
                Game_Manager.g_GameManager.ui.GetComponent<UIManager>().changeText(Game_Manager.g_GameManager.ui.GetComponent<UIManager>().winText);
                Game_Manager.g_GameManager.gameOver = true;

            }
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
