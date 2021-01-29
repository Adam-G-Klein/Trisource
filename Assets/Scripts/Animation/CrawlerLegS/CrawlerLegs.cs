using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerLegs : MonoBehaviour
{
    // Start is called before the first frame update
    private List<CrawlerLeg> legs = new List<CrawlerLeg>();
    void Start()
    {
        foreach(CrawlerLeg leg in transform.GetComponentsInChildren<CrawlerLeg>()){
            legs.Add(leg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMovDir(Vector3 newDir) {
        foreach(CrawlerLeg leg in legs){
            leg.setMovDir(newDir);
        }
    }
}
