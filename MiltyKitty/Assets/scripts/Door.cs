using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
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
            Debug.Log("Open");
        if(collision.tag == "Player" && gameObject != null )
        {
            switch(keyColour)
            {
                case Manager.DoorKeyColours.Red:
                    if (Manager.redKey)
                    {
                        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position, 1f);
                        Destroy(gameObject);
                    }
                    break;
                case Manager.DoorKeyColours.Blue:
                    if (Manager.blueKey) 
                    {
                        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position, 1f);
                        Destroy(gameObject);
                    }
                    break;
                case Manager.DoorKeyColours.Yellow:
                    if (Manager.yellowKey) 
                    {
                        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position, 1f);
                        Destroy(gameObject); 
                    }
                    break;
            }
        }
    }
}
