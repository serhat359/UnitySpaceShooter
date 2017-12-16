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
    public AllPowerupsScript powerUps;
    public AudioSource backGroundMusic;

    private int waveCountBeforeBoss = 4;
    private int enemyCountInWave = 4;
    private Animator enemyAnimator;
    private int lastUsedWaveId = 0;

    [HideInInspector]
    public Dictionary<int, int> waveKillings;

    [Range(0, 1)]
    public float powerupDropProbability = 0.5f;

    // Use this for initialization
    void Start()
    {
        instance = this;
        isAlive = true;
        gameOverScreen.SetActive(false);
        gameOverWinImage.SetActive(false);
        gameOverLoseImage.SetActive(false);

        enemyAnimator = enemyAnimations.GetComponent<Animator>();

        waveKillings = new Dictionary<int, int>();

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
        int animationNumber = Random.Range(0, enemyAnimator.GetInteger(Parameters.AnimationCount)) + 1;

        int waveId = ++lastUsedWaveId;
        waveKillings.Add(waveId, enemyCountInWave);

        for (int i = 0; i < enemyCountInWave; i++)
        {
            var newEnemy = Instantiate(enemyObject, new Vector3(1.23f, 2.24f, 0), Quaternion.identity);
            Animator newEnemyAnimator = newEnemy.AddComponent<Animator>();
            newEnemy.GetComponent<EnemyScript>().waveId = waveId;

            newEnemyAnimator.runtimeAnimatorController = enemyAnimator.runtimeAnimatorController;
            newEnemyAnimator.SetTrigger(Parameters.Trigger + animationNumber);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void EnemyKilledInWave(int waveId, Vector3 position)
    {
        int newCount = waveKillings[waveId] - 1;

        waveKillings[waveId] = newCount;

        if (newCount <= 0)
        {
            float random = Random.Range(0f, 1f);

            if (random < powerupDropProbability)
            {
                int randomPowerupIndex = Random.Range(0, PowerupType.AllPowerUps.Length);

                powerUps.DropPowerup(PowerupType.AllPowerUps[randomPowerupIndex], position);
            }
        }
    }

    IEnumerator StartBossBattle()
    {
        Instantiate(this.boss);

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
        backGroundMusic.Pause();
        Time.timeScale = 0;
    }
}
