using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public const float LAUNCH_FORCE = 900f;
    private Rigidbody2D thisBody;

    // Use this for initialization
    void Start () {
        thisBody = GetComponent<Rigidbody2D>();
        thisBody.AddForce(transform.right * LAUNCH_FORCE);
        Debug.Log((Vector2) transform.right);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
