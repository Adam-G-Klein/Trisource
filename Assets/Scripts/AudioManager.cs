using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip projectileClip;
    public AudioClip projectileHitClip;
    public AudioClip projectileMissClip;
    public AudioClip forcePushClip;
    public AudioClip forcePushHitClip;
    public AudioClip yellowClip;
    public AudioClip yellowOutroClip;
    public AudioClip jumpTakeoffClip;
    public AudioClip jumpLandingClip;
    public AudioClip idleClip;
    public List<AudioClip> playerStepClips;
    public List<AudioClip> crawlerStepClips;
    public List<AudioClip> introCinematicClips;

    private AudioSource _audioSource;
    private AudioSource _oneShotAudioSource;
    private AudioSource _idleSource;
    private bool _playSteps = false;
    private float sfxVolume = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _oneShotAudioSource = transform.Find("OneShot").GetComponent<AudioSource>();
        _idleSource = transform.Find("AmbientLoop").GetComponent<AudioSource>();
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    public void playFireProjectile()
    {
        _oneShotAudioSource.PlayOneShot(projectileClip, sfxVolume);
    }

    public void playProjectileHit()
    {
        _oneShotAudioSource.PlayOneShot(projectileHitClip, sfxVolume);
    }

    public void playProjectileMiss()
    {
        _oneShotAudioSource.PlayOneShot(projectileMissClip, sfxVolume);
    }

    public void playForcePush()
    {
        _oneShotAudioSource.PlayOneShot(forcePushClip, sfxVolume);
    }

    public void playForcePushHit()
    {
        _oneShotAudioSource.PlayOneShot(forcePushHitClip, sfxVolume);
    }

    public void playIntro(int i)
    {
        _oneShotAudioSource.PlayOneShot(introCinematicClips[i], sfxVolume);
    }

    public void playIdle()
    {
        _idleSource.clip = idleClip;
        _idleSource.volume = 0.03f;
        _idleSource.loop = true;
        _idleSource.Play();
    }

    public void playYellow()
    {
        // Loop the yellow sound
        _audioSource.clip = yellowClip;
        _audioSource.volume = 0.05f;
        _audioSource.loop = true;
        _audioSource.Play();
        return;
    }

    public void stopYellow()
    {
        // Stop the looping of the yellow sound
        // play one shot of the yellow outro
        _audioSource.Stop();
        _audioSource.PlayOneShot(yellowOutroClip, 0.3f);
        return;
    }

    public void playJumpTakeoff()
    {
        _oneShotAudioSource.PlayOneShot(jumpTakeoffClip, sfxVolume);
    }

    public void playJumpLanding()
    {
        _oneShotAudioSource.PlayOneShot(jumpLandingClip, sfxVolume);
    }

    public void playCrawlerStep()
    {
        int index = Random.Range(0, crawlerStepClips.Count);
        _oneShotAudioSource.PlayOneShot(crawlerStepClips[index], 0.05f);
    }

    public void playSteps(float moveSpeed)
    {
        // some math going on here
        // Movement speed is going to be somewhere around 4-8
        // For a normal movement speed, the step time interval found to be nice is 0.4
        // Want it to speed up a bit if the movement speed is faster
        // slow down if movement speed is slower
        // SOLVE THE y=mx+b
        // (6, 0.4)
        // (12, 0.27)
        // b = 0.53
        // m = -.13
        float stepTimeInterval = (moveSpeed * -0.022f) + 0.53f;
        if (_playSteps == false)
        {
            _playSteps = true;
            StartCoroutine("steps", stepTimeInterval);
        }
    }

    public void stopSteps()
    {
        _playSteps = false;
        StopCoroutine("steps");
    }

    private IEnumerator steps(float stepTimeInterval)
    {
        Debug.Log("play step");
        yield return new WaitForSeconds(stepTimeInterval/3);
        while (_playSteps)
        {
            int index = Random.Range(0, playerStepClips.Count);
            _oneShotAudioSource.PlayOneShot(playerStepClips[index], 0.05f);
            yield return new WaitForSeconds(stepTimeInterval);
        }
    }
}
