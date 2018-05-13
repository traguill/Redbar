using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager g_GameManager;

    [SerializeField] public GameObject message;
    [SerializeField] public GameObject player;

    public bool isTutorialFinished = false;

    void Awake () {
        g_GameManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMessage(string text)
    {
        message.GetComponent<Text>().text = text;
        message.SetActive(true);

    }

    IEnumerator DeactivateMessage()
    {
        yield return new WaitForSeconds(2.0f);
        message.SetActive(false);
    }
}
