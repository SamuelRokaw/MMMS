using UnityEngine;

public class OverworldMovement : MonoBehaviour
{
    // movement objects idk
    private Rigidbody2D owPlayerRB;
    
    //movement stats
    public float speed = 1f;
    public float spdMod = 1f; //for dash and similar abilities
    
    private void Awake()
    {
        owPlayerRB = GetComponent<Rigidbody2D>();
    }

    public void moveup()
    {
        owPlayerRB.MovePosition(owPlayerRB.position + Vector2.up * speed * Time.deltaTime);
    }

    public void movedown()
    {
        owPlayerRB.MovePosition(owPlayerRB.position + Vector2.down * speed * Time.deltaTime);
    }

    public void moveleft()
    {
        owPlayerRB.MovePosition(owPlayerRB.position + Vector2.left * speed * Time.deltaTime);
    }

    public void moveright()
    {
        owPlayerRB.MovePosition(owPlayerRB.position + Vector2.right * speed * Time.deltaTime);
    }
}
