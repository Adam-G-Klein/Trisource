using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    private CrawlerMovement movement;
    void Start()
    {
        movement = GetComponent<CrawlerMovement>();
        
    }

}
