using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public float maxWalkingSpeed = .2f;
    public float jumpSpeed = .3f;
    private int jumpingCount = 0;
    public const int MAX_JUMP_COUNT = 5;
    private bool holstered = true;
    private bool grounded = false;
    private Vector3 speed;
	// Use this for initialization
	void Start () {
        transform.position = new Vector2(-3, -3);
        holstered = true;
        speed = new Vector3(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        readKeys();
        readMovement();
        Debug.Log(speed.y);

	}

    void readKeys()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if(speed.x >= -(maxWalkingSpeed))
            {
                speed.x -= .03f;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (speed.x <= maxWalkingSpeed)
            {
                speed.x += .03f;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            if(holstered)
            {
                jumpingCount = MAX_JUMP_COUNT;
                grounded = false;
            }
        }
        transform.Translate(speed);
        
        
    }

    void readMovement()
    {
        if(speed.x > 0)
        {
            speed.x -= .01f;
        }
        else if(speed.x < 0)
        {
            speed.x += .01f;
        }
        if(Mathf.Abs(speed.x)<.005f)
        {
            speed.x = 0f;
        }
        if (jumpingCount > 0)
        {
            jumpingCount--;
            speed.y = jumpSpeed;
        }
        else
        {
            if (speed.y > 0)
            {
                speed.y -= .01f;
            }
            else if (speed.y < 0)
            {
                speed.y += .01f;
            }
            if (Mathf.Abs(speed.y) < .005f)
            {
                speed.y = 0f;
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.name.Contains("Ground"))
        {
            grounded = true;
        }
    }
}
