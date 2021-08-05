using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainArcControls : MonoBehaviour
{
    // IMPORTANT NOTE:

    // We Assume this script is on "handGraphic", 
    // the parent of Armature and the skinned mesh object
    // and that the parent holding all the poles is a direct child
    // of it

    // We also assume that the Poles are named <fingerName>+"Pole"
    // We also assume that the Targets are named <fingerName>+"Target"
    private Dictionary<string, Transform> fingerTargets = new Dictionary<string, Transform>();
    private Dictionary<string, Vector3> fingerTargInitPos = new Dictionary<string, Vector3>();
    private Dictionary<string, Transform> fingerPoles = new Dictionary<string, Transform>();
    private Dictionary<string, Vector3> fingerPoleInitPos = new Dictionary<string, Vector3>();
    public Vector3 poleRatios = new Vector3();
    public Vector3 targRatios = new Vector3();

    public float testArcSet = 0;
    
    public List<string> targetNames = new List<string>{
        "pointer",
        "middle",
        "ring",
        "pinky"
    };

    // Start is called before the first frame update
    void Start()
    {
        populateTargets(fingerTargets);
        if(fingerTargets.Count == 0) print("HAND IK targets NOT FOUND, check they're placed correctly in the hierarchy");
        populatePoles(fingerPoles);
        if(fingerPoles.Count == 0) print("HAND IK poles NOT FOUND, check they're placed correctly in the hierarchy");
        
    }

    void Update(){
    }

    public void arcSet(float set){
        foreach(string name in targetNames){
            arcSetFinger(set, name);
        }
    }


    private void populateTargets(Dictionary<string, Transform> targs){
        // Assume this script is on "handGraphic", 
        // the parent of Armature and the skinned mesh object
        Transform targParent = transform.parent.parent.Find("IKTargets");
        int i = 0;
        while(i < targParent.childCount){
            Transform child = targParent.GetChild(i);
            // the case where this is the thumb in its own coordinate space
            if(child.childCount > 0){
                child = child.GetChild(0);
            }
            targs.Add(child.name, child);
            fingerTargInitPos.Add(child.name, child.localPosition);
            i++;
        }
    }

    private void populatePoles(Dictionary<string, Transform> poles){
        Transform polesParent = transform.Find("IKPoles");
        int i = 0;
        while(i < polesParent.childCount){
            Transform child = polesParent.GetChild(i);
            if(child.childCount > 0){
                child = child.GetChild(0);
            }
            poles.Add(child.name, child);
            fingerPoleInitPos.Add(child.name, child.localPosition);
            i++;
        }

    }
    public void arcSetFinger(float angle, string fingerName){
        Transform pole = fingerPoles[fingerName+"Pole"];
        Transform targ = fingerTargets[fingerName+"Target"];
        Vector3 poleInitPos = fingerPoleInitPos[fingerName+"Pole"];
        Vector3 targInitPos = fingerTargInitPos[fingerName+"Target"];
        pole.localPosition = new Vector3(poleInitPos.x + Mathf.Cos(angle + 3 * Mathf.PI/2) * poleRatios.x, 
            poleInitPos.y + Mathf.Sin(angle) * poleRatios.y, 
            poleInitPos.z + Mathf.Cos(angle + 3 * Mathf.PI/2) * poleRatios.z 
            );

        targ.localPosition = new Vector3(targInitPos.x + Mathf.Cos(angle + 3 * Mathf.PI/2) * targRatios.x, 
            targInitPos.y + Mathf.Sin(angle) * targRatios.y, 
            targInitPos.z + Mathf.Cos(angle + 3 * Mathf.PI/2) * targRatios.z 
            );
        /*
        pole.localPosition = posAdd(pole.localPosition, 
            Mathf.Sin(angle) * poleYRatio, 
            Mathf.Sin(angle) * poleZRatio);
        targ.localPosition = posAdd(targ.localPosition, 
            Mathf.Sin(angle), 
            Mathf.Sin(angle));
            */
    }
    private Vector3 posAdd(Vector3 currPos, float yAdd = 0, float zAdd = 0, float xAdd = 0){
        return new Vector3(
            currPos.x + xAdd,
            currPos.y + yAdd,
            currPos.z + zAdd
             );

    }
}
