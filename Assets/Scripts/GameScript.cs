using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public static GameScript instance;

    public GameObject enemyObject;
    public GameObject enemyAnimations;
    public int score;

    // Use this for initialization
    void Start()
    {
        instance = this;

        StartCoroutine(StartEnemySpawning());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartEnemySpawning()
    {
        while (true)
        {
            yield return CreateNewWave();

            yield return new WaitForSeconds(5f);
        }

        //yield return CreateNewWave();
    }

    IEnumerator CreateNewWave()
    {
        for (int i = 0; i < 4; i++)
        {
            var newEnemy = Instantiate(enemyObject, new Vector3(1.23f, 2.24f, 0), Quaternion.identity);
            Animator newEnemyAnimator = newEnemy.AddComponent<Animator>();

            newEnemyAnimator.runtimeAnimatorController = enemyAnimations.GetComponent<Animator>().runtimeAnimatorController;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
