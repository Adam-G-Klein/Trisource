 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 
 [ExecuteInEditMode]
 public class ReassignArmature : MonoBehaviour
 {
     public Transform newArmature;
     public string rootBoneName = "Root";
     public bool PressToReassign;
 
     void Update()
     {
         if (PressToReassign)
             Reassign();
         PressToReassign = false;
     }
 
     // [ContextMenu("Reassign Bones")]
     public void Reassign()
     {
         if (newArmature == null) {
             Debug.Log("No new armature assigned");
             return;
         }
 
         if (newArmature.Find(rootBoneName) == null) {
             Debug.Log("Root bone not found");
             return;
         }
 
         Debug.Log("Reassingning bones");
         SkinnedMeshRenderer rend = gameObject.GetComponent<SkinnedMeshRenderer>();
         Transform[] oldbones = rend.bones;
 
        print("got existing bones: ");
        foreach(Transform bone in oldbones){
        print("\t" + bone.gameObject.name);
        }
        rend.rootBone = newArmature.Find(rootBoneName);

        Transform[] children = newArmature.gameObject.GetComponentsInChildren<Transform>();
        print("children of new armature: " );

        List<Transform> newBones = new List<Transform>();

        foreach(Transform child in children){
            if(child.tag == "Bone"){
                print("\t" + child.gameObject.name + " added to bones");
                newBones.Add(child);
            }
        }

        print("setting oldBones to newBones: ");

        for (int i = 0; i < oldbones.Length; i++){
            for (int a = 0; a < newBones.Count; a++){
                print("\tchecked old" + oldbones[i].name + " against new " + newBones[i].name);
                if (oldbones[i].name == newBones[a].name) {
                    print("\tassigning new bone " + newBones[a].gameObject.name);
                    oldbones[i] = newBones[a];
                    break;
                }
            }
        }

        rend.bones = oldbones;
     }


 
 }