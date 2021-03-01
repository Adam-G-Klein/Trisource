using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPrevious : MonoBehaviour
{
    private LevelManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("CrawlerEnemy") == null)
        {
            _manager.previousLevel();
        }
    }
}
