using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundMovement))]
public class CrawlerMovement : MonoBehaviour
{
    private GroundMovement movement;
    private GameObject player;
    private Transform playerTrans;
    public bool stayStill = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
        movement = GetComponent<GroundMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stayStill)
            movement.moveHorizontal((playerTrans.position - transform.position).normalized);
    }

    public Vector3 getMovDir(){
        return (playerTrans.position - transform.position).normalized;
    }
}
