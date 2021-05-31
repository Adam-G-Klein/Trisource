using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInterface : MonoBehaviour
{
    private float curlSpeed = 0.002f;
    private float curlAmount = 0.005f;
    private HandCurler _leftHandCurler;
    private HandCurler _rightHandCurler;
    private bool _leftHandCurled = false;
    private bool _rightHandCurled = true;

    void Start()
    {
        _leftHandCurler = GameObject.FindGameObjectWithTag("Player").transform.Find("Graphics/Hands/PlayerLeftHand").GetComponentInChildren<HandCurler>();
        _rightHandCurler = GameObject.FindGameObjectWithTag("Player").transform.Find("Graphics/Hands/PlayerRightHand").GetComponentInChildren<HandCurler>();
    }

    public void curlLeftHand()
    {
        if (_leftHandCurled == false)
        {
            _leftHandCurled = true;
            StartCoroutine(curlLeftHandRoutine());
        }
    }

    private IEnumerator curlLeftHandRoutine()
    {
        int count = -1;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet + curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
    }
    public void uncurlLeftHand()
    {
        if (_leftHandCurled == true)
        {
            _leftHandCurled = false;
            StartCoroutine(uncurlLeftHandRoutine());
        }
    }

    private IEnumerator uncurlLeftHandRoutine()
    {
        int count = -1;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet - curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
    }

    public void curlAndUncurlLeftHand()
    {
        if (_leftHandCurled == false)
        {
            _leftHandCurled = true;
            StartCoroutine(curlAndUncurlLeftHandRoutine());
        }
    }

    private IEnumerator curlAndUncurlLeftHandRoutine()
    {
        int count = -1;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet + curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
        yield return new WaitForSeconds(0.2f);
        count = -1;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet - curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
        _leftHandCurled = false;
    }

    public void uncurlAndCurlLeftHand()
    {
        if (_leftHandCurled == true)
        {
            _leftHandCurled = false;
            StartCoroutine(uncurlAndCurlLeftHandRoutine());
        }
    }

    private IEnumerator uncurlAndCurlLeftHandRoutine()
    {
        int count = -1;
        _leftHandCurler.arcSet = 0.5f;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet - curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
        count = -1;
        while (count < 100)
        {
            count++;
            _leftHandCurler.arcSet = _leftHandCurler.arcSet + curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
        _leftHandCurler.arcSet = 0.5f;
        _leftHandCurled = true;
    }

    public void curlRightHand()
    {
        if (_rightHandCurled == false)
        {
            _rightHandCurled = true;
            StartCoroutine(curlRightHandRoutine());
        }
    }

    private IEnumerator curlRightHandRoutine()
    {
        int count = -1;
        while (count < 100)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet + curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
    }
    public void uncurlRightHand()
    {
        if (_rightHandCurled == true)
        {
            _rightHandCurled = false;
            StartCoroutine(uncurlRightHandRoutine());
        }
    }

    private IEnumerator uncurlRightHandRoutine()
    {
        int count = -1;
        while (count < 100)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet - curlAmount;
            yield return new WaitForSeconds(curlSpeed);
        }
    }

    public void uncurlAndCurlRightHand(float speed)
    {
        if (_rightHandCurled == true)
        {
            _rightHandCurled = false;
            StartCoroutine(uncurlAndCurlRightHandRoutine(speed));
        }
    }

    private IEnumerator uncurlAndCurlRightHandRoutine(float speed)
    {
        int count = -1;
        while (count < 50)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet - 0.01f;
            yield return new WaitForSeconds(speed);
        }
        count = -1;
        while (count < 50)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet + 0.01f;
            yield return new WaitForSeconds(speed);
        }
        _rightHandCurled = true;
    }
}
