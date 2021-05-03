using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector director;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            director.enabled = true;
        }
    }
}
