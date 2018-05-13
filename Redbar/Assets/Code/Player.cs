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

    public float bckMoveSpeed = 1.0f;
    public float bckMoveSpeedFast = 3.0f;

    public delegate void DelegateAction();
    DelegateAction action;


    public Actions currentState = Actions.None;

	// Use this for initialization
	void Start () {
        action = NoAction;
	}
	
	// Update is called once per frame
	void Update () {
        #if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Space))
                    running = true;
                if (Input.GetKeyUp(KeyCode.Space))
                    running = false;
        #endif
        
        if(Input.GetButton("AButton"))
        {
            action = PortalAction;
        }

        if(Input.GetButton("BButton"))
        {
            action = MobileAction;
        }

        if (Input.GetAxis("LeftJoystickVertical") < -0.1f && Input.GetAxis("LT") > 0.1f && Input.GetAxis("LT") < 1)
        {
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

        // Move Objects to the left
        float step = Time.deltaTime * (!running ? bckMoveSpeed : bckMoveSpeedFast);

        if (stop)
            step = 0.0f;

        transform.position += new Vector3(step, 0.0f, 0.0f);
	}

    void MobileAction()
    {
        currentState = Actions.Mobile;
        IEnumerator coroutine = EndOnSeconds(2); //TODO ANIMATION LENGTH
        StartCoroutine(coroutine);
    }

    void NoAction()
    {
        currentState = Actions.None;
    }

    void PortalAction()
    {
        currentState = Actions.Portal;
        IEnumerator coroutine = EndOnSeconds(4);
        StartCoroutine(coroutine);
    }

    IEnumerator EndOnSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        action = NoAction;
    }
}
