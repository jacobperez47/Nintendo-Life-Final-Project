using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] private Direction direction;

    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundry;
            print("changing map boundry");
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPosition = player.transform.position;

        switch (direction)
        {
            case Direction.UP:
                newPosition.y += 2;
                break;
            case Direction.DOWN:
                newPosition.y -= 2;
                break;
            case Direction.LEFT:
                newPosition.x -= 2;
                break;
            case Direction.RIGHT:
                newPosition.x += 2;
                break;
        }
        
        player.transform.position = newPosition;
    }
}
