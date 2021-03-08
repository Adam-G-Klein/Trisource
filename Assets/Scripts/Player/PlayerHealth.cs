using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float healthRegen = 25f;
    private float regenWaitTime = 3f;

    private float _health;
    private Rigidbody _body;
    private EntityConst _entityConst;
    private PlayerHealthVisuals _visuals;
    private bool _canRegenHealth = false;
    
    private PlayerInterface _interface;

    // Start is called before the first frame update
    void Start()
    {
        _entityConst = GetComponent<EntityConst>();
        _health = _entityConst.maxHealth;
        _body = GetComponent<Rigidbody>();
        _visuals = GameObject.FindGameObjectWithTag("VisualManager").GetComponentInChildren<PlayerHealthVisuals>();
        _interface = GetComponent<PlayerInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    public void takeDamage(float damage)
    {
        _health -= damage;
        _canRegenHealth = false;
        _visuals.setHealth(_health / _entityConst.maxHealth);
        StopCoroutine("regenTimer");
        StartCoroutine("regenTimer");
        if (_health <= 0f)
        {
            _interface.killPlayer();
        }
    }

    IEnumerator regenTimer()
    {
        yield return new WaitForSeconds(regenWaitTime);
        Debug.Log("Enabling Regeneration");
        _canRegenHealth = true;
        StartCoroutine(regenerateHealth());
    }
    IEnumerator regenerateHealth()
    {
        while (_health < 100f && _canRegenHealth == true)
        {
            _health += 1;
            _visuals.setHealth(_health / _entityConst.maxHealth);
            yield return new WaitForSeconds(1 / healthRegen);
        }
    }
}
