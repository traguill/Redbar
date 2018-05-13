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

    public bool canPortal = false;

    public float bckMoveSpeed = 1.0f;
    public float bckMoveSpeedFast = 3.0f;
    public float timeBoost = 3.0f;
    public float currentBoost = 3.0f;
    public float timeToCharge = 4.0f;

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
        }

        else if(Input.GetAxis("LeftJoystickVertical") < -0.1f)
        {
            running = false;
            stop = false;
        }

        else
        {
            stop = true;
            running = false;
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

        if (stop)
            step = 0.0f;

        anim.SetFloat("speed", step);
        transform.position += new Vector3(step, 0.0f, 0.0f);
        
	}

    void MobileAction()
    {
        anim.SetBool("isPhoneOut", true);
        currentState = Actions.Mobile;
        IEnumerator coroutine = EndOnSeconds(2); //TODO ANIMATION LENGTH
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
        IEnumerator coroutine = EndOnSeconds(4);
        StartCoroutine(coroutine);
    }

    IEnumerator EndOnSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        NoAction();
    }
}
