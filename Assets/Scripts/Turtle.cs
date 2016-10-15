using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {

    public GameObject dummy;
    public GameObject key;

    public Vector3 keyPos;
    private int health = 10;
	// Use this for initialization
	void Start () {
        Instantiate(dummy, new Vector3(transform.position.x,
                             transform.position.y - 13f, transform.position.z)
                             , new Quaternion());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name.Contains("Ground"))
        {
            Destroy(GameObject.Find("Dummy(Clone)"));
        }
        if(coll.gameObject.name.Contains("Projectile"))
        {
            health--;
            if(health <= 0)
            {
                Instantiate(key, keyPos, new Quaternion());
                Destroy(gameObject);
            }
        }
    }
}
