using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private LevelManager _manager;
    private ActivateResource _tethering;
    private PlayerHealthVisuals _healthVisuals;
    private Vignette _vignette;
    private PostProcessVolume volume;

    private Vector3 _respawnPoint = new Vector3(50.85f, 211.08f, -97.22f);
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        _tethering = GetComponent<ActivateResource>();
        _healthVisuals = GameObject.FindGameObjectWithTag("VisualManager").transform.GetComponentInChildren<PlayerHealthVisuals>();
        volume = GameObject.FindGameObjectWithTag("VisualManager").transform.Find("DeathEffects").GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out _vignette);
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            killPlayer();
        }
    }

    public void takeDamage(float damage) 
    {
        _playerHealth.takeDamage(damage);
    }

    public void killPlayer()
    {
        _tethering.deactivateAll();
        _healthVisuals.setHealth(1f);
        _playerHealth.resetHealth();
        StartCoroutine(playerDeathAnimation());
    }

    public void checkpoint(Vector3 respawnPoint)
    {
        _respawnPoint = respawnPoint;
        Debug.Log("Checkpoint set");
    }

    private IEnumerator playerDeathAnimation()
    {
        int count = 0;
        _vignette.intensity.value = 0f;
        volume.priority = 2;
        while (count < 50)
        {
            count++;
            _vignette.intensity.value = _vignette.intensity.value + 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = _respawnPoint;
        count = 0;
        while (count < 50)
        {
            count++;
            _vignette.intensity.value = _vignette.intensity.value - 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        volume.priority = 0;
    }
}
