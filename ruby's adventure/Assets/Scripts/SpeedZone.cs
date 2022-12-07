using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController >();

        if (controller != null)
        {
            //controller.ChangeHealth(-1);
            //controller.canGoFast=true;
            
            
            controller.GoingFast();
            //controller.canGoFast=false;
            
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController >();
        controller.GottaGoFast();
        if (controller != null)
        {
            //controller.ChangeHealth(-1);
            //controller.canGoFast=true;
            
            
            controller.GottaGoFast();
            //controller.canGoFast=false;
            
        }
        
    }
}
