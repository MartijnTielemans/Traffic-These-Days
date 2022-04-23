using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateAmount = new Vector3(0, 0, -3.5f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount);
    }
}
