using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyButton : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] bool canPress = true;
    GameControls controls;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new();

        controls.UI.AnyKey.started += ctx => Continue();
    }

    void Continue()
    {
        Debug.Log(" Here");

        if (canPress)
        {
            StartCoroutine(ContinueSequence());
            canPress = false;
        }
    }

    IEnumerator ContinueSequence()
    {
        yield return new WaitForSeconds(.5f);

        fade.Play(" Fade_In");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }
}
