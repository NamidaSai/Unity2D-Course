using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField] GameObject healthPack = default;
    [SerializeField] float dropSpeed = 3f;
    [SerializeField] AudioClip bossDiesSFX = default;
    [Range(0f,1f)] [SerializeField] float bossDiesSFXVolume = 0.25f;

    public void BossDies()
    {
        GameObject healthDrop = Instantiate
            (healthPack,
             transform.position,
             Quaternion.identity) as GameObject;
        healthDrop.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-dropSpeed);
        AudioSource.PlayClipAtPoint(bossDiesSFX, Camera.main.transform.position, bossDiesSFXVolume);
    }
}
