using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector director;
    private CinematicManager manager;
    public bool triggered = false;

    void Start(){
        manager = GameObject.FindGameObjectWithTag("CinematicManager").GetComponent<CinematicManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !triggered)
        {
            manager.playCutscene(director);
            triggered = true;
        }
    }
}
