using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCurler : MonoBehaviour
{

    private ChainArcControls[] arcControls;
    [Range(0f, 0.5f)]
    public float arcSet = 0f;
    // Start is called before the first frame update
    void Start()
    {
        arcControls = GetComponents<ChainArcControls>();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(ChainArcControls arcControls in arcControls){
            arcControls.arcSet(arcSet);
        }
    }
}
