using UnityEngine;
using System.Collections;
using Fungus;

public class CharacterInteraction : ObjectToUse {

	// Use this for initialization
	public override void Start () {
		defaultShader = GetComponentInChildren<Renderer>().material.shader;
		//flowchart = transform.Find("Flowchart").gameObject.GetComponent<Flowchart>();
		flowchart = GetComponentInChildren<Flowchart>();
	}

	public override void DoAction (Transform somebody)
	{
		base.DoAction (somebody);
		flowchart.SendFungusMessage(somebody.gameObject.name + 
			somebody.gameObject.GetComponentInChildren<Flowchart>().GetVariable("ChapterNumber").ToString());

	}
	// Update is called once per frame
	//void Update () {}
}
