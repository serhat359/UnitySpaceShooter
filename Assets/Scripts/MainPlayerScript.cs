using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    Animator animator;

    public float horizontalSpeedMultiplier = 3f;
    public float verticalSpeedMultiplier = 3f;
    public float laserBulletSpeed = 10f;
    public float fireRatePerSeconds = 5;
    public GameObject laserBulletTemp;
    public GameObject laserLocation;
    public GameObject laserBulletSound;
    public GameObject fireGroup;

    private bool canFire = true;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleFiring();
    }

    private void HandleFiring()
    {
        if (GameScript.instance.isAlive && canFire && Input.GetButton("Fire1"))
        {
            GameObject newBullet = Instantiate(this.laserBulletTemp);

            Rigidbody2D newBulletRigidBody = newBullet.GetComponent<Rigidbody2D>();

            // Set position
            newBullet.transform.position = this.laserLocation.transform.position;

            // Set velocity and direction
            newBulletRigidBody.velocity = Vector2.up * laserBulletSpeed;

            // Create a bullet sound
            if (this.laserBulletSound != null)
                Instantiate(this.laserBulletSound);

            StartCoroutine(DisableFire(1f / fireRatePerSeconds));
        }
    }

    private void HandleMovement()
    {
        float hax = Input.GetAxis("Horizontal");
        float vax = Input.GetAxis("Vertical");

        if (!GameScript.instance.isAlive)
        {
            hax = 0;
            vax = 0;
        }

        rigidbody2D.velocity = new Vector2(hax * horizontalSpeedMultiplier, vax * verticalSpeedMultiplier);
    }

    private IEnumerator DisableFire(float seconds)
    {
        canFire = false;
        yield return new WaitForSeconds(seconds);
        canFire = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogFormat("Player touched a: {0}", other.gameObject.tag);

        if (other.tag == Tags.Enemy)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger(Parameters.Die);
        fireGroup.SetActive(false);
        GameScript.instance.isAlive = false;
    }

    void DyingAnimationOver()
    {
        GameScript.instance.GameOverDead();
    }
}
