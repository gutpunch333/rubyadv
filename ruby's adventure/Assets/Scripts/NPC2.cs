using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    public float displayTime2 = 4.0f;
    public GameObject dialogBox3;
    public GameObject dialogBox4;
    float timerDisplay2;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox3.SetActive(false);
        dialogBox4.SetActive(true);
        //timerDisplay = -1.0f;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (timerDisplay >= 0)
    {
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
            dialogBox3.SetActive(false);
        }
    }
    }

    public void DisplayDialog3()
    {
        timerDisplay = displayTime;
        dialogBox3.SetActive(true);
        dialogBox4.SetActive(true);
    }*/
    public void DisplayDialog4()
    {
        
        dialogBox4.SetActive(false);
    }
}
