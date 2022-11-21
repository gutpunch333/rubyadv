using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    
    void OnTriggerEnter2D(Collider2D other)
{
    RubyController controller = other.GetComponent<RubyController>();

    if (controller != null)
    {
        if(controller.health < controller.maxHealth && gameObject.tag!="groundCog")
          {
	       controller.ChangeHealth(1);
	       Destroy(gameObject);
           controller.PlaySound(collectedClip);
          }
          else if (gameObject.tag=="groundCog")
          {
            controller.ChangeAmmo(1);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
          }
    }
}
}
