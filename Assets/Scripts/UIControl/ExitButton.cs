using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void onclick(){
        if(GetComponent<Clickable>().clickable)
            Application.Quit();
    }
}
