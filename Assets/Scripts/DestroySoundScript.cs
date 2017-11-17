using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        float audioLength = GetComponent<AudioSource>().clip.length;

        Destroy(gameObject, audioLength);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
