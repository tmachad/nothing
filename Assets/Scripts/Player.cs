using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float stepDelay = 1.0f;

    public LayerMask walkableLayers;

    private float remainingDelay = 0.0f;

    private void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        );

        if (remainingDelay <= 0 && (input.x != 0 || input.y != 0))
        {
            Move((int)input.x, (int)(input.x == 0 ? input.y : 0));
            remainingDelay = stepDelay;
        } else if (input.x == 0 && input.y == 0)
        {
            remainingDelay = 0; // Instantly reset delay if player stops pressing keys to allow for fast movement 
        } else
        {
            remainingDelay -= Time.deltaTime;
        }
    }

    private void Move(int x, int y)
    {
        Vector3 pos = transform.position;
        pos.x += x;
        pos.z += y;

        if (Physics.Raycast(pos, Vector3.down, 1.0f, walkableLayers))
        {
            // Walkable surface detected in the next spot, so just move there
            transform.position = pos;
        } else
        {
            // No walkable surface detected, play some sort of animation to notify the player
        }
    }
}
