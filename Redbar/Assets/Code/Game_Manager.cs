using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager g_GameManager;

    public bool isTutorialFinished = false;

    void Awake () {
        g_GameManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
