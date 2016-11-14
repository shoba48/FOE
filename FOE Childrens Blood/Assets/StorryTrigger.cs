using UnityEngine;
using System.Collections;
using Fungus;
public class StorryTrigger : MonoBehaviour {
	Flowchart flowchart;
	// Use this for initialization
	void Start () 
	{
		flowchart = GetComponent<Flowchart>();
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log(col);
		flowchart.SendFungusMessage(col.gameObject.name + flowchart.GetVariable("ChapterNumber").ToString());
	}


	// Update is called once per frame
	void Update () {
	
	}
}
