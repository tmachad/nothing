using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject letterPrefab;

    private Transform levelRoot;

    private void Awake()
    {
        levelRoot = transform;
    }
}
