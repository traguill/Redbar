using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkmanTilt : MonoBehaviour 
{
    public float speed = 1.0f;
    public float intensity = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0.0f, intensity * Mathf.Sin(Time.timeSinceLevelLoad * speed));
	}
}
