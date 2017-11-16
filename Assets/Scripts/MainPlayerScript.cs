using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    public float horizontalSpeedMultiplier = 3f;
    public float verticalSpeedMultiplier = 3f;
    public float laserBulletSpeed = 10f;
    public float fireRatePerSeconds = 5;
    public GameObject laserBulletTemp;
    public GameObject laserLocation;
    public GameObject laserBulletSound;

    private bool canFire = true;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleFiring();
    }

    private void HandleFiring()
    {
        if (canFire && Input.GetButton("Fire1"))
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

        rigidbody2D.velocity = new Vector2(hax * horizontalSpeedMultiplier, vax * verticalSpeedMultiplier);
    }

    private IEnumerator DisableFire(float seconds)
    {
        canFire = false;
        yield return new WaitForSeconds(seconds);
        canFire = true;
    }
}
