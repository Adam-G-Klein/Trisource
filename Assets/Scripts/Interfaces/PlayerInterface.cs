using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private LevelManager _manager;
    private ActivateResource _tethering;
    private PlayerHealthVisuals _healthVisuals;

    private Vector3 _respawnPoint = new Vector3(50.85f, 211.08f, -97.22f);
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        _tethering = GetComponent<ActivateResource>();
        _healthVisuals = GameObject.FindGameObjectWithTag("VisualManager").transform.GetComponentInChildren<PlayerHealthVisuals>();
    }

    public void takeDamage(float damage) 
    {
        _playerHealth.takeDamage(damage);
    }

    public void killPlayer()
    {
        transform.position = _respawnPoint;
        _tethering.deactivateAll();
        _healthVisuals.setHealth(1f);
        _playerHealth.resetHealth();
    }

    public void checkpoint(Vector3 respawnPoint)
    {
        _respawnPoint = respawnPoint;
        Debug.Log("Checkpoint set");
    }
}
