using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlatformController>().laddered = true;
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.FootStepWood, transform.position, 1f);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlatformController>().laddered = false;
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.FootStepWood, transform.position, 1f);
        }
        
    }

}
