using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;

    [SerializeField] GameObject winLabel = default;
    [SerializeField] float winLoadDelay = 4f;
    [SerializeField] GameObject loseLabel = default;
    [SerializeField] AudioClip[] levelEndSFX = default;


    int numberOfAttackers = 0;
    bool levelTimerFinished = false;

    AudioSource myAudioSource;
    LivesDisplay livesDisplay;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        myAudioSource = GetComponent<AudioSource>();
        livesDisplay = FindObjectOfType<LivesDisplay>();
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if(numberOfAttackers <= 0 && levelTimerFinished && livesDisplay.GetLives() > 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        AudioClip clip = levelEndSFX[1];
        myAudioSource.PlayOneShot(clip);
        gameSpeed = 0;
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        AudioClip clip = levelEndSFX[0];
        myAudioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(winLoadDelay);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}
