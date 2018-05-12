using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerUpSpeed = 0.03f;
    public string type = "";

    // Use this for initialization
    void Start()
    {
        var rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = new Vector2(0, -powerUpSpeed * 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Powerup touched a: {0}", other.gameObject.tag);

        if (other.tag == Tags.Player)
        {
            other.GetComponent<MainPlayerScript>().GiveAbilityPowerup(type);
            Destroy(gameObject);
        }
    }
}
