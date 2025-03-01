﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{

    int max;
    int min;
    int guess;

    // Start is called before the first frame update

    void Start() 
    {
        StartGame();
    }

    void StartGame()
    {

        max = 1000;
        min = 1;
        guess = 500;

        Debug.Log("Welcome to the super duper Number Wizard");
        Debug.Log("In this game, you pick a number and the Wizard uses the 'magic' of maths to figure out what it is.");
        Debug.Log("First things first, pick a number but don't tell me what it is.");
        Debug.Log("The highest number you can pick is " + max);
        Debug.Log("The lowest number you can pick is " + min);
        Debug.Log("Tell me if your number is higher or lower than " + guess);
        Debug.Log("Push Up = Higher, Push Down = Lower, Push Enter = Correct");
        max += 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            min = guess;
            NextGuess();

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            max = guess;
            NextGuess();
        }

        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            Debug.Log("Yay, I've figured it out!");
            StartGame();
        }
    }

    void NextGuess()
    {
        guess = (max + min) / 2;
        Debug.Log("Is it higher or lower than... " + guess);
    }

}
