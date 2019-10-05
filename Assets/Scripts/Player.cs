using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PushyObject
{
    public float stepDelay = 1.0f;
    public bool inputEnabled = true;

    private float remainingDelay = 0.0f;

    private void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        );

        if (remainingDelay <= 0 && (input.x != 0 || input.y != 0) && inputEnabled)
        {
            Vector2Int moveDir = new Vector2Int((int)input.x, (int)(input.x == 0 ? input.y : 0));
            if (CanMove(moveDir.x, moveDir.y))
            {
                StartCoroutine(LerpMove(moveDir.x, moveDir.y, GameManager.Instance.CheckWinCondition));
                remainingDelay = stepDelay;
            }
        //} else if (input.x == 0 && input.y == 0)
        //{
        //    remainingDelay = 0; // Instantly reset delay if player stops pressing keys to allow for fast movement
        } else
        {
            remainingDelay -= Time.deltaTime;
        }
    }
}
