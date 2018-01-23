using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject destroySound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Laser touched a: {0}", other.gameObject.tag);

        if (other.tag == Tags.Enemy)
        {
            GameScript.instance.score += 100;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
