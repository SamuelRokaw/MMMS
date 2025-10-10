using UnityEngine;

public class PunchLogic : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fadeDuration = 0.5f;
    public int aP= 1;
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
        aP = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>().AttackPower;
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
            float fadeTimer = timer / fadeDuration;
            Color punchColor = startColor;
            punchColor.a = Mathf.Lerp(1f, 0f, fadeTimer);
            punchSR.color = punchColor;
        }
        
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}