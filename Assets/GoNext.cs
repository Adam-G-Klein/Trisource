using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNext : MonoBehaviour
{
    private GameObject _player;
    private LevelManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = _player.transform.position - transform.position;
        if (distance.magnitude < 2 && Input.GetKey(KeyCode.E))
        {
            _manager.nextLevel();
        }
    }
}
