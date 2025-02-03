using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelButtons : MonoBehaviour
{
    public EndLevel completeLevel;
    //private bool levelIsComplete = false;

     void Start()
    {
        completeLevel = GameObject.Find("EndLevelHandler").GetComponent<EndLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitToMenu()
    {
        completeLevel.ExitToMenu();
    }

    public void Continue()
    {
        completeLevel.Continue();
    }
}
