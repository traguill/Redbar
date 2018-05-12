using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour 
{
    public float maxDist = 10.0f;
    public float speed = 1.0f;


    bool camMoving = false;
    bool movingForward = false;

    Vector3 startPos, endPos;
	// Use this for initialization
	void Start () {
        startPos = transform.localPosition;
        endPos = startPos - new Vector3(maxDist, 0.0f, 0.0f);	            	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Slow(true);
        if (Input.GetKeyUp(KeyCode.Space))
            Slow(false);
	    
        if(camMoving)
        {
            float step = Time.deltaTime * speed;
            if (movingForward)
            {
                transform.localPosition -= new Vector3(step, 0.0f, 0.0f);
                if (transform.localPosition.x < endPos.x)
                {
                    transform.localPosition = endPos;
                    camMoving = false;
                }
            }
            else
            { 
                transform.localPosition += new Vector3(step, 0.0f, 0.0f); 
                if(transform.localPosition.x > startPos.x)
                {
                    transform.localPosition = startPos;
                    camMoving = false;
                }
            }
        }
           
        
	
	}

    public void Slow(bool enabled)
    {
        movingForward = enabled;
        camMoving = true;
    }
}
