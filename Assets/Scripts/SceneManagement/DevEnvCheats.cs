using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevEnvCheats : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Transform[] cheatlocs;
    void Start()
    {
        cheatlocs = GetComponentsInChildren<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){

        }
        
    }
}
