using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool cursorLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        if (cursorLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
