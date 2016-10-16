using UnityEngine;
using System.Collections;

public class TurtleEye : MonoBehaviour {

    private int eyeOscillation;
    public float eyeSpeed = .0001f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (eyeOscillation < 20)
        {
            transform.Translate(new Vector3(0, eyeSpeed));
        }
        else if(eyeOscillation < 40)
        {
            transform.Translate(new Vector3(0, -eyeSpeed));
        }
        else
        {
            eyeOscillation = 0;
        }
        eyeOscillation++;
	}
}
