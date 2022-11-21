using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogBox2;
    float timerDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        dialogBox2.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
    {
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
            dialogBox.SetActive(false);
        }
    }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        dialogBox2.SetActive(true);
    }
    public void DisplayDialog2()
    {
        
        dialogBox2.SetActive(false);
    }
}
