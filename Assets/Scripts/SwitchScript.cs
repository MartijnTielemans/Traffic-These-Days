using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] Color disabledColor;
    [SerializeField] MeshRenderer switchRenderer;

    [Space]

    [SerializeField] CinemachinePathBase pathToSwitchTo;

    [SerializeField] GameObject oldSpline;
    [SerializeField] GameObject newSpline;

    bool activated = false;

    // Called when player triggers switch
    // Deactivates one path and activates the other
    public void SwitchPaths(PlayerMovement player)
    {
        // deactivate old spline and activate the new one
        oldSpline.SetActive(false);
        newSpline.SetActive(true);

        // Set player to new path
        player.SetPath(pathToSwitchTo);

        // Find closest path point and set player location
        player.ResetPosition();

        SetActivated();
    }

    // Disables the switch script and changes material color
    public void SetActivated()
    {
        switchRenderer.material.SetColor("_MainColor", disabledColor);
        activated = true;
    }

    public bool GetActivated()
    {
        return activated;
    }
}
