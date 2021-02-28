using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLegAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform raycastTarget;
    public Transform ikTarget;
    public float allowableDistance;
    // register hits on everything but the crawler layer
    private int layerMask;
    public float maxRaycastDistance = 3f;
    void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Crawlers"));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastHit = getRaycastHit();
        if(Vector3.Magnitude(ikTarget.position - raycastHit) > allowableDistance)
            ikTarget.position = raycastHit;
    }

    private Vector3 getRaycastHit(){
        RaycastHit hit;
        Vector3 raycastDir = raycastTarget.position - transform.position;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, 
                raycastDir, 
                out hit, 
                maxRaycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, raycastDir * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxRaycastDistance, Color.white);
            Debug.Log("Did not Hit");
        }
        return hit.point;

    }
}
