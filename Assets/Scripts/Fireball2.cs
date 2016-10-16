using UnityEngine;
using System.Collections;

public class Fireball2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-.04f, -.015f));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
