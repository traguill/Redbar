using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Actions
{
    Portal,
    Mobile,
    None
}

public class Player : MonoBehaviour {


    bool stop = false;
    bool running = false;
    bool breathing = false;

    public bool canPortal = false;

    public float bckMoveSpeed = 1.0f;
    public float bckMoveSpeedFast = 3.0f;
    public float timeBoost = 3.0f;
    public float currentBoost = 3.0f;
    public float timeToCharge = 4.0f;

    public AudioSource source;
    public AudioClip mobile;
    public AudioClip breath;
    public AudioClip gate;

    public delegate void DelegateAction();
    DelegateAction action;

    Animator anim;

    public Actions currentState = Actions.None;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
        NoAction();
	}
	
	// Update is called once per frame
	void Update () {
        #if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Space))
                    running = true;
                if (Input.GetKeyUp(KeyCode.Space))
                    running = false;
        #endif
        
        if(Input.GetButton("AButton") && canPortal)
        {
            PortalAction();
        }

        if(Input.GetButton("BButton"))
        {
            MobileAction();
        }

        if (Input.GetAxis("LeftJoystickVertical") < -0.1f && Input.GetAxis("LT") > 0.1f && Input.GetAxis("LT") < 1 && currentBoost >= 0.0f)
        {
            currentBoost -= Time.deltaTime;
            running = true;
            stop = false;
            if (!breathing)
            {
                source.PlayOneShot(breath);
                breathing = true;
            }
        }

        else if(Input.GetAxis("LeftJoystickVertical") < -0.1f)
        {
            running = false;
            stop = false;
            breathing = false;
        }

        else
        {
            stop = true;
            running = false;
            breathing = false;
        }

        if(currentBoost <= 0 && timeToCharge > 0)
        {
            timeToCharge -= Time.deltaTime;
        }

        else if(currentBoost <= 0 && timeToCharge <= 0)
        {
            timeToCharge = 4.0f;
            currentBoost = timeBoost;
        }
        // Move Objects to the left
        float step = Time.deltaTime * (!running ? bckMoveSpeed : bckMoveSpeedFast);

        if (stop || currentState == Actions.Portal)
        {
            step = 0.0f;
        }

        anim.SetFloat("speed", step);
        transform.position += new Vector3(step, 0.0f, 0.0f);

        if (Game_Manager.g_GameManager.gameOver)
            Destroy(gameObject);
	}

    void MobileAction()
    {
        anim.SetBool("isPhoneOut", true);
        currentState = Actions.Mobile;
        source.PlayOneShot(mobile);
        IEnumerator coroutine = EndOnSeconds(2);
        StartCoroutine(coroutine);
    }

    void NoAction()
    {
        anim.SetBool("isPhoneOut", false);
        anim.SetBool("isInsidePortal", false);
        currentState = Actions.None;
    }

    void PortalAction()
    {
        anim.SetBool("isInsidePortal", true);
        currentState = Actions.Portal;
        source.PlayOneShot(gate);
        IEnumerator coroutine = EndOnSeconds(4);
        StartCoroutine(coroutine);
    }

    IEnumerator EndOnSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        NoAction();
    }
}
