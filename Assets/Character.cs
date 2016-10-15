using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public const float MAX_WALK_SPEED = 3f;
    public const float SLOW_FORCE = 20f;
    public float walkForce= 50f;
    public float jumpForce = 1000f;
    private bool holstered = true;
    private bool grounded = false;
    private bool moving = false;
    Rigidbody2D thisBody;
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
        slowWalk();
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

            ground = Physics2D.Raycast(transform.position, Vector2.down, 1f);
            if (ground.collider.gameObject.name.Contains("Ground"))
            {
                Debug.Log(ground.distance);
                thisBody.AddForce(new Vector2(0, jumpForce));

            }
        }
        moving = false;

        
        
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
