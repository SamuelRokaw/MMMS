using UnityEngine;

public class PunchLogic : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.3f; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fadeDuration = 0.3f;

    private float timer = 0f;
    private SpriteRenderer punchSR;
    private Color startColor;
    private Vector3 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Awake()
    {
        punchSR = GetComponent<SpriteRenderer>();
        if (punchSR != null)
            startColor = punchSR.color;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
        if (punchSR != null)
        {
            float t = timer / fadeDuration;
            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            punchSR.color = c;
        }
        
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}