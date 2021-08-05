using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToMenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");

        
    }
}
