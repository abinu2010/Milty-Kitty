using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int initialHitPoints = 2;
    private int hitpointsLeft;
    private SpriteRenderer sr;
    private Color initialspriteColour;
    public Color deathColour = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initialspriteColour = sr.color;
        hitpointsLeft = initialHitPoints;
    }

    public void RecieveHit(int damage)

    {
        hitpointsLeft = hitpointsLeft - damage;
        ChangeColour();
        if (hitpointsLeft <= 0) 
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Squish, transform.position, 1f);
            Destroy(gameObject);

        }
    }
    void ChangeColour()
    {
        float percentageOfDamageTaken = 1f - ((float)hitpointsLeft / (float)initialHitPoints);
        Color newHealthColour = Color.Lerp(initialspriteColour, deathColour, percentageOfDamageTaken);
        sr.color = newHealthColour;
    }
}
