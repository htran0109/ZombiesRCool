using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public const float ENEMY_SPEED = 1f;
    public const float VISION_LENGTH = 10f;
    public const float SLOW_FORCE = 20f;
    public const float KNOCKBACK_FORCE = 800f;

    private bool firstSeen = true;
    private int hitTimer = 0;
    public LayerMask visionMask;
    public AudioSource seenNoiseSource;

    private GameObject character;
    private Rigidbody2D thisBody;
    public float walkForce = 50f;
    private bool moving;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(4,-3,0);
        character = GameObject.Find("Character");
        thisBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        int found = findPlayer();
        moving = false;
	    if(found == -1)
        {
            move(-1);
        }
        else if(found == 1)
        {
            move(1);
        }
        slowWalk();
        hitTimer--;
    }

    /**
     * found left = -1
     * found right = 1
     * not found = 0
     * */
    int findPlayer()
    {
 
        RaycastHit2D vision = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -1f),
                        Vector3.left, VISION_LENGTH, visionMask);
        RaycastHit2D vision2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f),
                        Vector3.left, VISION_LENGTH, visionMask);
        RaycastHit2D vision3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        Vector3.left, VISION_LENGTH, visionMask);
        if ((vision.collider && vision.collider.gameObject.name.Contains("Character")) ||
            (vision2.collider && vision2.collider.gameObject.name.Contains("Character")) ||
            (vision3.collider && vision3.collider.gameObject.name.Contains("Character")))
        {
            if(firstSeen)
            {
                Debug.Log("Playing sound");
                seenNoiseSource.Play();
            }
            firstSeen = false;
            return -1;
        }
        vision = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f),
            Vector3.right, VISION_LENGTH, visionMask);
        vision2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f),
                       Vector3.right, VISION_LENGTH, visionMask);
        vision3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                        Vector3.right, VISION_LENGTH, visionMask);
        if ((vision.collider && vision.collider.gameObject.name.Contains("Character")) ||
            (vision2.collider && vision2.collider.gameObject.name.Contains("Character"))||
            (vision3.collider && vision3.collider.gameObject.name.Contains("Character")))
        {
            if (firstSeen)
            {
                Debug.Log("Playing sound");
                seenNoiseSource.Play();
            }
            firstSeen = false;
            return 1;
        }
        firstSeen = true;
        return 0;
    }

    void move(int direction)
    {
        moving = true;
        if(direction == 1)
        {
            if (thisBody.velocity.x < ENEMY_SPEED)
            {
                thisBody.AddForce(new Vector2(walkForce, 0));
            }
            else
            {
                thisBody.velocity = new Vector2(ENEMY_SPEED, thisBody.velocity.y);
            }
        }
        else if(direction == -1)
        {
            if (thisBody.velocity.x > ENEMY_SPEED)
            {
                thisBody.AddForce(new Vector2(-walkForce, 0));
            }
            else
            {
                thisBody.velocity = new Vector2(-ENEMY_SPEED, thisBody.velocity.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.name.Contains("Character") && hitTimer <=0)
        {
            hitTimer = 20;
            if(thisBody.velocity.x > 0)
            {
                thisBody.AddForce(new Vector2(KNOCKBACK_FORCE, 0));
            }
            else if(thisBody.velocity.x < 0)
            {
                thisBody.AddForce(new Vector2(-KNOCKBACK_FORCE, 0));
            }
        }
    }

    void slowWalk()
    {
        if (!moving)
        {
            if (thisBody.velocity.x > 0)
            {
                thisBody.AddForce(new Vector2(-SLOW_FORCE, 0));
            }
            else if (thisBody.velocity.x < 0)
            {
                thisBody.AddForce(new Vector2(SLOW_FORCE, 0));
            }
        }
    }
}
