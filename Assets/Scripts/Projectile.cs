using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public const float LAUNCH_FORCE = 400;
    private Rigidbody2D thisBody;

    // Use this for initialization
    void Start () {
        thisBody = GetComponent<Rigidbody2D>();
        thisBody.AddForce(new Vector2(LAUNCH_FORCE, 0));
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
