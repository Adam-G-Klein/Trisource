using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInterface>().killPlayer();
        }
        else if (other.gameObject.tag == "CrawlerEnemy")
        {
            other.gameObject.GetComponent<CrawlerInterface>().kill();
        }
    }
}
