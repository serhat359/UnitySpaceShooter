using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour {

    new Rigidbody2D rigidbody2D;

    public float horizontalSpeedMultiplier = 3f;
    public float verticalSpeedMultiplier = 3f;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float hax = Input.GetAxis("Horizontal");
        float vax = Input.GetAxis("Vertical");
        
        rigidbody2D.velocity = new Vector2(hax * horizontalSpeedMultiplier, vax * verticalSpeedMultiplier);
    }
}
