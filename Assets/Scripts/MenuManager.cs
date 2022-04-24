using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] float timer = 1.2f;

    public void StartButton()
    {
        GameManager.Instance.ResetValues();
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(timer / 5);

        fade.Play("Fade_In");

        yield return new WaitForSeconds(timer);

        SceneManager.LoadScene(GameManager.Instance.GetLevels()[0]);
    }

    public void ExitButton()
    {
        StartCoroutine(ExitSequence());
    }

    IEnumerator ExitSequence()
    {
        yield return new WaitForSeconds(timer / 5);

        fade.Play("Fade_In");

        yield return new WaitForSeconds(timer);

        Application.Quit();
    }
}
