using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
        }
    }
}
