using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellList : MonoBehaviour
{
	public Dictionary<string,GameObject> spellDic = new Dictionary<string, GameObject>();
    public GameObject currentSpell;
    private KeyCode fireKey = KeyCode.Alpha1;



    public void AddSpell(string SpellGameObjectName)
    {
		if (!spellDic.ContainsKey(SpellGameObjectName))
		{
			GameObject spell2Add = Resources.Load("FOE_Characters/Spells/Prefabs/" + SpellGameObjectName) as GameObject;
			if (spell2Add == null)
				Debug.LogWarning(SpellGameObjectName + " not found.");
			else spellDic.Add(SpellGameObjectName, spell2Add);
		}
		else Debug.LogWarning(SpellGameObjectName + " allready in spellList.");

    }



    public void RemoveSpell(string SpellGameObjectName)
    {
		if (spellDic.ContainsKey(SpellGameObjectName))
			spellDic.Remove(SpellGameObjectName);
		else Debug.LogWarning("Cant remove " + SpellGameObjectName);
    }



    public void Fire()
    {
        InstantiateSpell(Mathf.RoundToInt(Random.value * (spellDic.Count - 1)));
    }



    public void Fire(int spellIndex)
    {
		InstantiateSpell(spellIndex % spellDic.Count);
    }



    public void FireOnTarget(int spellIndex, GameObject target)
    {		
        InstantiateSpell(spellIndex, target);
    }

    public void FireOnTarget(string SpellName, GameObject target)
    { 
		InstantiateSpell(SpellName, target);
    }


	private void InstantiateSpell(string SpellName, GameObject target)
	{
		currentSpell = Instantiate(spellDic[SpellName]);
		currentSpell.transform.parent = transform;

		//currentSpell.transform.position = transform.position + transform.forward + transform.up; // change to a spell dependent default position
		currentSpell.transform.rotation = transform.rotation; // change to a spell dependent default rotation

		Spell spell = currentSpell.GetComponent<Spell>();

		spell.creator = this.gameObject;
		spell.victim = target;
		spell.fireKey = this.fireKey;
	}


    private void InstantiateSpell(int spellIndex, GameObject target)
    {
		List<string> keys = new List<string>(spellDic.Keys);
		currentSpell = Instantiate(spellDic[keys[spellIndex]]);
        currentSpell.transform.parent = transform;

        //currentSpell.transform.position = transform.position + transform.forward + transform.up; // change to a spell dependent default position
        currentSpell.transform.rotation = transform.rotation; // change to a spell dependent default rotation

        Spell spell = currentSpell.GetComponent<Spell>();

        spell.creator = this.gameObject;
        spell.victim = target;
        spell.fireKey = this.fireKey;
    }



    private void InstantiateSpell(int spellIndex)
    {
		List<string> keys = new List<string>(spellDic.Keys);
		currentSpell = Instantiate(spellDic[keys[spellIndex]]);
        currentSpell.transform.parent = transform;

		currentSpell.transform.localPosition = Vector3.forward; // change to a spell dependent default position
        currentSpell.transform.rotation = transform.rotation; // change to a spell dependent default rotation

        Spell spell = currentSpell.GetComponent<Spell>();
 
        spell.creator = this.gameObject;
		VisorSystem visor = GetComponentInChildren<VisorSystem>();
        if (visor != null)
			spell.victim = visor.lastTargetObj;
        spell.fireKey = this.fireKey;

    }



    void Start()
    {
		//AddSpell("BodyExchangeSpellGameObject");
		//AddSpell("Fireball");
    }



    void Update()
    {
		if (spellDic.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && this.gameObject.tag == "Player")
            {
                fireKey = KeyCode.Alpha1;
                Fire(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && this.gameObject.tag == "Player")
            {
                fireKey = KeyCode.Alpha2;
                Fire(1);
            }
        }
    }
}
