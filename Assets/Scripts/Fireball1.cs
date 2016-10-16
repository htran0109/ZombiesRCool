using UnityEngine;
using System.Collections;

public class Fireball1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-.06f, 0));
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
