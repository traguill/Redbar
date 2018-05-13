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

        if (Vector3.Distance(transform.position, target.position) < (loseDistance + 7))
        {
            GamePadController.instance.SetTimer(0.5f);
            StartCoroutine(GamePadController.instance.Vibrate());
            Debug.Log("Game Over");
        }

        if (Vector3.Distance(transform.position, target.position) < (loseDistance + 5))
        {
            GamePadController.instance.SetTimer(1.0f);
            StartCoroutine(GamePadController.instance.Vibrate());
            Debug.Log("Game Over");
        }

        if (Vector3.Distance(transform.position, target.position) < loseDistance)
        {
            GamePadController.instance.SetTimer(2.0f);
            StartCoroutine(GamePadController.instance.Vibrate());

            if(reacts) //TODO OR PLAYER CHOOSES CORRECT OPTION.
            {
                // TODO SHOW GAME OVER SCREEN
                Debug.Log("Game Over");
            }
        }
    }

    IEnumerator Interact()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        // TODO DISPLAY RANDOM SENTENCE

        GamePadController.instance.SetTimer(0.5f);
        StartCoroutine(GamePadController.instance.Vibrate());
        Game_Manager.g_GameManager.ShowMessage("hey girl");
        alreadyInteracted = true;
    }
}
