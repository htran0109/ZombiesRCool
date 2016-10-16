using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {

    public GameObject dummy;
    public GameObject key;
    public GameObject fireball1;
    public GameObject fireball2;

    private int fireballTimer;

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
        if(fireballTimer == 180)
        {
            Instantiate(fireball1, new Vector3(transform.position.x,
                             transform.position.y + 2f, transform.position.z), new Quaternion());
        }
        else if(fireballTimer == 360)
        {
            Instantiate(fireball2, new Vector3(transform.position.x,
                             transform.position.y + 2f, transform.position.z), new Quaternion());
        }
        else if(fireballTimer > 360)
        {
            fireballTimer = 0;
        }
        fireballTimer++;
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
