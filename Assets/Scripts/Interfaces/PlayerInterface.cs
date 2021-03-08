using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private LevelManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public void takeDamage(float damage) 
    {
        _playerHealth.takeDamage(damage);
    }

    public void killPlayer()
    {
        _manager.resetGame();
    }
}
