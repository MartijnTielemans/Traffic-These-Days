using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Animator fade;
    [SerializeField] EnemyMovement[] enemies;

    // For the ending sequence
    GameObject regularCam;

    [Space]
    [Header("Levels")]
    [SerializeField] string[] levelStrings;
    [SerializeField] float gameTimer;
    TimeSpan timePlaying;
    bool isTiming;
    int currentLevel;

    private void Awake()
    {
        if (GameManager.Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // For the timer functions
        if (isTiming)
        {
            gameTimer += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(gameTimer);
        }
    }

    // gets every object with the enemy tag in the scene
    public void GetEnemies()
    {
        List<GameObject> enemyList = new();
        List<EnemyMovement> scripts = new();

        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        for (int i = 0; i < enemyList.Count; i++)
        {
            scripts.Add(enemyList[i].GetComponent<EnemyMovement>());
        }

        enemies = scripts.ToArray();
    }

    public void GetFade()
    {
        fade = GameObject.Find("Fade").GetComponent<Animator>();
    }

    public void FadeIn()
    {
        fade.Play("Fade_In");
    }

    public void FadeOut()
    {
        fade.Play("Fade_Out");
    }

    public string[] GetLevels()
    {
        return levelStrings;
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void PauseTimer()
    {
        isTiming = false;
    }

    public TimeSpan GetTimer()
    {
        return timePlaying;
    }

    public void ResetEnemyPositions()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ResetPosition();
        }
    }

    public void LevelEnd()
    {
        PauseTimer();
        StartCoroutine(LevelEndSequence());
    }

    IEnumerator LevelEndSequence()
    {
        yield return new WaitForSeconds(1);
        FadeIn();
        yield return new WaitForSeconds(2);

        // Increase current Level by 1
        currentLevel++;

        // Go to next level
        SceneManager.LoadScene(levelStrings[currentLevel]);
    }

    public void StartGameEnd()
    {
        PauseTimer();
        StartCoroutine(GameEndSequence());
    }

    IEnumerator GameEndSequence()
    {
        // wait for a bit
        yield return new WaitForSeconds(1f);

        // Switch vcams
        regularCam = GameObject.Find("VCam");
        regularCam.SetActive(false);

        // Wait, then fade out
        yield return new WaitForSeconds(2f);
        FadeOut();

        // Switch the scene
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndingScene");
    }

    public void ResetValues()
    {
        currentLevel = 0;
        gameTimer = 0;
    }
}
