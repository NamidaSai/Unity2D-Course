using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Image[] playerLife = default;

    int currentIndex = 2;

    public void RemoveLife()
    {
        playerLife[currentIndex].enabled = false;
        currentIndex--;
    }

    public void GainLife()
    {
        currentIndex++;
        playerLife[currentIndex].enabled = true;
    }
}
