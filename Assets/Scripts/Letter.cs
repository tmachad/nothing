using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MovableObject
{
    public LayerMask letterLayer;

    private Text textObj;

    private void Awake()
    {
        textObj = GetComponentInChildren<Text>();
    }

    public string GetText()
    {
        return textObj.text;
    }

    public string BuildWord(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 1.0f, letterLayer) && hit.collider.GetComponent<Letter>() != null && hit.collider.GetComponent<Letter>() != this)
        {
            // Hit another letter, so get their text too
            return GetText() + hit.collider.GetComponent<Letter>().BuildWord(dir);
        } else
        {
            // No adjacent letter in that direction, just return this letter's text
            return GetText();
        }
    }

    public bool IsWordEnd()
    {
        return (
            (Physics.Raycast(transform.position, new Vector3(0, 0, 1), 1.0f, letterLayer) ^ Physics.Raycast(transform.position, new Vector3(0, 0, -1), 1.0f, letterLayer)) ||
            (Physics.Raycast(transform.position, new Vector3(1, 0, 0), 1.0f, letterLayer) ^ Physics.Raycast(transform.position, new Vector3(-1, 0, 0), 1.0f, letterLayer))
        );
    }

    public Letter GetNeighbour(Vector3 dir)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, dir, out hit, 1.0f, letterLayer);
        if (hit.collider != null && hit.collider.GetComponent<Letter>() != null)
        {
            return hit.collider.GetComponent<Letter>();
        } else
        {
            return null;
        }
    }

    public List<string> GetWords()
    {
        List<string> result = new List<string>();

        // Build up-down word
        Letter upNeighbour = GetNeighbour(new Vector3(0, 0, 1));
        Letter downNeighbour = GetNeighbour(new Vector3(0, 0, -1));
            
        if (upNeighbour != null && downNeighbour == null)
        {
            // Have up neighbour but no down neighbour, must be at the end of a word
            result.Add(BuildWord(new Vector3(0, 0, 1)));
        } else if (upNeighbour == null && downNeighbour != null)
        {
            // Have down neighbour but no up neighbour, must be at the end of a word
            result.Add(BuildWord(new Vector3(0, 0, -1)));
        }

        // Build right-left word
        Letter rightNeighbour = GetNeighbour(new Vector3(1, 0, 0));
        Letter leftNeighbour = GetNeighbour(new Vector3(-1, 0, 0));

        if (rightNeighbour != null && leftNeighbour == null)
        {
            // Have right neighbour but no left neighbour, must be at the end of a word
            result.Add(BuildWord(new Vector3(1, 0, 0)));
        } else if (rightNeighbour == null && leftNeighbour != null)
        {
            // Have left neighbour but no right neighbour, must be at the end of a word
            result.Add(BuildWord(new Vector3(-1, 0, 0)));
        }

        return result;
    }
}
