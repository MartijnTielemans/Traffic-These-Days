using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Animator fade;
    [SerializeField] EnemyMovement[] enemies;

    [Space]
    [Header("Levels")]
    [SerializeField] string[] levelStrings;
    [SerializeField] float[] levelTimes;
    int currentLevel;

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
}
