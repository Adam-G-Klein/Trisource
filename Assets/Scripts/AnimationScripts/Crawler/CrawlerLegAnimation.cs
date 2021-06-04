using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLegAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform raycastTarget;
    public Transform ikTarget;
    public Transform raycastOrigin;
    public float allowableDistance;
    // register hits on everything but the crawler layer
    private int layerMask;
    public float maxRaycastDistance = 3f;
    private Vector3 defaultLegPos;
    private bool didhit;
    void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Crawlers"));

        defaultLegPos = (raycastTarget.position - raycastOrigin.position) * 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastHit = getRaycastHit();
        if(Vector3.Magnitude(ikTarget.position - raycastHit) > allowableDistance){
            if(didhit){
                ikTarget.position = raycastHit;
            } else {
                ikTarget.position = defaultLegPos;
            }
        }

    }

    private Vector3 getRaycastHit(){
        RaycastHit hit;
        Vector3 raycastDir = raycastTarget.position - raycastOrigin.position;
        // Does the ray intersect any objects excluding the player layer
        didhit = Physics.Raycast(raycastOrigin.position, 
                raycastDir, 
                out hit, 
                maxRaycastDistance, layerMask);
        if (didhit)
        {
            Debug.DrawRay(raycastOrigin.position, raycastDir * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(raycastOrigin.position, transform.TransformDirection(Vector3.forward) * maxRaycastDistance, Color.white);
            //Debug.Log("Did not Hit");
        }
        return hit.point;

    }
}
