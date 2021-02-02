using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCollisionDetector : MonoBehaviour
{
    //private List<string> _forwardNamesList = 
    private List<Transform> _forwardPointsList = new List<Transform>();
    private List<RaycastHit> _forwardHitList = new List<RaycastHit>();
    private List<bool> _forwardCheckList = new List<bool>();
    /*private List<Transform> leftPointsList = new List<Transform>();
    private List<Transform> rightPointsList = new List<Transform>();
    private List<Transform> backPointsList = new List<Transform>();*/

    private float _castDistance = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        getForwardPoints();
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
            }
            for (int i = 0; i < _forwardPointsList.Count; i++)
            {
                if (_forwardCheckList[i])
                {
                    newMoveDir = newMoveDir - Vector3.Project(newMoveDir, _forwardHitList[i].normal);
                    break;
                }
            }
        }
        return newMoveDir;
    }

    void getForwardPoints()
    {
        _forwardPointsList.Add(transform.Find("Forward/f_center"));
        _forwardPointsList.Add(transform.Find("Forward/f_right"));
        _forwardPointsList.Add(transform.Find("Forward/f_left"));
        _forwardCheckList.Add(false);
        _forwardCheckList.Add(false);
        _forwardCheckList.Add(false);
        _forwardHitList.Add(new RaycastHit());
        _forwardHitList.Add(new RaycastHit());
        _forwardHitList.Add(new RaycastHit());
    }
}
