using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class SkillCanvas : MonoBehaviour {
	Canvas skillCanvas;

	public GameObject player = null;
	Attributes attr;
	SpellList spells;

	Dictionary<string,Dictionary<string, Text>> dic = new Dictionary<string, Dictionary<string, Text>>();
	// Use this for initialization
	void Start () 
	{
		skillCanvas = GetComponent<Canvas>();
		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
		attr = player.GetComponent<Attributes>();
		spells = player.GetComponent<SpellList>();


		Text[] txt = GetComponentsInChildren<Text>();

		foreach (Text t in txt)
		{
			string gridName = t.transform.parent.parent.name;
			string slotName = t.transform.parent.name;
			if (!dic.ContainsKey(gridName))
				dic.Add(gridName, new Dictionary<string, Text>(){{slotName, t}});
			else dic[gridName].Add(slotName, t);
		}
	}

	void CheckAttributes(Dictionary<string, Text> attrDic) 
	{
		attrDic["Life"].text = "Life" + "\n"
			+ attr.LifePoints.ToString() + " / " +	attr.maxLifePoints + "\n"
			+ "Exp : " + "0"; // later
		attrDic["Mana"].text = "Mana" + "\n"
			+ attr.Mana.ToString() + " / " +	attr.maxMana + "\n"
			+ "Exp : " + "0"; // later
		attrDic["Strenght"].text = "Strenght" + "\n"
			+ attr.strenght.ToString() + " / " +	attr.maxStrenght + "\n"
			+ "Exp : " + "0"; // later
		attrDic["Mana Charge Speed"].text = "Mana Charge Speed" + "\n"
			+ attr.chargeSpeed;
	}
	void CheckSpells(Dictionary<string, Text> spellDic)
	{	int i = 1;
		foreach(string spellName in spells.spellDic.Keys)
		{
			spellDic["Spell " + i.ToString()].text = spellName + "\n\n" 
				+ "Exp : 0";// later
			i++;
		}
		
	}
	void CheckInventory(){}//later
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			skillCanvas.enabled = !skillCanvas.enabled;
			CheckAttributes(dic["AttributeGrid"]);
			CheckSpells(dic["SpellGrid"]);
		
		}
			
	}
}
