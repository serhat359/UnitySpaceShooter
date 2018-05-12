using System;
using System.Collections;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    Animator animator;

    public float horizontalSpeedMultiplier = 3f;
    public float horizontalSpeedSecondMultiplier = 6f;
    public float verticalSpeedMultiplier = 3f;
    public float laserBulletSpeed = 10f;
    public float fireRatePerSeconds = 5;
    public GameObject laserBulletTemp;
    public GameObject laserLocation;
    public GameObject laserBulletSound;
    public GameObject fireGroup;
    public GameObject dyingSound;

    public GameObject wings1;
    private bool isWings1Active;

    public GameObject power1;
    private bool isPower1Active;

    public GameObject gunLeft;
    public GameObject gunRight;

    private bool canFire = true;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ResetPlayer();
    }

    private void ResetPlayer()
    {
        SetEnabledPart(wings1, false);
        isWings1Active = false;

        SetEnabledPart(power1, false);
        isPower1Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleFiring();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameScript.instance.PauseGame();
        }
    }

    private void HandleFiring()
    {
        if (GameScript.instance.controllerState == GameScript.ControllerState.Player && GameScript.instance.isAlive && canFire && CustomInput.GetButton("Fire1"))
        {
            GameObject newBullet = Instantiate(this.laserBulletTemp);
            Rigidbody2D newBulletRigidBody = newBullet.GetComponent<Rigidbody2D>();
            // Set position
            newBullet.transform.position = this.laserLocation.transform.position;
            // Set velocity and direction
            newBulletRigidBody.velocity = Vector2.up * laserBulletSpeed;

            if (isPower1Active)
            {
                GameObject rightBullet = Instantiate(this.laserBulletTemp, this.gunRight.transform.position, gunRight.transform.rotation);
                Rigidbody2D rightBulletRigidBody = rightBullet.GetComponent<Rigidbody2D>();
                rightBulletRigidBody.velocity = gunRight.transform.rotation * Vector2.up * laserBulletSpeed;

                GameObject leftBullet = Instantiate(this.laserBulletTemp, this.gunLeft.transform.position, gunLeft.transform.rotation);
                Rigidbody2D leftBulletRigidBody = leftBullet.GetComponent<Rigidbody2D>();
                leftBulletRigidBody.velocity = gunLeft.transform.rotation * Vector2.up * laserBulletSpeed;
            }

            Debug.Log(gunLeft.transform.rotation.eulerAngles.z);

            // Create a bullet sound
            if (this.laserBulletSound != null)
                Instantiate(this.laserBulletSound);

            StartCoroutine(DisableFire(1f / fireRatePerSeconds));
        }
    }

    private void HandleMovement()
    {
        float hax = CustomInput.GetAxis("Horizontal");
        float vax = CustomInput.GetAxis("Vertical");

        if (!(GameScript.instance.controllerState == GameScript.ControllerState.Player && GameScript.instance.isAlive))
        {
            hax = 0;
            vax = 0;
        }

        float horSpeedMultiplier = isWings1Active ? horizontalSpeedSecondMultiplier : horizontalSpeedMultiplier;

        rigidbody2D.velocity = new Vector2(hax * horSpeedMultiplier, vax * verticalSpeedMultiplier);
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

    public void Die()
    {
        if (GameScript.instance.isAlive)
        {
            ResetPlayer();
            animator.SetTrigger(Parameters.Die);
            Instantiate(dyingSound);
            fireGroup.SetActive(false);
            GameScript.instance.isAlive = false;
        }
    }

    public void GiveAbilityPowerup(string type)
    {
        if (type == PowerupType.Speed)
        {
            SetEnabledPart(wings1, true);
            isWings1Active = true;
        }
        else if (type == PowerupType.Power)
        {
            SetEnabledPart(power1, true);
            isPower1Active = true;
        }
        else
        {
            throw new Exception(string.Format("Powerup type: {0} is not recognized", type));
        }
    }

    void DyingAnimationOver()
    {
        GameScript.instance.GameOverDead();
    }

    private void SetEnabledPart(GameObject part, bool value)
    {
        part.GetComponent<SpriteRenderer>().enabled = value;
        part.GetComponent<Collider2D>().enabled = value;
    }

}
