using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapTransitionNoDoor : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    Cinemachine.CinemachineConfiner confiner;   


private    void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundry;
            confiner.InvalidatePathCache();
        }
    }
}
