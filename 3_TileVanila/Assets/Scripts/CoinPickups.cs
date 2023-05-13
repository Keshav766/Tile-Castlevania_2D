using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickups : MonoBehaviour
{

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    [SerializeField] float audioVolume = 1f;
    PlayerMovement plrMovScrRef;
    void Start()
    {
        plrMovScrRef = FindObjectOfType<PlayerMovement>();
    }

    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, audioVolume);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
