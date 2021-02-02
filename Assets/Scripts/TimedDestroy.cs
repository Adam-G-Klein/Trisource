using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float timeToLive = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeToLiveTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timeToLiveTimer()
    {
        yield return new WaitForSeconds(timeToLive);
        Object.Destroy(this.gameObject);
    }
}
