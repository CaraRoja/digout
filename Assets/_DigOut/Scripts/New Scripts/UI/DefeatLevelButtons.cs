using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatLevelButtons : MonoBehaviour
{
    public DefeatLevel defeat;
    // Start is called before the first frame update
    void Start()
    {
        defeat = GameObject.Find("DefeatLevelHandler").GetComponent<DefeatLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        defeat.RestartLevel();
    }

    public void ExitToMenu()
    {
        defeat.ExitToMenu();
    }

}
