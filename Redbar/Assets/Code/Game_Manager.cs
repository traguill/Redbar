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

    public float introTimer = 8.0f;
    public bool introShown = false;
    public bool titleShown = false;
    public bool isTutorialFinished = false;
    public bool gameOver = false;
    private bool fade_initiating = false;

    public List<string> dialogue;

    void Awake () {
        g_GameManager = this;
        dialogue = new List<string>();
        dialogue.Add("Hey sweetie, where are you going?");
        dialogue.Add("Wanna have some fun?");
        dialogue.Add("Do you want to come with me?");
        dialogue.Add("Nice body you have.");
        dialogue.Add("Come here honey, don’t be shy.");
        dialogue.Add("Wanna see something great?");
        dialogue.Add("Isn’t it dangerous for such a beauty to walk alone at night?");
        dialogue.Add("Don’t be scared, I just want to know you.");
        dialogue.Add("Hey girl you look so cute.");
	}

    private void Start()
    {
        ui.GetComponent<UIManager>().FadeIn();
        ui.GetComponent<UIManager>().activateTitle(true);
        ui.GetComponent<UIManager>().changeText(ui.GetComponent<UIManager>().introText);
        
    }
    // Update is called once per frame
    void Update ()
    {
        if(introTimer >= 0 && !introShown)
        {
            if(introTimer < 4 && !titleShown)
            {
                ui.GetComponent<UIManager>().activateTitle(false);
                titleShown = true;
            }
            introTimer -= Time.deltaTime;
        }
        else if(introTimer <= 0 && !introShown)
        {
            ui.GetComponent<UIManager>().FadeOut();
            introShown = true;
            // Spawn tutorial
            Enemymanager.g_EnemyManager.SpawnEvent();
        }

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
