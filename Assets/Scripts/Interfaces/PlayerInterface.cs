using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void takeDamage(float damage) 
    {
        _playerHealth.takeDamage(damage);
    }
}
