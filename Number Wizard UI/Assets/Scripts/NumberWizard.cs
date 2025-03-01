﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{

    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] TextMeshProUGUI guessText;

    int guess;

    // Start is called before the first frame update
    void Start() 
    {
        StartGame();
    }

    void StartGame()
    {
        NextGuess();
    }

    public void OnPressHigher()
    {
        if (guess == max)
        {
            min = guess;
        }
        else
        {
            min = guess + 1;
        }
        NextGuess();
    }

    public void OnPressLower()
    {
        if (guess == min)
        {
            max = guess;
        }
        else
        {
            max = guess - 1;
        }
        NextGuess();
    }

    void NextGuess()
    {
        guess = Random.Range(min,max+1);
        guessText.text = guess.ToString();
    }

}
