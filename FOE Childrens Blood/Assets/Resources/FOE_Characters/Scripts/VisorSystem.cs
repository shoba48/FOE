using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisorSystem : MonoBehaviour
{
    float smalestAngle = 31;
    public GameObject lastTargetObj = null;



    Collider[] GetColiderInRange(float range, int layer)
    {
        LayerMask l_mask = 1 << layer;
        return Physics.OverlapSphere(transform.position, range, l_mask);
    }



    void FillColliderDicByParents(Dictionary<Transform, List<Collider>> colliderDicByParents, Collider[] objects)
    {// colOfSameParent = [List<Parent1>, List<Parent2>, ... ] with maby some Parent == null
        foreach (Collider c in objects)
		{
            Transform key = c.transform;
			if (key.gameObject.GetComponent<ObjectToUse>() != null && key.gameObject != this.gameObject)
			{

            if (colliderDicByParents.ContainsKey(key))
                colliderDicByParents[key].Add(c);
            else
					colliderDicByParents[key] = new List<Collider>() { c };
			}
        }
    }
		


    void CheckCollidersParentPositios(Dictionary<Transform, List<Collider>> colliderDicByParents)
    {
        GameObject targetObj = null;
        smalestAngle = 31; // somehow an initialisation
        foreach (Transform t_Key in colliderDicByParents.Keys)
        {
            ObjectToUse obj2U = t_Key.gameObject.GetComponent<ObjectToUse>();
            if (obj2U != null)
            {
 
                Vector3 direction = t_Key.position - transform.position + transform.forward;
                if (direction.magnitude < obj2U.useRange)
                {

                    float angle = Vector3.Angle(transform.forward, direction);
                    if (angle < smalestAngle + 10 / (1 + (direction.magnitude)))
                        smalestAngle = angle; targetObj = t_Key.gameObject;
                }
                else if (direction.magnitude < obj2U.autoActionRange)
					obj2U.AutoAction(this.transform);
            }           
        }
        if (smalestAngle == 31)
        {
            // if lastTargetObj != null this means we left the angle-Range to set it as target
            // else: we've never met a target or loose any target
            if (lastTargetObj != null)
            { // Unhighlight the last target obj
                ObjectToUse obj2U = lastTargetObj.GetComponent<ObjectToUse>();
                if (obj2U != null)
                    ShaderChooser.SetShader(lastTargetObj, obj2U.defaultShader);
				lastTargetObj = null;
            }
        }
        else
        {
            // we found something whitch matches all target conditions
            ObjectToUse obj2U = targetObj.GetComponent<ObjectToUse>();
            if (obj2U != null)
                ShaderChooser.SetShader(targetObj, "Highlighted");
            // we can check if player wants to so something with the target object
            if (Input.GetKeyDown(KeyCode.Mouse0))
				obj2U.DoAction(this.transform);
            // save target for next update so we can unhighlightet it when its out of conditions
            lastTargetObj = targetObj;
        }
    }



    // Update is called once per frame
    void Update()
    {

        // get "Interactable" Collider 
        Collider[] objects = GetColiderInRange(10, 9);
            // Sort Collider by parent 
            Dictionary<Transform, List<Collider>> colliderDicByParents = new Dictionary<Transform, List<Collider>>();
            FillColliderDicByParents(colliderDicByParents, objects);
        CheckCollidersParentPositios(colliderDicByParents);    
    }
}
