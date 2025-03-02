using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Manager.DoorKeyColours keyColour;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch (keyColour)
        {
            case Manager.DoorKeyColours.Red:
                sr.color = Color.red;
                break;
            case Manager.DoorKeyColours.Blue:
                sr.color = Color.blue;
                break;
            case Manager.DoorKeyColours.Yellow:
                sr.color = Color.yellow;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
        if(collision.tag == "Player")
        {
            Manager.KeyPickup(keyColour);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.PickupKey, transform.position, 1f);
            Destroy(gameObject);
        }
    }
}