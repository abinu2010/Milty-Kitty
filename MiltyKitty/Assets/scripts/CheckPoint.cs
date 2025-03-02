using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject Flag;
    // Start is called before the first frame update
    void Start()
    {
        //Flag = transform.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            sp.color = Color.red;
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Flag, transform.position, 1f);

            Manager.UpdateCheckPoints(gameObject);
        }
    }
    public void LowerFlag()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
        
    
}
