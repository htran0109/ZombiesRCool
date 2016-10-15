using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static bool keyFound = false;
    public static int level = 1;
    public GameObject ladder;
    public GameObject bottomGround;
    public GameObject platformGround;
    public GameObject key;
    public GameObject door;
    public GameObject turtle;


    public Vector3 ladder1Level2;
    public Vector3 ladder2Level2;
    public Vector3 bottomGroundLevel2;
    public Vector3 platformGround1Level2;
    public Vector3 platformGround2Level2;
    public Vector3 characterLevel2;
    public Vector3 turtleLevel2;
    public Vector3 doorLevel2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void switchLevel()
    {
        Debug.Log("Switching Level");
        if(level == 1)
        {
            level++;
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (!o.name.Contains("Character") && !o.name.Contains("Health")
                    && !o.name.Contains("GameManager") && !o.name.Contains("Background")
                    && !o.name.Contains("Camera"))
                {
                    Destroy(o);
                }
            }
            Instantiate(ladder, ladder1Level2, new Quaternion());
            Instantiate(ladder, ladder2Level2, new Quaternion());
            Instantiate(platformGround, platformGround1Level2, new Quaternion());
            Instantiate(bottomGround, bottomGroundLevel2, new Quaternion());
            Instantiate(turtle, turtleLevel2, new Quaternion());
            Instantiate(door, doorLevel2, new Quaternion());
            GameObject.Find("Character").transform.position = characterLevel2;
        }
    }
}
