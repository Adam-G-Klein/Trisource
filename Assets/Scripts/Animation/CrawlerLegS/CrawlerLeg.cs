using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLeg : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform legOrigin;
    private Transform legTarget;
    private List<CrawlerLegSegment> legSegments = new List<CrawlerLegSegment>();
    public float underLeg1 = 40f;
    public float segmentLength = 1f;
    void Start()
    {
        legOrigin = transform.GetComponentInChildren<CrawlerLegOrigin>().transform;
        legTarget = transform.GetComponentInChildren<CrawlerLegTarget>().transform;
        foreach(CrawlerLegSegment seg in transform.GetComponentsInChildren<CrawlerLegSegment>()){
            legSegments.Add(seg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        setLegs();
    }

    void setLegs(){
        Vector3 targPos = legTarget.position;
        Vector3 originPos = legOrigin.position;
        Vector3 distance = targPos - originPos;
        int numLegs = legSegments.Count;
        //print("numLegs: " + numLegs + " distanceDiff: " + distance);
        float lastAngle = 0;
        float thisAngle;
        for(int i = 0; i < legSegments.Count; i+=1){
            CrawlerLegSegment seg = legSegments[i];
            seg.transform.position = originPos + ((distance / (numLegs + 1)) * (i + 1));
            //print("distanceDiff / (numlegs+1): " + distance / (numLegs + 1) + " i + 1: " + (i+1) + " distanceDiff/(numLegs+1) * (i+1): " + (distance / (numLegs + 1)) * (i + 1));
            if(i == 0){
                thisAngle = Mathf.Rad2Deg * Mathf.Acos((distance.magnitude/2) / segmentLength);
            } else {
                thisAngle = Mathf.Rad2Deg * -Mathf.Acos((distance.magnitude/2) / segmentLength);
            }
            print("i: " + i + " dist: " + distance + " thisAngle: " + thisAngle);
            seg.setRotation(distance, Vector3.up, thisAngle);
        }
    }
}
