using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [Tooltip("Level Timer in SECONDS")]
    [SerializeField] int levelTime = 10;

    bool triggeredLevelFinished = false;

    void Update()
    {
        if (triggeredLevelFinished) { return; }
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        if (timerFinished)
        {
            triggeredLevelFinished = true;
            FindObjectOfType<LevelController>().LevelTimerFinished();
        }
    }
}
