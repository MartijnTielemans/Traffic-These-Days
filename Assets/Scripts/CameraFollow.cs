using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float followSpeedX = 9;
    [SerializeField] float followSpeedY = 15;

    Vector3 newPos;

    private void Start()
    {
        transform.position = followTarget.position;
    }

    void LateUpdate()
    {
        // Lerp to the follow target
        Vector3 posX = Vector3.Lerp(transform.position, followTarget.position, followSpeedX * Time.deltaTime);
        Vector3 posY = Vector3.Lerp(transform.position, followTarget.position, followSpeedY * Time.deltaTime);
        newPos = new Vector3(posX.x, posY.y, posX.z);

        // Apply the new position
        transform.position = newPos;
    }
}
