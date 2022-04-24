using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopMusic : MonoBehaviour
{
    [SerializeField] bool value = true;

    // Start is called before the first frame update
    void Start()
    {
        if (value)
        {
            GameManager.Instance.StartMusic();
        }
        else
        {
            GameManager.Instance.FadeOutMusic();
        }
    }
}
