﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Attacker attacker = other.GetComponent<Attacker>();

        if(attacker)
        {
            //add animation
        }
    }
}
