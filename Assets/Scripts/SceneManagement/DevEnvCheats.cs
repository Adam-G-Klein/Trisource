using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevEnvCheats : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject teleportLocationsParent;
    public List<Transform> tplocs = new List<Transform>();
    public float postTeleportFallHeight = 999f;
    private PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        for(int i = 0; i < teleportLocationsParent.transform.childCount; i++){
            tplocs.Add(teleportLocationsParent.transform.GetChild(i));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        pollTeleportLocs();
        
    }

    private void pollTeleportLocs(){
        for(int i=0;i < tplocs.Count;i++)
        {
           if(Input.GetKeyDown((KeyCode)(48+i))) //alpha0 keycode has value 48
           {
               StopCoroutine("teleportToLoc");
               StartCoroutine("teleportToLoc", i);
           }
        }
    }

    private IEnumerator teleportToLoc(int i){
        float prevFallHeight = playerMovement.survivableFallHeight;
        playerMovement.survivableFallHeight = postTeleportFallHeight;
        player.transform.position = tplocs[i].position;
        yield return new WaitForSeconds(1);
        playerMovement.survivableFallHeight = prevFallHeight;
        yield return null;
    }
}
