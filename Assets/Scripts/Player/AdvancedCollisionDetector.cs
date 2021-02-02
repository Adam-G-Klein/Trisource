using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCollisionDetector : MonoBehaviour
{
    private List<string> _namesList = new List<string> {"left", "center", "right"};
    private List<Transform> _forwardPointsList = new List<Transform>();
    private List<RaycastHit> _forwardHitList = new List<RaycastHit>();
    private List<bool> _forwardCheckList = new List<bool>();
    private List<Transform> _leftPointsList = new List<Transform>();
    private List<RaycastHit> _leftHitList = new List<RaycastHit>();
    private List<bool> _leftCheckList = new List<bool>();
    private List<Transform> _rightPointsList = new List<Transform>();
    private List<RaycastHit> _rightHitList = new List<RaycastHit>();
    private List<bool> _rightCheckList = new List<bool>();
    private List<Transform> _backPointsList = new List<Transform>();
    private List<RaycastHit> _backHitList = new List<RaycastHit>();
    private List<bool> _backCheckList = new List<bool>();


    private float _castDistance = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        getForwardPoints();
        getLeftPoints();
        getRightPoints();
        getBackPoints();
    }

    public Vector3 detectCollision(Vector3 moveDir)
    {
        RaycastHit hit;
        Vector3 inputs = transform.InverseTransformDirection(moveDir);
        Vector3 newMoveDir = moveDir;
        if (inputs.z > 0)
        {
            for (int i = 0; i < _forwardPointsList.Count; i++)
            {
                _forwardCheckList[i] = Physics.Raycast(_forwardPointsList[i].position, transform.forward, out hit, _castDistance);
                _forwardHitList[i] = hit;
                if (_forwardCheckList[i])
                {
                    newMoveDir = newMoveDir - Vector3.Project(newMoveDir, _forwardHitList[i].normal);
                    break;
                }
            }
        }
        if (inputs.z < 0)
        {
            for (int i = 0; i < _forwardPointsList.Count; i++)
            {
                _backCheckList[i] = Physics.Raycast(_backPointsList[i].position, -transform.forward, out hit, _castDistance);
                _backHitList[i] = hit;
                if (_backCheckList[i])
                {
                    newMoveDir = newMoveDir - Vector3.Project(newMoveDir, _backHitList[i].normal);
                    break;
                }
            }
        }
        if (inputs.x < 0)
        {
            for (int i = 0; i < _namesList.Count; i++)
            {
                _leftCheckList[i] = Physics.Raycast(_leftPointsList[i].position, -transform.right, out hit, _castDistance);
                _leftHitList[i] = hit;
                if (_leftCheckList[i])
                {
                    newMoveDir = newMoveDir - Vector3.Project(newMoveDir, _leftHitList[i].normal);
                    break;
                }
            }
        }
        if (inputs.x > 0)
        {
            for (int i = 0; i < _namesList.Count; i++)
            {
                _rightCheckList[i] = Physics.Raycast(_rightPointsList[i].position, transform.right, out hit, _castDistance);
                _rightHitList[i] = hit;
                if (_rightCheckList[i])
                {
                    newMoveDir = newMoveDir - Vector3.Project(newMoveDir, _rightHitList[i].normal);
                    break;
                }
            }
        }
        return newMoveDir;
    }

    void getForwardPoints()
    {
        for (int i = 0; i < _namesList.Count; i++)
        {
            _forwardPointsList.Add(transform.Find("Forward/f_" + _namesList[i]));
            _forwardCheckList.Add(false);
            _forwardHitList.Add(new RaycastHit());
        }
    }

    void getLeftPoints()
    {
        for (int i = 0; i < _namesList.Count; i++)
        {
            _leftPointsList.Add(transform.Find("Left/l_" + _namesList[i]));
            _leftCheckList.Add(false);
            _leftHitList.Add(new RaycastHit());
        }
    }

    void getRightPoints()
    {
        for (int i = 0; i < _namesList.Count; i++)
        {
            _rightPointsList.Add(transform.Find("Right/r_" + _namesList[i]));
            _rightCheckList.Add(false);
            _rightHitList.Add(new RaycastHit());
        }
    }

    void getBackPoints()
    {
        for (int i = 0; i < _namesList.Count; i++)
        {
            _backPointsList.Add(transform.Find("Back/b_" + _namesList[i]));
            _backCheckList.Add(false);
            _backHitList.Add(new RaycastHit());
        }
    }
}
