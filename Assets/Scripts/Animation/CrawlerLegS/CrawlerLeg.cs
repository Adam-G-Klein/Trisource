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
    public Vector3 movDir; //set by parent object
    public float myRot = 0f;
    void Start()
    {
        myRot = transform.localRotation.eulerAngles.y;
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
        Vector3 up = Vector3.Cross(movDir,distance);
        //Debug.DrawRay(transform.position, distance.normalized, Color.red, 1);
        Debug.DrawRay(transform.position, up.normalized, Color.green, 1);
        Debug.DrawRay(transform.position, movDir.normalized, Color.blue, 1);
        print("up: " + up.ToString());
        int numLegs = legSegments.Count;
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
            //print("i: " + i + " dist: " + distance + " thisAngle: " + thisAngle);
            seg.setRotation(distance, up, thisAngle);
        }
    }

    public void setMovDir(Vector3 movDir){
        //this.movDir = Quaternion.AngleAxis(myRot, transform.up) * movDir;
        this.movDir = movDir;
    }
}
