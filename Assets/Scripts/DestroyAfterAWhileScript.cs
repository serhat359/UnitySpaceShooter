using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAWhileScript : MonoBehaviour
{
    public float lifeSeconds = 0.1f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyThisInstance());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestroyThisInstance()
    {
        yield return new WaitForSeconds(lifeSeconds);
        Destroy(gameObject);
    }
}
