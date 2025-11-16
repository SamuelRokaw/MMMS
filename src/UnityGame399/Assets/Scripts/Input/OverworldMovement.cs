using UnityEngine;

public class OverworldMovement : MonoBehaviour
{
    // movement objects idk
    [SerializeField] private Rigidbody2D playerRB;
    
    //movement stats
    public float speed = 1f;
    public float spdMod = 1f; //for dash and similar abilities
    
    private void Awake()
    {
        if(playerRB == null)
        {
            playerRB = GetComponent<Rigidbody2D>();
        }
    }

    public void moveup()
    {
        zerovelocity();
        playerRB.MovePosition(playerRB.position + Vector2.up * spdMod * speed * Time.deltaTime);
    }

    public void movedown()
    {
        zerovelocity();
        playerRB.MovePosition(playerRB.position + Vector2.down * speed * Time.deltaTime);
    }

    public void moveleft()
    {
        zerovelocity();
        playerRB.MovePosition(playerRB.position + Vector2.left * speed * Time.deltaTime);
    }

    public void moveright()
    {
        zerovelocity();
        playerRB.MovePosition(playerRB.position + Vector2.right * speed * Time.deltaTime);
    }
    
    private void zerovelocity()
    {
        playerRB.linearVelocity = Vector2.zero;
    }
}
