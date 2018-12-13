using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    //Dna of the person, it will contain genes that will be passed on the offsprings
    

    //Gene fo colour
    public float r;
    public float g;
    public float b;
   

    SpriteRenderer sRenderer;
    Collider2D sCollider;

    bool dead = false; //to see if the person has been clicked on or not
    public float timeToDie = 0; //to store how long they have lived


    //size
    public float sX;
    public float sY;

    //On click
    private void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    // Use this for initialization
    void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);

        transform.localScale= new Vector3(sX, sY, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
