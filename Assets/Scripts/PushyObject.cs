using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushyObject : MovableObject
{
    public LayerMask pushableLayers;

    public override void Move(int x, int y)
    {
        Vector3 moveDir = new Vector3(x, 0, y);
        RaycastHit hit;

        Physics.Raycast(transform.position, moveDir, out hit, 1.0f);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<MovableObject>() != null)
        {
            // There's something in the way that is movable
            hit.collider.gameObject.GetComponent<MovableObject>().Move(x, y);
        }

        Vector3 newPos = transform.position;
        newPos.x += x;
        newPos.z += y;

        transform.position = newPos;
    }

    public override IEnumerator LerpMove(int x, int y, UnityAction onFinish = null)
    {
        Vector3 moveDir = new Vector3(x, 0, y);
        RaycastHit hit;

        Physics.Raycast(transform.position, moveDir, out hit, 1.0f);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<MovableObject>() != null)
        {
            // There's something in the way that is movable
            StartCoroutine(hit.collider.gameObject.GetComponent<MovableObject>().LerpMove(x, y));
        }

        return base.LerpMove(x, y, onFinish);
    }

    public override bool CanMove(int x, int y)
    {
        Vector3 moveDir = new Vector3(x, 0, y);
        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.x += x;
        newPos.z += y;

        bool walkable = Physics.Raycast(newPos, Vector3.down, 1.0f, walkableLayers);
        bool blocked = Physics.Raycast(transform.position, moveDir, out hit, 1.0f);

        return (
            walkable &&   // New location has a walkable surface and
            (
                !blocked ||         // there is nothing in the way, or
                (
                    pushableLayers == (pushableLayers | (1 << hit.collider.gameObject.layer)) &&    // the object blocking the way can be pushed and
                    hit.collider.gameObject.GetComponent<MovableObject>().CanMove(x, y)             // the object blocking the way can move in the same direction
                )
            )
        );
    }
}
