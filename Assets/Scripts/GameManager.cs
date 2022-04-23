using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Animator fade;
    [SerializeField] EnemyMovement[] enemies;

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

        // Go to next level
    }
}
