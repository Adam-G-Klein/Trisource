using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    public bool cutscenesDisabled = false;
    [SerializeField]
    private List<string> disabledCinematics = new List<string>();
    public PlayableDirector playOnAwakeCinematic;
    // Start is called before the first frame update

    void Start()
    {

        //do everything else before the line below:
        playOnAwakeCinematic.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool cutsceneEnabled(PlayableDirector director){
        if(cutscenesDisabled) return false;
        else return !disabledCinematics.Contains(director.name);
    }

    public void playCutscene(PlayableDirector director){
        if(cutsceneEnabled(director)) director.Play();
    }
}
