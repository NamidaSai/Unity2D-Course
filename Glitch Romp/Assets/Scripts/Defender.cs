using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int eggsCost = 100;

    public void AddEggs(int amount)
    {
        FindObjectOfType<EggsDisplay>().AddEggs(amount);
    }

    public int GetEggsCost()
    {
        return eggsCost;
    }

}
