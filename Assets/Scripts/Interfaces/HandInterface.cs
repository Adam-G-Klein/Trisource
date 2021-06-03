using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInterface : MonoBehaviour
{
    private float leftCurlSpeed = 0.01f;
    private float curlAmount;
    private float curlCount = 20f;
    private HandCurler _leftHandCurler;
    private HandCurler _rightHandCurler;
    private bool _leftHandCurled = false;
    private bool _rightHandCurled = true;

    void Start()
    {
        _leftHandCurler = GameObject.FindGameObjectWithTag("Player").transform.Find("Graphics/Hands/PlayerLeftHand").GetComponentInChildren<HandCurler>();
        _rightHandCurler = GameObject.FindGameObjectWithTag("Player").transform.Find("Graphics/Hands/PlayerRightHand").GetComponentInChildren<HandCurler>();
        curlAmount = 0.5f / curlCount;
    }

    // ==============================================================
    //                      CURLING THE HANDS
    // ==============================================================
    public void curlLeftHand()
    {
        if (_leftHandCurled == false)
        {
            _leftHandCurled = true;
            StartCoroutine(curlHandRoutine(_leftHandCurler));
        }
    }

    public void curlRightHand()
    {
        if (_rightHandCurled == false)
        {
            _rightHandCurled = true;
            StartCoroutine(curlHandRoutine(_rightHandCurler));
        }
    }

    private IEnumerator curlHandRoutine(HandCurler hand)
    {
        int count = -1;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet + curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
    }

    // ==============================================================
    //                      UNCURLING THE HANDS
    // ==============================================================

    public void uncurlLeftHand()
    {
        if (_leftHandCurled == true)
        {
            _leftHandCurled = false;
            StartCoroutine(uncurlHandRoutine(_leftHandCurler));
        }
    }

    public void uncurlRightHand()
    {
        if (_rightHandCurled == true)
        {
            _rightHandCurled = false;
            StartCoroutine(uncurlHandRoutine(_rightHandCurler));
        }
    }

    private IEnumerator uncurlHandRoutine(HandCurler hand)
    {
        int count = -1;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet - curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
    }

    // ==============================================================
    //                      CURL THEN UNCURL THE HANDS
    // ==============================================================

    public void curlAndUncurlLeftHand()
    {
        if (_leftHandCurled == false)
        {
            _leftHandCurled = true;
            StartCoroutine(curlAndUncurlHandRoutine(_leftHandCurler));
        }
    }

    private IEnumerator curlAndUncurlHandRoutine(HandCurler hand)
    {
        int count = -1;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet + curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
        yield return new WaitForSeconds(0.2f);
        count = -1;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet - curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
        _leftHandCurled = false;
    }

    // ==============================================================
    //                      UNCURL THEN CURL THE HANDS
    // ==============================================================

    public void uncurlAndCurlLeftHand()
    {
        if (_leftHandCurled == true)
        {
            _leftHandCurled = false;
            StartCoroutine(uncurlAndCurlHandRoutine(_leftHandCurler));
        }
    }

    private IEnumerator uncurlAndCurlHandRoutine(HandCurler hand)
    {
        int count = -1;
        hand.arcSet = 0.5f;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet - curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
        count = -1;
        while (count < curlCount)
        {
            count++;
            hand.arcSet = hand.arcSet + curlAmount;
            yield return new WaitForSeconds(leftCurlSpeed);
        }
        hand.arcSet = 0.5f;
        _leftHandCurled = true;
    }

    public void uncurlAndCurlRightHandRed()
    {
        if (_rightHandCurled == true)
        {
            _rightHandCurled = false;
            StartCoroutine(uncurlAndCurlRightHandRedRoutine());
        }
    }

    private IEnumerator uncurlAndCurlRightHandRedRoutine()
    {
        int count = -1;
        _rightHandCurler.arcSet = 0.5f;
        while (count < 8)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet - 0.5f/8;
            yield return new WaitForSeconds(0.008f);
        }
        count = -1;
        _rightHandCurler.arcSet = 0f;
        while (count < 8)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet + 0.5f/8;
            yield return new WaitForSeconds(0.008f);
        }
        _rightHandCurler.arcSet = 0.5f;
        _rightHandCurled = true;
    }

    public void uncurlAndCurlRightHandBlue()
    {
        if (_rightHandCurled == true)
        {
            _rightHandCurled = false;
            StartCoroutine(uncurlAndCurlRightHandBlueRoutine());
        }
    }

    private IEnumerator uncurlAndCurlRightHandBlueRoutine()
    {
        int count = -1;
        _rightHandCurler.arcSet = 0.5f;
        while (count < curlCount)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet - curlAmount;
            yield return new WaitForSeconds(0.01f);
        }
        count = -1;
        _rightHandCurler.arcSet = 0f;
        while (count < curlCount)
        {
            count++;
            _rightHandCurler.arcSet = _rightHandCurler.arcSet + curlAmount;
            yield return new WaitForSeconds(0.01f);
        }
        _rightHandCurler.arcSet = 0.5f;
        _rightHandCurled = true;
    }
}
