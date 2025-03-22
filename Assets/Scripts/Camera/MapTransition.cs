using System;
using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;
    [SerializeField] private Direction direction;
    [SerializeField] private float additiveValue = 2;
    
    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(other.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;
        
        switch (direction)
        {
            case Direction.Up:
                newPos.y += additiveValue;
                direction = Direction.Down;
                break;
            case Direction.Down:
                newPos.y -= additiveValue;
                direction = Direction.Up;
                break;
            case Direction.Left:
                newPos.x += additiveValue;
                direction = Direction.Left;
                break;
            case Direction.Right:
                newPos.x -= additiveValue;
                direction = Direction.Right;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        player.transform.position = newPos;
    }
}
