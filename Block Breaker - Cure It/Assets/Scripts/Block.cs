using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip[] breakSounds = default;
    [SerializeField] GameObject blockSparklesVFX = default;
    [SerializeField] Sprite[] hitSprites = default;

    // cached references
    Level level;
    GameSession gameSession;

    // state variables
    [SerializeField] int timesHit = 0; // serialized for debugging purposes only

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite missing from hitSprites array in " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroyedSFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroyedSFX()
    {
        AudioClip clip = breakSounds[UnityEngine.Random.Range(0,breakSounds.Length)];
        gameSession.AddToScore();
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
