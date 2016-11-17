using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeWeapon : MonoBehaviour
{
	Attributes thisAttr = null;
	Attributes otherAttr = null;
	public string AnimationState = "";
	// "UnarmedFight.Kick L1"
	// "UnarmedFight.Kick R1"
	// "UnarmedFight.Attack L3"
	// "UnarmedFight.Attack R3"
	Dictionary<string,float[]> damageGapDic = new Dictionary<string, float[]>();
	Animator anim = null;
	// Use this for initialization
	void Start ()
	{
		thisAttr = GetComponentInParent<Attributes> ();
		anim = GetComponentInParent<Animator> ();
		InitDamageGapDic();
	
	}
	void InitDamageGapDic()
	{
		damageGapDic.Add("UnarmedFight.Kick L1", new float[2]{ 0.43f, 0.55f });	
		damageGapDic.Add("UnarmedFight.Kick R1", new float[2]{ 0.43f, 0.55f });
		damageGapDic.Add("UnarmedFight.Attack L3", new float[2]{ 0.43f, 0.55f });
		damageGapDic.Add("UnarmedFight.Attack R3", new float[2]{ 0.38f, 0.50f });
	}

	void OnTriggerEnter (Collider other)
	{
		otherAttr = other.gameObject.GetComponent<Attributes> ();
		if (otherAttr != null) 
		{
			AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo (0);
			if 
				(
					animInfo.IsName (AnimationState)
					&& animInfo.normalizedTime % 1 > damageGapDic[AnimationState][0]
					&& animInfo.normalizedTime % 1 < damageGapDic[AnimationState][1]
				)
			{
				Debug.Log (animInfo.normalizedTime % 1);
				otherAttr.AddLifePoints (-thisAttr.strength);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
