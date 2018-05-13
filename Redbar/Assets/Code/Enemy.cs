using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    DRUNK,
    GROUP,
    CAR,
    SILHOUETTE,
    NONE
}

public class Enemy : MonoBehaviour {

    [SerializeField] public bool reacts = false;
    [SerializeField] public EnemyType type;
    [SerializeField] public float speed;
    [SerializeField] public float loseDistance;
    [SerializeField] public float mobileProb;

    private Transform target;
    private bool alreadyInteracted = false;

    private SpriteRenderer[] sprites;
    private bool playerSafed = false;

    BoxCollider boxCol;
    Plane[] planes;
    void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

    }

    // Use this for initialization
    void Start () {
        target = Game_Manager.g_GameManager.player.transform;

        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        else
        {
            sprites[0] = gameObject.GetComponent<SpriteRenderer>();
        }

        if (target.position.x < transform.position.x)
        {
            speed = -speed;
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = true;
            }
        }

        else
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.flipX = false;
            }
        }

    }

    public void Init(bool reaction)
    {
        reacts = reaction;
    }
	// Update is called once per frame
	void Update () {

        if(playerSafed)
        {
            CheckOutOfCam();
        }

		if(reacts && !alreadyInteracted)
        {
            StartCoroutine("Interact");
        }

        float step = Time.deltaTime * speed;

        transform.position += new Vector3(step, 0.0f, 0.0f);
        
        if (Mathf.Abs(transform.position.x - target.position.x) < loseDistance && Game_Manager.g_GameManager.gameOver == false && playerSafed == false)
        {

            if(reacts && Game_Manager.g_GameManager.GetPlayerState() != Actions.Portal) //TODO OR PLAYER CHOOSES CORRECT OPTION.
            {
                if(Game_Manager.g_GameManager.GetPlayerState() == Actions.Mobile)
                {
                    int prob = Random.Range(0, 100);
                    
                    if(prob > mobileProb)
                    {
                        Game_Manager.g_GameManager.ui.GetComponent<UIManager>().changeText(Game_Manager.g_GameManager.ui.GetComponent<UIManager>().loseText);
                        Game_Manager.g_GameManager.gameOver = true;

                        GamePadController.instance.SetTimer(0.5f);
                        StartCoroutine(GamePadController.instance.Vibrate());
                        Debug.Log("Game Over");
                        Destroy(gameObject);
                    }

                    else
                    {
                        playerSafed = true;
                        Enemymanager.g_EnemyManager.NotifyEventEnd();
                    }
                   
                }
            }

            else
            {
                playerSafed = true;
                Enemymanager.g_EnemyManager.NotifyEventEnd();
            }
        }


        if (Game_Manager.g_GameManager.gameOver)
            Destroy(gameObject);
    }

    IEnumerator Interact()
    {
        alreadyInteracted = true;

        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        
        int dialogue = Random.Range(0, Game_Manager.g_GameManager.dialogue.Count - 1);
        Game_Manager.g_GameManager.ShowMessage(Game_Manager.g_GameManager.dialogue[dialogue]);
        yield return null;


        GamePadController.instance.SetTimer(0.5f);
        yield return GamePadController.instance.Vibrate();

    }

    void CheckOutOfCam()
    {

         if (GeometryUtility.TestPlanesAABB(planes, boxCol.bounds) == false)
         {
             Destroy(gameObject);
         }
    }
}
