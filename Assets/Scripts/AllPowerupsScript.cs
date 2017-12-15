using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPowerupsScript : MonoBehaviour
{
    public GameObject powerupSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropPowerup(string type, Vector3 position)
    {
        GameObject powerUp;

        if (type == PowerupType.Speed)
        {
            powerUp = powerupSpeed;
        }
        else
        {
            throw new Exception(string.Format("Powerup type not recognized: {0}", type));
        }

        Instantiate(powerUp, position, Quaternion.identity);
    }
}
