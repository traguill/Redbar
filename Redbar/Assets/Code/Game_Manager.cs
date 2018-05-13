using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager g_GameManager;

    [SerializeField] public GameObject message;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject ui;

    public bool isTutorialFinished = false;
    public bool gameOver = false;
    private bool fade_initiating = false;

    void Awake () {
        g_GameManager = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gameOver == true && fade_initiating == false)
        {
            fade_initiating = true;
            ui.GetComponent<UIManager>().FadeIn();
        }
	}

    public Actions GetPlayerState()
    {
        return player.GetComponent<Player>().currentState;
    }

    public void ShowMessage(string text)
    {
        message.GetComponent<Text>().text = text;
        message.SetActive(true);
        StartCoroutine("DeactivateMessage");
    }

    IEnumerator DeactivateMessage()
    {
        yield return new WaitForSeconds(2.0f);
        message.SetActive(false);
    }

}
