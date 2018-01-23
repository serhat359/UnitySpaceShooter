using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float powerUpSpeed = 0.03f;
    public string type = "";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -powerUpSpeed);
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
