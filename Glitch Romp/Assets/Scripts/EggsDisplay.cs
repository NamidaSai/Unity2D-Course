using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggsDisplay : MonoBehaviour
{
    [SerializeField] int eggsCount = 100;
    TextMeshProUGUI eggsText;

    private void Start()
    {
        eggsText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        eggsText.text = eggsCount.ToString();
    }

    public bool HaveEnoughEggs(int amount)
    {
        return eggsCount >= amount;
    }

    public void AddEggs(int amount)
    {
        eggsCount += amount;
        UpdateDisplay();
    }

    public void SpendEggs(int amount)
    {
        if (eggsCount >= amount)
        {
            eggsCount -= amount;
            UpdateDisplay();
        }
    }

    public int GetEggsCount()
    {
        return eggsCount;
    }
}
