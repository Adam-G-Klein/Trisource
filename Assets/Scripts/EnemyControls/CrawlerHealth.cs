using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerHealth : MonoBehaviour
{
    private float _health;
    private GameObject _player;
    private PlayerInterface _playerInterface;
    private CrawlerMovement _crawlerMovement;
    private EntityConst _entityConst;

    private bool _canDamage = true;
    private float _damageInvulnerableTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInterface = _player.GetComponent<PlayerInterface>();
        _crawlerMovement = GetComponent<CrawlerMovement>();
        _entityConst = GetComponent<EntityConst>();
        _health = _entityConst.maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && _canDamage)
        {
            _playerInterface.takeDamage(_entityConst.damage);
            _crawlerMovement.moveBack();
            _canDamage = false;
            StartCoroutine(damageTimer());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Crawler Health: " + _health);
        return;
    }

    public void takeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
        {
            Object.Destroy(this.gameObject);
        }
    }

    IEnumerator damageTimer()
    {
        float elapsedTime = 0;

        while (elapsedTime < _damageInvulnerableTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canDamage = true;
    }
}
