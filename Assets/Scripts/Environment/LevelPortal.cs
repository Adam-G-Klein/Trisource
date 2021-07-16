using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    public string goTo;
    public bool spawnOnEnemiesDefeated = true;

    private LevelManager _levelManager;
    private ParticleSystem _particleSystem;
    private Collider _collider;
    private bool _enabled;
    private GameObject _light;

    private void Start()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        _particleSystem = GetComponent<ParticleSystem>();
        _collider = GetComponent<CapsuleCollider>();
        _enabled = !spawnOnEnemiesDefeated;
        _light = transform.Find("Point Light").gameObject;
    }

    private void Update()
    {
        if (spawnOnEnemiesDefeated)
        {
            if (GameObject.FindGameObjectWithTag("CrawlerEnemy") == null)
            {
                _enabled = true;
            }
        }

        if (!_enabled)
        {
            _particleSystem.Stop();
            _collider.enabled = false;
            _light.SetActive(false);
        }
        else
        {
            _particleSystem.Play();
            _light.SetActive(true);
            _collider.enabled = true;
        }
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _enabled == true)
        {
            _levelManager.loadLevel(goTo);
        }
    }
}
