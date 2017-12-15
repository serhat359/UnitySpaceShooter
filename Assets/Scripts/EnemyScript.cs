using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IAnimatedLater
{
    public int waveId = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void IAnimatedLater.AnimationOver()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Enemy touched a: {0}", other.gameObject.tag);
    }

    private void OnDestroy()
    {
        GameScript.instance.EnemyKilledInWave(waveId, transform.position);
    }
}
