using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] private Direction direction;
    [SerializeField] private Transform movePoint;
    private static bool midTransition = false;

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

        if (midTransition)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            print(movePoint);
            print(this);
            StartCoroutine(Transition(collision));
        }
    }

    private IEnumerator Transition(Collider2D collision)
    {
        midTransition = true;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        PlayerMovement playerScript = collision.GetComponent<PlayerMovement>();

        if (playerScript != null)
        {
            playerScript.ResetMovement();
        }
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        confiner.m_BoundingShape2D = mapBoundry;
        confiner.InvalidateCache();
        UpdatePlayerPosition(collision.gameObject);

        yield return new WaitForSeconds(.5f);
      
        midTransition = false;
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (playerScript != null)
        {
            playerScript.ResetMovement();
        }

     

        Vector2 newPosition = movePoint.transform.position;

   
        
       
        player.transform.position = newPosition;
        
    }
}