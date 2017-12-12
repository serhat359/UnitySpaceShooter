using System;
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
    public GameObject bossDyingSound;
    public RuntimeAnimatorController spareAnimationController;
    public GameObject bossChargedLaser;
    public GameObject bossChargingLaser;
    public GameObject bossChargingLaserSound;
    public GameObject bossChargedLaserSound;

    Animator animator;
    private Vector3 positionOfDeath;
    private int currentExplosionCount = 0;
    private const int explosionCount = 5;
    private bool firingNormally = true;
    private Coroutine bossChargeAttack;
    private GameObject playingSound;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        SetEnabledLaser(bossChargedLaser, false);
        SetEnabledLaser(bossChargingLaser, false);

        this.bossChargeAttack = StartCoroutine(PrepareBossChargedAttack());
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

            StartCoroutine(DisableFireFor(1f / fireRatePerSeconds));
        }
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
            }
        }
        else if (other.tag == Tags.Player)
        {
            other.GetComponent<MainPlayerScript>().Die();
        }
    }

    void LoopAnimationStart()
    {
        canFire = true;
    }

    void DestroyedAnimationBegin()
    {
        if (currentExplosionCount < explosionCount)
        {
            gameObject.transform.position = positionOfDeath;
            currentExplosionCount++;
            Instantiate(bossDyingSound);
        }
        else
        {
            GameScript.instance.GameOverWin();
        }
    }

    private IEnumerator PrepareBossChargedAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            DisableFire();
            SetEnabledLaser(bossChargingLaser, true);
            playingSound = Instantiate(this.bossChargingLaserSound);
            float audioLength = this.bossChargingLaserSound.GetComponent<AudioSource>().clip.length;

            yield return new WaitForSeconds(audioLength);

            SetEnabledLaser(bossChargingLaser, false);

            SetEnabledLaser(bossChargedLaser, true);
            playingSound = Instantiate(this.bossChargedLaserSound);

            yield return new WaitForSeconds(1);

            SetEnabledLaser(bossChargedLaser, false);

            yield return new WaitForSeconds(2);

            EnableFire();
        }
    }

    private void SetEnabledLaser(GameObject laser, bool value)
    {
        laser.GetComponent<SpriteRenderer>().enabled = value;
        laser.GetComponent<Collider2D>().enabled = value;
    }

    private void DisableFire()
    {
        firingNormally = false;

        canFire = false;
    }

    private void EnableFire()
    {
        firingNormally = true;

        canFire = true;
    }

    private IEnumerator DisableFireFor(float seconds)
    {
        if (firingNormally)
            canFire = false;

        yield return new WaitForSeconds(seconds);

        if (firingNormally)
            canFire = true;
    }

    private void Die()
    {
        StopCoroutine(bossChargeAttack);
        SetEnabledLaser(bossChargedLaser, false);
        SetEnabledLaser(bossChargingLaser, false);
        if (playingSound != null)
            Destroy(playingSound);

        positionOfDeath = gameObject.transform.position;
        isBossAlive = false;
        animator.runtimeAnimatorController = spareAnimationController;
    }
}
