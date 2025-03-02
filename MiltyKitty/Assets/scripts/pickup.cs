using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public GameObject audioObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manager.AddCoins(5);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.PickupCoin, transform.position, 1f);
            Destroy(gameObject);

        }
        
    }
   
}
