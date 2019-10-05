using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovableObject : MonoBehaviour
{
    public LayerMask walkableLayers;

    public float moveTime = 0.2f;
    public float moveDelay = 0f;

    public virtual void Move(int x, int y)
    {
        Vector3 newPos = transform.position;
        newPos.x += x;
        newPos.z += y;

        transform.position = newPos;
    }

    public virtual IEnumerator LerpMove(int x, int y, UnityAction onFinish = null)
    {
        Vector3 origin = transform.position;
        Vector3 destination = transform.position + new Vector3(x, 0, y);

        if (moveDelay > 0)
        {
            yield return new WaitForSeconds(moveDelay);
        }

        for (float timeLeft = moveTime; timeLeft > 0; timeLeft -= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(origin, destination, 1 - (timeLeft / moveTime));
            yield return 0;     // Wait for next frame
        }

        transform.position = destination;

        if (onFinish != null)
        {
            onFinish.Invoke();
        }
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
