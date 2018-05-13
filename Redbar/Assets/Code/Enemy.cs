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

    private Transform target;
    private bool alreadyInteracted = false;

    private SpriteRenderer[] sprites;

    // Use this for initialization
    void Start () {
        target = Game_Manager.g_GameManager.player.transform;
        sprites = GetComponentsInChildren<SpriteRenderer>();

    }

    public void Init(bool reaction)
    {
        reacts = reaction;
    }
	// Update is called once per frame
	void Update () {

		if(reacts && !alreadyInteracted)
        {
            StartCoroutine("Interact");
        }

        float step = Time.deltaTime * speed;

        if(target.position.x < transform.position.x)
        {
            step = -step;
            foreach(SpriteRenderer sprite in sprites)
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

        transform.position += new Vector3(step, 0.0f, 0.0f);
        
        if (Mathf.Abs(transform.position.x - target.position.x) < loseDistance && Game_Manager.g_GameManager.gameOver == false)
        {

            if(reacts) //TODO OR PLAYER CHOOSES CORRECT OPTION.
            {
                Game_Manager.g_GameManager.GetComponent<UIManager>().changeText(Game_Manager.g_GameManager.GetComponent<UIManager>().loseText);
                Game_Manager.g_GameManager.gameOver = true;

                GamePadController.instance.SetTimer(0.5f);
                StartCoroutine(GamePadController.instance.Vibrate());
                Debug.Log("Game Over");
                Destroy(gameObject);
            }
        }
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
}
