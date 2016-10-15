using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Sprite health5;
    public Sprite health4;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    public Sprite health0;

    private SpriteRenderer spriteRenderer;
    private Character character;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = health5;
        character = (Character)GameObject.Find("Character").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        checkHealth();	
	}

    void checkHealth()
    {
        if(character.getHealth() == 5)
        {
            spriteRenderer.sprite = health5;
        }
        else if(character.getHealth() == 4)
        {
            spriteRenderer.sprite = health4;
        }
        else if (character.getHealth() == 3)
        {
            spriteRenderer.sprite = health3;
        }
        else if (character.getHealth() == 2)
        {
            spriteRenderer.sprite = health2;
        }
        else if (character.getHealth() == 1)
        {
            spriteRenderer.sprite = health1;
        }
        else { 
            spriteRenderer.sprite = health0;
        }
    }
}
