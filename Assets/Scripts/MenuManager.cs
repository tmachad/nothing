using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainCamera;

    public Vector3[] cameraPositions;

    public float cameraMoveTime;

    public AudioSource sfxSource;

    public void GoToPosition(int index)
    {
        if (index < cameraPositions.Length)
        {
            sfxSource.Play();
            StartCoroutine(MoveTo(cameraPositions[index]));
        }
    }

    private IEnumerator MoveTo(Vector3 position)
    {
        Vector3 startPos = mainCamera.transform.position;

        for (float timeLeft = cameraMoveTime; timeLeft > 0; timeLeft -= Time.deltaTime)
        {
            float t = 1 - (timeLeft / cameraMoveTime);
            Vector3 newPos = new Vector3(
                Mathf.SmoothStep(startPos.x, position.x, t),
                Mathf.SmoothStep(startPos.y, position.y, t),
                Mathf.SmoothStep(startPos.z, position.z, t)
            );

            mainCamera.transform.position = newPos;

            yield return 0; // Wait until next frame
        }
    }
}
