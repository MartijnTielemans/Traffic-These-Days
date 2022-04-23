using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Animator fade;
    [SerializeField] EnemyMovement[] enemies;
    [SerializeField] GameObject gameEndVCam;

    [Space]
    [Header("Levels")]
    [SerializeField] string[] levelStrings;
    [SerializeField] float[] levelTimes;
    int currentLevel;

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

    public void FadeIn()
    {
        fade.Play("Fade_In");
    }

    public void FadeOut()
    {
        fade.Play("Fade_Out");
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
        StartCoroutine(LevelEndSequence());
    }

    IEnumerator LevelEndSequence()
    {
        yield return new WaitForSeconds(1);
        FadeOut();
        yield return new WaitForSeconds(2);

        // Increase current Level by 1
        currentLevel++;

        // Go to next level
        SceneManager.LoadScene(levelStrings[currentLevel]);
    }

    public void StartGameEnd()
    {
        StartCoroutine(GameEndSequence());
    }

    IEnumerator GameEndSequence()
    {
        // wait for a bit
        yield return new WaitForSeconds(1f);

        // Switch vcams
        gameEndVCam.SetActive(true);

        // Wait, then fade out
        yield return new WaitForSeconds(3.5f);
        FadeOut();

        // Switch the scene
        yield return new WaitForSeconds(1f);
    }
}
