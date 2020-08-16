using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] float baseLives = 3f;
    [SerializeField] int damage = 1;
    float lives;
    TextMeshProUGUI livesText;

    private void Start()
    {
        lives = baseLives - PlayerPrefsController.GetDifficultyLevel();
        livesText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }

    public void ReduceLives()
    {
        lives -= damage;
        UpdateDisplay();
        if(lives <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }

    private void UpdateDisplay()
    {
        livesText.text = lives.ToString();
    }

    public float GetLives()
    {
        return lives;
    }
}
