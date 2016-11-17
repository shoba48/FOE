using UnityEngine;
using System.Collections;
using Fungus;

public class Attributes : MonoBehaviour
{
    Flowchart flowchartOpenWorld = null;

	public float lifePoints = 100;
	public float maxLifePoints = 100;

	public float mana = 100;
	public float maxMana = 100;

	public float chargeSpeed = 1;

	public float strength = 20;
	public float maxStrength = 20;

    public bool alive = true;

	public float LifePoints {
		get {
			return lifePoints;	
		}
		set {
			if (value >= 0) {
				if (value <= maxLifePoints)
					lifePoints = value;
				else
					lifePoints = maxLifePoints;
			} else
				lifePoints = 0;
		}
	}

	public float Mana {
		get {
			return mana;
		}
		set {
			if (value >= 0) {
				if (value <= maxMana)
					mana = value;
				else
					mana = maxMana;
			} else
				mana = 0;
		}
	}
    void Start()
    {
        if (this.gameObject.tag == "Player")
        {
            Transform flowchartOW = transform.Find("FlowchartOpenWorld");
            if (flowchartOW != null)
                flowchartOpenWorld = flowchartOW.gameObject.GetComponent<Flowchart>();
        }
    }


	void Update ()
	{
		//AddMana(-1);
		if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
			AddMana(Mathf.RoundToInt(chargeSpeed));
		else
			AddMana(-1);

        if (lifePoints == 0 && flowchartOpenWorld != null && alive)
        {
            alive = false;
            flowchartOpenWorld.FindBlock("Game Over").StartExecution();
        }


    }

	public void AddLifePoints(float deltaLP)
	{
		LifePoints += deltaLP;
	}
	public void AddMana(float deltaM)
	{
		Mana += deltaM;
	}





	// stor this out to Character
	public float GetDamage ()
	{
		return strength;
	}

	public void SetAttribute(string attribute, float value)
	{
		if (attribute == "LifePoints") LifePoints = value;
		else if (attribute == "MaxLifePoints") maxLifePoints = value;
		else if (attribute == "Mana") Mana = value;
		else if (attribute == "MaxMana") maxMana = value;
		else if (attribute == "Strenght") strength = value;
		else if (attribute == "MaxStrenght") maxStrength = value;
		else if (attribute == "ChargeSpeed") chargeSpeed = value;
	}




	public static AttributesSA ToSA(Attributes attr)
	{
		AttributesSA attrSA = new AttributesSA();
		attrSA.lifePoints = attr.LifePoints;
		attrSA.maxLifePoints = attr.maxLifePoints;
		attrSA.mana = attr.Mana;
		attrSA.maxMana = attr.maxMana;
		attrSA.strength = attr.strength;
		attrSA.maxStrength = attr.maxStrength;
		attrSA.chargeSpeed = attr.chargeSpeed;
		return attrSA;
	}
}
