using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool canFire = false;
    public bool isBossAlive = true;
    public float fireRatePerSeconds = 2f;
    public float bossLaserBulletSpeed = 10f;
    public float remainingLife = 3000f;
    public GameObject bossBulletTemp;
    public GameObject bulletLocation1;
    public GameObject bulletLocation2;
    public GameObject bossLaserBulletSound;
    public RuntimeAnimatorController spareAnimationController;

    Animator animator;
    private Vector3 positionOfDeath;
    private int currentExplosionCount = 0;
    private const int lastExplosionCount = 5;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire && isBossAlive)
        {
            GameObject bullet1 = Instantiate(this.bossBulletTemp);
            GameObject bullet2 = Instantiate(this.bossBulletTemp);

            Rigidbody2D bulletRigidBody1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRigidBody2 = bullet2.GetComponent<Rigidbody2D>();

            // Set position
            bullet1.transform.position = this.bulletLocation1.transform.position;
            bullet2.transform.position = this.bulletLocation2.transform.position;

            // Set velocity and direction
            bulletRigidBody1.velocity = Vector2.down * bossLaserBulletSpeed;
            bulletRigidBody2.velocity = Vector2.down * bossLaserBulletSpeed;

            // Create a bullet sound
            if (this.bossLaserBulletSound != null)
                Instantiate(this.bossLaserBulletSound);

            StartCoroutine(DisableFire(1f / fireRatePerSeconds));
        }
    }

    private IEnumerator DisableFire(float seconds)
    {
        canFire = false;
        yield return new WaitForSeconds(seconds);
        canFire = true;
    }

    void LoopAnimationStart()
    {
        canFire = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Boss touched a: {0}", other.gameObject.tag);

        if (other.tag == Tags.Laser)
        {
            if (remainingLife > 0)
            {
                remainingLife -= 100;
                GameScript.instance.score += 50;
                Destroy(other.gameObject);
            }

            if (remainingLife <= 0)
            {
                Die();

                if (isBossAlive)
                    Debug.LogFormat("Boss position of death: {0}", gameObject.transform.position);
            }
        }
        else if (other.tag == Tags.Player)
        {
            other.GetComponent<MainPlayerScript>().Die();
        }
    }

    void Die()
    {
        positionOfDeath = gameObject.transform.position;
        isBossAlive = false;
        animator.runtimeAnimatorController = spareAnimationController;
    }

    void DestroyedAnimationBegin()
    {
        gameObject.transform.position = positionOfDeath;
        currentExplosionCount++;

        if (currentExplosionCount > lastExplosionCount)
        {
            GameScript.instance.GameOverWin();
        }
    }
}
