using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundMovement))]
public class CrawlerMovement : MonoBehaviour
{
    private GroundMovement _movement;
    private GameObject _player;
    private Transform _playerTrans;
    private bool _regularMovement = true;
    private Vector3 _movementDirection;

    private float _moveBackTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTrans = _player.transform;
        _movement = GetComponent<GroundMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCrawler();
    }

    private void moveCrawler()
    {
        if (_regularMovement)
        {
            _movementDirection = (_playerTrans.position - transform.position).normalized;
        }
        _movement.moveHorizontal(_movementDirection);
    }

    public void moveBack()
    {
        // Move the crawler away from the player after it does damage
        _movementDirection = -1 * (_playerTrans.position - transform.position).normalized;
        _regularMovement = false;
        StartCoroutine(moveBackTimer());
    }

    IEnumerator moveBackTimer()
    {
        yield return new WaitForSeconds(_moveBackTime);
        _regularMovement = true;
    }
}
