using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public float walkForce= 50f;
    public float jumpForce = 200f;
    private int jumpingCount = 0;
    public const int MAX_JUMP_COUNT = 6;
    private bool holstered = true;
    private bool grounded = false;
    private Vector3 speed;
    Rigidbody2D thisBody;
	// Use this for initialization
	void Start () {
        transform.position = new Vector2(-3, -3);
        holstered = true;
        speed = new Vector3(0, 0);
        thisBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        readKeys();
        Debug.Log(speed.y);

	}

    void readKeys()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            thisBody.AddForce(new Vector2(-walkForce, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            thisBody.AddForce(new Vector2(walkForce, thisBody.velocity.y));
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            thisBody.AddForce(new Vector2(0, jumpForce));
        }

        
        
    }
    
    
}
