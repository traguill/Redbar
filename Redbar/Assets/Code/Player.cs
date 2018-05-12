using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    bool stop = false;
    bool running = false;

    public float bckMoveSpeed = 1.0f;
    public float bckMoveSpeedFast = 3.0f;

	// Use this for initialization
	void Start () {

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
            GamePadController.instance.Vibrate(1);
            //Enter portal
        }

        if(Input.GetButton("BButton"))
        {
            //Mobile phone
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
}
