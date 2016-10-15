using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public const float MAX_WALK_SPEED = 3f;
    public const float SLOW_FORCE = 20f;
    public const float KNOCKBACK_FORCE = 2000f;

    private int invinceTimer = 0;
    private int health = 5;
    public float walkForce= 50f;
    public float jumpForce = 1000f;
    public float ladderSpeed = 3f;
    private bool holstered = true;
    private bool grounded = false;
    private bool moving = false;
    private bool ladder = false;
    private bool ladderDown = false;
    Rigidbody2D thisBody;


    public const float MAX_PROJECTILE_COOLDOWN = 2f;
    public Transform projectile;
    private float projectileCooldown = 0f;
    RaycastHit2D ground;
	// Use this for initialization
	void Start () {
        transform.position = new Vector2(-3, -3);
        holstered = true;
        thisBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        readKeys();

        if (projectileCooldown > 0)
        {
            projectileCooldown -= Time.deltaTime;
        }

        slowWalk();
        checkPlatforms();
        invinceTimer--;
  	}

    void readKeys()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            moving = true;
            if (thisBody.velocity.x > -MAX_WALK_SPEED)
            {
                thisBody.AddForce(new Vector2(-walkForce, 0));
            }
            else
            {
                thisBody.velocity = new Vector2(-MAX_WALK_SPEED, thisBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moving = true;
            if (thisBody.velocity.x < MAX_WALK_SPEED)
            {
                thisBody.AddForce(new Vector2(walkForce, thisBody.velocity.y));
            }
            else
            {
                thisBody.velocity = new Vector2(MAX_WALK_SPEED, thisBody.velocity.y);
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!ladder)
            {
                ground = Physics2D.Raycast(transform.position, Vector2.down, 1f);
                if (ground.collider && ground.collider.gameObject.name.Contains("Ground"))
                {

                    thisBody.AddForce(new Vector2(0, jumpForce));

                }
            }
            
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            if(ladder)
            {
                thisBody.velocity = new Vector2(thisBody.velocity.x, ladderSpeed);
            }
        }
        ladderDown = false;
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if (ladder)
            {
                ladderDown = true;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"),
                               LayerMask.NameToLayer("Ground"), true);
                thisBody.velocity = new Vector2(thisBody.velocity.x, -ladderSpeed);
            }
        }
        moving = false;
        
        if(Input.GetKey(KeyCode.Space) && projectileCooldown <= 0f /*&& !holstered*/)
        {
            projectileCooldown = MAX_PROJECTILE_COOLDOWN;
            Instantiate(projectile, gameObject.transform.position + new Vector3(1, 0), Quaternion.identity);
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit: " + coll.gameObject.name);
        if(coll.gameObject.name.Contains("Enemy") && invinceTimer <= 0)
        {
            invinceTimer = 20;
            health--;
            if(thisBody.velocity.x > 0)
            {
                thisBody.AddForce(new Vector2(-KNOCKBACK_FORCE, 0));
            }
            else
            {
                thisBody.AddForce(new Vector2(KNOCKBACK_FORCE, 0));
            }
            if(health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.name.Contains("ladder"))
        {
            ladder = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.name.Contains("ladder"))
        {
            ladder = false;
        }
    }

    void checkPlatforms()
    {
        if (!ladderDown)
        {
            if (thisBody.velocity.y > 0)
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"),
                                               LayerMask.NameToLayer("Ground"), true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"),
                                               LayerMask.NameToLayer("Ground"), false);
            }
        }
    }
    
    
}
