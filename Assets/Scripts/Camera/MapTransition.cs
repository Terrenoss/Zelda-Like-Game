using System;
using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;
    [SerializeField] private Direction direction;
    [SerializeField] private float additiveValue = 3;
    
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
                break;
            case Direction.Down:
                newPos.y -= additiveValue;
                break;
            case Direction.Left:
                newPos.x += additiveValue;
                break;
            case Direction.Right:
                newPos.x -= additiveValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        player.transform.position = newPos;
    }
}
