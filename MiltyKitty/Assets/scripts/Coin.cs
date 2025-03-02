using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinvalue = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manager.AddCoins(coinvalue);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.PickupCoin, transform.position, 1f);
            Destroy(gameObject);
        }

    }
 
}
