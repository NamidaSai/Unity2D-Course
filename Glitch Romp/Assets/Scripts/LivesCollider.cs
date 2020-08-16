using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCollider : MonoBehaviour
{
    [SerializeField] AudioClip lifeLostSFX = default;
    [Range(0f,1f)] [SerializeField] float lifeLostSFXVolume = 0.25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<LivesDisplay>().ReduceLives();
        AudioSource.PlayClipAtPoint(lifeLostSFX, Camera.main.transform.position, lifeLostSFXVolume);
        Destroy(other.gameObject);
    }
}
