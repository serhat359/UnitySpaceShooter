using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public static GameScript instance;

    public int score = 0;
    public bool isAlive = true;
    public GameObject enemyObject;
    public GameObject enemyAnimations;
    public GameObject gameOverScreen;
    public GameObject gameOverWinImage;
    public GameObject gameOverLoseImage;
    public GameObject boss;

    private int waveCountBeforeBoss = 4;

    // Use this for initialization
    void Start()
    {
        instance = this;
        isAlive = true;
        gameOverScreen.SetActive(false);
        gameOverWinImage.SetActive(false);
        gameOverLoseImage.SetActive(false);

        StartCoroutine(StartEnemySpawning());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartEnemySpawning()
    {
        for (int i = 0; i < waveCountBeforeBoss; i++)
        {
            yield return CreateNewWave();

            yield return new WaitForSeconds(5f);
        }

        yield return StartBossBattle();
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

    IEnumerator StartBossBattle()
    {
        GameObject boss = Instantiate(this.boss);

        // This line was written for syntax purposes
        yield return new WaitForSeconds(0);
    }

    public void GameOverDead()
    {
        gameOverLoseImage.SetActive(true);
        GameOver();
    }

    public void GameOverWin()
    {
        gameOverWinImage.SetActive(true);
        GameOver();
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
