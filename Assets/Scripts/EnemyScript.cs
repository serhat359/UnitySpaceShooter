using UnityEngine;

public class EnemyScript : MonoBehaviour, IAnimatedLater
{
    public int waveId = 0;
    public bool killedByAnimation = false;

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
        killedByAnimation = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Enemy touched a: {0}", other.gameObject.tag);
    }

    private void OnDestroy()
    {
        if (!killedByAnimation)
            GameScript.instance.EnemyKilledInWave(waveId, transform.position);
    }
}
