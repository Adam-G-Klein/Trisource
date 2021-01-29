using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    private CrawlerLegs legs;
    private CrawlerMovement movement;
    void Start()
    {
        legs = transform.GetComponentInChildren<CrawlerLegs>();
        movement = GetComponent<CrawlerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        setMovDir(movement.getMovDir());
        
    }

    public void setMovDir(Vector3 newDir){
        Debug.DrawRay(transform.position, newDir, Color.blue, 1);
        legs.setMovDir(newDir);
    }
}
