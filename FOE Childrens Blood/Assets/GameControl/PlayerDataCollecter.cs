using UnityEngine;
using System.Collections;
using System;
using Fungus;

public class PlayerDataCollecter : MonoBehaviour {
	public static PlayerDataCollecter collecter;

	private TransformSA _playerTransform = new TransformSA();
	private AttributesSA _attrSA = new AttributesSA();
	private FlowchartSA _fCSLSA = new FlowchartSA();
	private SpellListSA _spellListSA = new SpellListSA();

	private Attributes attr;
	private SpellList spellList;
	private Flowchart fCSL;

	public SpellListSA Spells
	{
		get
		{
			UpdateSpellListSA();
			return _spellListSA;
		}
		set
		{
			UpdateSpellList(value);
			_spellListSA = value;
		}
	}
	void UpdateSpellListSA()
	{
		string[] keys = new string[spellList.spellDic.Count];
		spellList.spellDic.Keys.CopyTo(keys,0);
		_spellListSA.spell = keys;
	}
	void UpdateSpellList(SpellListSA spellListSA)
	{
		spellList.spellDic.Clear();
		foreach(string spell in spellListSA.spell)
			spellList.AddSpell(spell);
	}

	public TransformSA PlayerTransform
	{
		get
		{
			UpdateTransformSA();
			return _playerTransform;
		}
		set
		{
			UpdateTransform(value);
			_playerTransform = value;
		}
	}
	public AttributesSA AttrSA
	{
		get
		{
			UpdateAttributesSa();
			return _attrSA;
		}
		set
		{
			UpdateAttributes(value);
			_attrSA = value;
		}
	} 
	public FlowchartSA FCSLSA
	{
		get
		{
			UpdateFlowchartSa();
			return _fCSLSA;
		}
		set
		{
			UpdateFlowchart(value);
			_fCSLSA = value;
		}
	} 
	void UpdateTransformSA()
	{
		_playerTransform.positionX = transform.position.x;
		_playerTransform.positionY = transform.position.y;
		_playerTransform.positionZ = transform.position.z;

		_playerTransform.rotationX = transform.rotation.x;
		_playerTransform.rotationY = transform.rotation.y;
		_playerTransform.rotationZ = transform.rotation.z;
		_playerTransform.rotationW = transform.rotation.w;
	}
	void UpdateTransform(TransformSA transformSA)
	{
		transform.position = new Vector3(transformSA.positionX, transformSA.positionY, transformSA.positionZ);
		transform.rotation = new Quaternion(transformSA.rotationX, transformSA.rotationY, transformSA.rotationZ, transformSA.rotationW);
	}

	void UpdateFlowchartSa()
	{
		_fCSLSA.chapterNumber = fCSL.GetFloatVariable("ChapterNumber");
	}
	void UpdateFlowchart(FlowchartSA flowchartSA)
	{
		fCSL.SetFloatVariable("ChapterNumber",flowchartSA.chapterNumber);
	}

	void Awake()
	{
		if (collecter == null)
		{
			DontDestroyOnLoad(gameObject);
			collecter = this;
		}
		else if (collecter != this) 
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		attr = GetComponent<Attributes>();	
		spellList = GetComponent<SpellList>();
		fCSL = GetComponentInChildren<Flowchart>();
	}


	public void UpdateAttributesSa()
	{
		_attrSA = Attributes.ToSA(attr);
		/*_attrSA.lifePoints = attr.LifePoints;
		_attrSA.maxLifePoints = attr.maxLifePoints;
		_attrSA.mana = attr.Mana;
		_attrSA.maxMana = attr.maxMana;
		_attrSA.strength = attr.strength;
		_attrSA.maxStrength = attr.maxStrength;
		_attrSA.chargeSpeed = attr.chargeSpeed;*/
	}
	public void UpdateAttributes(AttributesSA attrSA)
	{
		attr.lifePoints = attrSA.lifePoints;
		attr.maxLifePoints = attrSA.maxLifePoints;
		attr.mana = attrSA.mana;
		attr.maxMana = attrSA.maxMana;
		attr.strength = attrSA.strength;
		attr.maxStrength = attrSA.maxStrength;
		attr.chargeSpeed = attrSA.chargeSpeed;	
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}

[Serializable]
public class AttributesSA
{
	public float lifePoints = 100;
	public float maxLifePoints = 100;
	public float mana = 30;
	public float maxMana = 30;
	public float strength = 10;
	public float maxStrength = 10;
	public float chargeSpeed = 0;	
}
[Serializable]
public class TransformSA
{
	public float positionX;
	public float positionY;
	public float positionZ;

	public float rotationX;
	public float rotationY;
	public float rotationZ;
	public float rotationW;
}
[Serializable]
public class FlowchartSA
{
	public float chapterNumber;
}
[Serializable]
public class SpellListSA
{
	public string[] spell;
}

