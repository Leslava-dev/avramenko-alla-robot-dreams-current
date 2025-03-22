using UnityEngine;

public class ExplosionSystem : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionForce = 100f;
    public float explosionRadius = 10f;
    public float upwardsModifier = 1.0f;
    public string explodableTag = "Explodable";

    [Header("Line Renderer Settings")]
    public LineRenderer lineRenderer;
    public float rayDuration = 2f;

    [Header("Crosshair Settings")]
    public GameObject crosshair;

    private float rayLifetime = 0f;

    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is not found on the object!");
            }
        }

        if (crosshair == null)
        {
            Debug.LogError("Crosshair is not assigned!");
        }

        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateExplosion();
        }

        if (lineRenderer.enabled)
        {
            rayLifetime -= Time.deltaTime;
            if (rayLifetime <= 0)
            {
                lineRenderer.enabled = false;
            }
        }
    }

    void CreateExplosion()
    {
        if (crosshair == null)
        {
            Debug.LogError("Crosshair is missing!");
            return;
        }

        Vector3 crosshairPosition = crosshair.transform.position;
        Vector3 direction = (crosshairPosition - transform.position).normalized;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        Vector3 explosionPoint;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            explosionPoint = hit.point;
            Debug.Log($"Explosion at: {explosionPoint} ({hit.collider.name})");
        }
        else
        {
            explosionPoint = transform.position + direction * 100f;
            Debug.Log($"Explosion at default point: {explosionPoint}");
        }

        DrawRay(explosionPoint);
        ApplyExplosionForce(explosionPoint);
    }

    void ApplyExplosionForce(Vector3 explosionPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius);
        Debug.Log($"Found {colliders.Length} objects in explosion radius.");

        foreach (Collider col in colliders)
        {
            if (col.CompareTag(explodableTag))
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, explosionPoint, explosionRadius, upwardsModifier);
                    Debug.Log($"Applied explosion force to: {col.name}");
                }
            }
        }
    }

    void DrawRay(Vector3 point)
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned!");
            return;
        }

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, point);

        lineRenderer.enabled = true;
        rayLifetime = rayDuration;
    }
}
