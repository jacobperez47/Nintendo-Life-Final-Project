using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnController : MonoBehaviour
{

    public static RespawnController Instance;

    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(respawn(other));

        }
    }

    private IEnumerator respawn(Collider2D other)
    {
        other.transform.position = respawnPoint.position;
        print("Respawn at " + other.transform.position);
        other.GetComponent<PlayerInput>().enabled = false;
        other.GetComponent<PlayerMovement>().ResetMovement();
        yield return new WaitForSeconds(0.2f);
        other.GetComponent<PlayerInput>().enabled = true;
    }
}