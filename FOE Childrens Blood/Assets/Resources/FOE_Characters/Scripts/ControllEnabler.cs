using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class ControllEnabler : MonoBehaviour 
{
	ThirdPersonUserControl tPUC;
	ThirdPersonCharacter tPC;
	Rigidbody rb;



	// Use this for initialization
	void Start () 
	{
		tPUC = GetComponent<ThirdPersonUserControl>();
		tPC = GetComponent<ThirdPersonCharacter>();
		rb = GetComponent<Rigidbody>();
	}



	public void EnableControl(bool enabled)
	{
		tPUC.enabled = enabled;
		if (!enabled)
		{
			tPC.FreezeMove();
			rb.constraints = RigidbodyConstraints.FreezeAll; 
		}
		else 
			rb.constraints = RigidbodyConstraints.FreezeRotation;
	}
}
