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
    }

    IEnumerator Interact()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        Game_Manager.g_GameManager.ShowMessage("hey blondie");
        alreadyInteracted = true;
    }
}
