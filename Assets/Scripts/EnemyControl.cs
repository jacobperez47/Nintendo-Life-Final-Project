using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed = 2f;
    [Tooltip("How close to a point before switching")]
    public float arriveThreshold = 0.05f;

    private Rigidbody2D rb;
    private Transform pointATransform;
    private Transform pointBTransform;
    private Transform targetPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("EnemyControl requires a Rigidbody2D on the same GameObject.");
            enabled = false;
            return;
        }

        if (pointA == null || pointB == null)
        {
            Debug.LogError("EnemyControl: pointA and pointB must be assigned in the Inspector.");
            enabled = false;
            return;
        }

        pointATransform = pointA.transform;
        pointBTransform = pointB.transform;

        // start heading to B
        targetPoint = pointBTransform;
    }

    // Use FixedUpdate when moving/setting Rigidbody2D positions/velocities
    void FixedUpdate()
    {
        if (targetPoint == null) return;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = targetPoint.position;

        // Move with physics-aware MovePosition to avoid tunneling/overshoot
        Vector2 next = Vector2.MoveTowards(currentPos, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        // flip sprite horizontally to face direction (optional; only if sprite faces +X originally)
        float dir = targetPos.x - currentPos.x;
        if (Mathf.Abs(dir) > 0.001f)
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Sign(dir) * Mathf.Abs(s.x);
            transform.localScale = s;
        }

        // arrive check using the new position to avoid missing the small window when overshooting
        if (Vector2.Distance(next, targetPos) <= arriveThreshold)
        {
            // swap target
            targetPoint = (targetPoint == pointBTransform) ? pointATransform : pointBTransform;
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pointA.transform.position, 0.2f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.2f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}
