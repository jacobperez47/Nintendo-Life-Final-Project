using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;
    [Header("Dashing")] [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    private Vector2 _dashDir;
    private float dashTimer;
    private bool canDash = true;
    private bool isDashing;
    AudioSource collectionSound;

    public CollectableManager collectableManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collectionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = _dashDir.normalized * dashSpeed;

            return;
        }

        rb.velocity = movement * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!isDashing)
        {
            movement = context.ReadValue<Vector2>();
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash && context.performed)
        {
            print("Dash");

            isDashing = true;
            canDash = false;
            _dashDir = movement;
            Physics2D.IgnoreLayerCollision(3, 6, true);
            if (_dashDir == Vector2.zero)
            {
                _dashDir = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);
            }

            StartCoroutine(StopDashing());
            StartCoroutine(DashCooldown());
        }
    }

    public void ResetMovement()
    {
        movement = Vector2.zero;


        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        isDashing = false;


        StopAllCoroutines();
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectableManager.count++;
        }

        if (other.CompareTag("Console"))
        {
            collectionSound.Play();
            print("console found");
            ConsoleManager instance = other.GetComponent<ConsoleManager>();
            CollectionTracker.add(instance.id);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        print("Stop Dashing");
        Physics2D.IgnoreLayerCollision(3, 6, false);

        isDashing = false;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}