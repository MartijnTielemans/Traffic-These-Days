using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyMovement : MonoBehaviour
{
    enum EnemyType { Yellow, Red, Green }

    [SerializeField] EnemyType type;

    [SerializeField] CinemachinePathBase path;
    public Cinemachine.CinemachinePathBase.PositionUnits m_PositionUnits = Cinemachine.CinemachinePathBase.PositionUnits.Distance;
    public float m_Position;
    float startM_Position;

    [Space]
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int direction = 1;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        startM_Position = m_Position;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Change behaviour dependent on type
            switch (type)
            {
                case EnemyType.Yellow:
                    YellowMove();
                    break;
                case EnemyType.Red:
                    RedMove();
                    break;
                case EnemyType.Green:
                    GreenMove();
                    break;
                default:
                    YellowMove();
                    break;
            }

            // Apply position and rotation
            transform.rotation = TrackRotation(m_Position);
            transform.position = GetNewPosition();
        }
    }

    // Yellow moves, then teleports back to start
    void YellowMove()
    {
        if (m_Position >= path.PathLength)
        {
            m_Position = 0;
        }
        else
        {
            m_Position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Red moves back and forth
    void RedMove()
    {
        // Reverse direction if the end points are reached
        if (m_Position >= path.PathLength && direction > 0)
        {
            SwitchDirection();
        }
        else if (m_Position == startM_Position && direction < 0)
        {
            SwitchDirection();
        }
        else
        {
            m_Position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Green always keeps moving until it can no longer
    void GreenMove()
    {
        m_Position += direction * moveSpeed * Time.deltaTime;
    }

    public void SwitchDirection()
    {
        direction = -direction;
    }

    Quaternion TrackRotation(float distanceAlongPath)
    {
        m_Position = path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
        Quaternion r = path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        return r;
    }

    Vector3 GetNewPosition()
    {
        Vector3 newLocation = new Vector3(path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).x, path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).y, path.EvaluatePositionAtUnit(m_Position, m_PositionUnits).z);
        return newLocation;
    }
}
