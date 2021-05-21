using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherVisuals : MonoBehaviour
{
    public Gradient blueGradient;
    public Gradient redGradient;
    public Gradient yellowGradient;
    public Gradient checkpointGradient;

    private LineRenderer _tetherLine;
    private Vector3 _toPoint;
    private GameObject _leftHand;
    private bool _drawingTether = false;
    private bool _drawingAnimation = false;
    private float _tetherSpeed = 25f;
    private float _tetherDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _tetherLine = GetComponent<LineRenderer>();
        _leftHand = GameObject.FindGameObjectWithTag("Player");
        _leftHand = _leftHand.transform.Find("Graphics/Hands/Left Hand").gameObject;
        _tetherLine.positionCount = 1;
        return;
    }

    private void Update()
    {
        Vector3 pointAlongLine;
        float totalDistance;
        _tetherLine.SetPosition(0, _toPoint);

        if (_drawingAnimation)
        {
            _tetherDistance = _tetherDistance + (_tetherSpeed * Time.deltaTime);
            totalDistance = (_toPoint - _leftHand.transform.position).magnitude;
            if (_tetherDistance >= totalDistance)
            {
                _drawingAnimation = false;
                _drawingTether = true;
            }
            else
            {
                pointAlongLine = _tetherDistance * Vector3.Normalize(_leftHand.transform.position - _toPoint) + _toPoint;
                _tetherLine.SetPosition(1, pointAlongLine);
            }
        }
        else if (_drawingTether)
        {
            _tetherLine.SetPosition(1, _leftHand.transform.position);
        }
        return;
    }

    public void tether(Vector3 toPoint)
    {
        _toPoint = toPoint;
        _drawingAnimation = true;
        _tetherDistance = 0f;
        _tetherLine.positionCount = 2;
        _tetherLine.SetPosition(0, _toPoint);
        _tetherLine.SetPosition(1, _toPoint);
    }

    public void checkpointTether(Vector3 toPoint)
    {
        _toPoint = toPoint;
        _drawingAnimation = true;
        _tetherDistance = 0f;
        _tetherLine.positionCount = 2;
        _tetherLine.SetPosition(0, _toPoint);
        _tetherLine.SetPosition(1, _toPoint);
        StartCoroutine(checkpointTetherDone());
    }

    public void untether()
    {
        _tetherLine.positionCount = 1;
        _drawingTether = false;
    }

    public void setBlue()
    {
        _tetherLine.colorGradient = blueGradient;
    }

    public void setYellow()
    {
        _tetherLine.colorGradient = yellowGradient;
    }

    public void setRed()
    {
        _tetherLine.colorGradient = redGradient;
    }

    public void setCheckpoint()
    {
        _tetherLine.colorGradient = checkpointGradient;
    }

    private IEnumerator checkpointTetherDone()
    {
        while (_drawingAnimation)
        {
            yield return new WaitForFixedUpdate();
        }
        untether();
    }
}
