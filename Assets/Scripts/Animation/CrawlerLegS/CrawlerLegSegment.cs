using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLegSegment : MonoBehaviour
{
    public Vector3 targetVector;
    public float fromTargetRotation;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.GetComponentInChildren<CrawlerLegTarget>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //setRotation(targetVector.normalized, Vector3.up, fromTargetRotation);
    }

    public void setRotation(Vector3 targetVector, Vector3 upVector, float fromTargetRotation){
        //targetVector is the direction from the origin of the leg to the leg target
        //upVector is the other vector used to define the plane the segment rotates within
        //fromTargetRotation is the amount of clockwise rotation the segment should have from
        // the target vector
        Debug.DrawRay(transform.position, transform.up / 2, Color.green, 1);
        Debug.DrawRay(transform.position, transform.forward / 2, Color.red, 1);
        transform.up = upVector;
        transform.LookAt(target,upVector);
        transform.localRotation = Quaternion.Euler(-fromTargetRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

}
