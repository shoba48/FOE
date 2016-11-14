using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {
	public GameObject player = null;
	Attributes attr;
	Dictionary<string,Slider> bars = new Dictionary<string, Slider>();
	// Use this for initialization
	void Start () 
	{
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
		attr = player.GetComponent<Attributes>();

		Slider[] barsArray = GetComponentsInChildren<Slider>();
		foreach (Slider s in barsArray)
			bars.Add(s.transform.name, s);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		bars["Healthbar"].maxValue = attr.maxLifePoints;
		bars["Healthbar"].value = attr.LifePoints;
		bars["Manabar"].maxValue = attr.maxMana;
		bars["Manabar"].value = attr.Mana;
		bars["Strenghtbar"].maxValue = attr.maxStrenght;
		bars["Strenghtbar"].value = attr.strenght;
	}
}
