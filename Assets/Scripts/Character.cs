using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public const float MAX_WALK_SPEED = 3f;
    public const float MAX_WALK_UNHOLSTERED_SPEED = 1f;
    public const float SLOW_FORCE = 20f;
    public const float KNOCKBACK_FORCE = 2000f;
    public const float MAX_AIM_ANGLE = 75f;
    public const float MIN_AIM_ANGLE = -30f;
    public const float ANGLE_AIM_RATE = 30f;

    public float groundRayDist;
    private int invinceTimer = 0;
    private int health = 5;
    public float walkForce= 500f;
    public float walkUnholsteredForce = 300f;
    public float jumpForce = 1000f;
    public float ladderSpeed = 3f;
    private bool holstered = true;
    private bool grounded = false;
    private bool moving = false;
    private bool ladder = false;
    private bool ladderDown = false;
    private GameObject arm;
    Rigidbody2D thisBody;

    public int faceDirection = 1;
    public float aimAngle = 15f;

    public const float MAX_PROJECTILE_COOLDOWN = 1f;
    public const float MAX_HOLSTER_COOLDOWN = 1f;
    public Transform projectile;

    private float projectileCooldown = 0f;
    private float holsterCooldown = 0f;
    RaycastHit2D ground;

	// Use this for initialization
	void Start () {
        arm = gameObject.transform.Find("CharacterArm").gameObject;
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

        if (holsterCooldown > 0)
        {
            holsterCooldown -= Time.deltaTime;
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
            float forceApply = walkForce;
            float maxSpeed = MAX_WALK_SPEED;

            if (!holstered)
            {
                forceApply = walkUnholsteredForce;
                maxSpeed = MAX_WALK_UNHOLSTERED_SPEED;
            }
            else if (faceDirection == 1)
            {
                transform.localScale += new Vector3(-2f * transform.localScale.x, 0, 0);
                faceDirection = -1;
            }

            if (thisBody.velocity.x > -maxSpeed)
            {
                thisBody.AddForce(new Vector2(-forceApply, 0));
            }
            else
            {
                thisBody.velocity = new Vector2(-maxSpeed, thisBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moving = true;
            float forceApply = walkForce;
            float maxSpeed = MAX_WALK_SPEED;

            if (!holstered)
            {
                forceApply = walkUnholsteredForce;
                maxSpeed = MAX_WALK_UNHOLSTERED_SPEED;
            }
            else if (faceDirection == -1)
            {
                transform.localScale += new Vector3 (-2f * transform.localScale.x, 0, 0);
                faceDirection = 1;
            }

            if (thisBody.velocity.x < maxSpeed)
            {
                thisBody.AddForce(new Vector2(forceApply, thisBody.velocity.y));
            }
            else
            {
                thisBody.velocity = new Vector2(maxSpeed, thisBody.velocity.y);
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!ladder && holstered)
            {
                ground = Physics2D.Raycast(transform.position, Vector2.down, groundRayDist);
                if (ground.collider && ground.collider.gameObject.name.Contains("Ground"))
                {

                    thisBody.AddForce(new Vector2(0, jumpForce));

                }
            }
            
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            if (ladder && holstered)
            {
                thisBody.velocity = new Vector2(thisBody.velocity.x, ladderSpeed);
            }
            if (!holstered)
            {
                if (aimAngle < MAX_AIM_ANGLE)
                {
                    aimAngle += Time.deltaTime * ANGLE_AIM_RATE;
                }
                else
                {
                    aimAngle = MAX_AIM_ANGLE;
                }
            }
        }
        ladderDown = false;
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if (ladder && holstered)
            {
                ladderDown = true;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"),
                               LayerMask.NameToLayer("Ground"), true);
                thisBody.velocity = new Vector2(thisBody.velocity.x, -ladderSpeed);
            }
            if (!holstered)
            {
                if (aimAngle > MIN_AIM_ANGLE)
                {
                    aimAngle -= Time.deltaTime * ANGLE_AIM_RATE;
                }
                else
                {
                    aimAngle = MIN_AIM_ANGLE;
                }
            }
        }
        moving = false;
        
        if(Input.GetKey(KeyCode.X) && projectileCooldown <= 0f && !holstered)
        {
            projectileCooldown = MAX_PROJECTILE_COOLDOWN;
            float trueAngle = aimAngle;
            if (faceDirection < 0)
            {
                trueAngle = 180 - trueAngle;
            }
            Instantiate(projectile, arm.transform.position + new Vector3(Mathf.Cos(trueAngle * Mathf.Deg2Rad), Mathf.Sin(trueAngle* Mathf.Deg2Rad)), Quaternion.AngleAxis(trueAngle, Vector3.forward));
        }

        if (Input.GetKey(KeyCode.Z) && holsterCooldown <= 0f)
        {
            holsterCooldown = MAX_HOLSTER_COOLDOWN;
            holstered = !holstered;
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
        if(coll.gameObject.name.Contains("Enemy") && invinceTimer <= 0)
        {
            invinceTimer = 20;
            health--;
            if(thisBody.velocity.x > 0)
            {
                thisBody.AddForce(new Vector2(-KNOCKBACK_FORCE, 0));
            }
            else if(thisBody.velocity.x < 0)
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

    public int getHealth()
    {
        return health;
    }
    
    
}
