using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string goal;
    public string nextSceneName;

    public Player player;

    public Image fadePanel;
    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;

    public AudioSource sfxSource;
    public AudioClip fadeOutClip;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadePanel.CrossFadeAlpha(0, fadeInTime, false);
        AudioManager.Instance.FadeMusic(true, fadeInTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void CheckWinCondition()
    {
        Physics.SyncTransforms();
        Letter[] letters = FindObjectsOfType<Letter>();

        List<string> words = new List<string>();

        foreach(Letter l in letters)
        {
            words.AddRange(l.GetWords());
        }

        string goalLower = goal.ToLower();
        foreach(string w in words)
        {
            if (goalLower == w.ToLower())
            {
                StartCoroutine(TransitionToScene(nextSceneName));
            }
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(TransitionToScene(SceneManager.GetActiveScene().name));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        if (player != null)
        {
            player.inputEnabled = false;    // Stop player from doing anything that might cause issues while fading out
        }
        fadePanel.CrossFadeAlpha(1, fadeOutTime, false);

        sfxSource.clip = fadeOutClip;
        sfxSource.Play();


        StartCoroutine(AudioManager.Instance.FadeMusic(false, fadeOutTime));

        yield return new WaitForSeconds(fadeOutTime);

        AudioManager.Instance.SaveTrackPosition();

        if (sceneName == "Quit")
        {
            Application.Quit();
        } else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
