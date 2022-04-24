using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AnyButton : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] bool canPress = true;
    GameControls controls;
    [SerializeField] TextMeshProUGUI timeText;

    private void Start()
    {
        TimeSpan finaltime = GameManager.Instance.GetTimer();
        Debug.Log("timer texted " + finaltime.ToString("mm':'ss'.'ff"));
        timeText.text = finaltime.ToString("mm':'ss'.'ff");
    }

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
        if (canPress)
        {
            StartCoroutine(ContinueSequence());
            canPress = false;
        }
    }

    IEnumerator ContinueSequence()
    {
        yield return new WaitForSeconds(.5f);

        fade.Play("Fade_In");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }
}
