using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public LayerMask walkableLayers;

    public virtual void Move(int x, int y)
    {
        Vector3 newPos = transform.position;
        newPos.x += x;
        newPos.z += y;

        transform.position = newPos;
    }

    public virtual bool CanMove(int x, int y)
    {
        Vector3 moveDir = new Vector3(x, 0, y);
        Vector3 newPos = transform.position;
        newPos.x += x;
        newPos.z += y;

        return Physics.Raycast(newPos, Vector3.down, 1.0f, walkableLayers) && !Physics.Raycast(transform.position, moveDir, 1.0f);
    }
}
