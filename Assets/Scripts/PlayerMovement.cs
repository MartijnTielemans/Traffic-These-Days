using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    GameControls controls;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float maxSpeed = 40f;
    [SerializeField] float deceleration = 1.2f;
    [SerializeField] float movement;
    bool canMove;
    public bool movingForward, movingBackward;

    [Space]

    [Header("Spline")]
    [SerializeField] CinemachinePathBase path;
    [SerializeField] CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
    [SerializeField] float m_Position;

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Awake()
    {
        controls = new();

        // Add context to button presses
        controls.Player.Forward.performed += ctx => movingForward = true;
        controls.Player.Backward.performed += ctx => movingBackward = true;

        controls.Player.Forward.canceled += ctx => movingForward = false;
        controls.Player.Backward.canceled += ctx => movingBackward = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If can move is true, do movement
        if (canMove)
        {
            // If moving forward or backward, add movement to that direction
            if (movingForward)
            {
                AddMovement();
            }

            if (movingBackward)
            {
                BackMovement();
            }

            // Apply the movement
            Move();

            // If no button is being pressed, decelerate
            if (movement > 0 && !movingForward || movement < 0 && !movingBackward)
            {
                movement = Mathf.Lerp(movement, 0, deceleration * Time.deltaTime);
            }
        }
    }

    // Adds positive movement
    void AddMovement()
    {
        if (movement < maxSpeed)
        {
            movement += moveSpeed;
        }
        else if (movement >= maxSpeed)
        {
            movement = maxSpeed;
        }
    }

    // Adds negative movement
    void BackMovement()
    {
        if (movement > -maxSpeed)
        {
            movement -= moveSpeed;
        }
        else if (movement <= maxSpeed)
        {
            movement = -maxSpeed;
        }
    }

    void Move()
    {
        m_Position += movement * Time.deltaTime;
        transform.rotation = TrackRotation(m_Position);
        transform.position = GetNewPosition();
    }

    // Returns a Quaternion based on track rotation
    Quaternion TrackRotation(float distanceAlongPath)
    {
        m_Position = path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
        Quaternion r = path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        return r;
    }

    // Returns a Vector3 based on track position
    Vector3 GetNewPosition()
    {
        Vector3 newLocation = new Vector3(path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).x, path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).y, path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).z);
        return newLocation;
    }
}