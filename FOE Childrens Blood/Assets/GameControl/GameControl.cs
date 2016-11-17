using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameControl : MonoBehaviour {
	public static GameControl control;

	void Awake()
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this) 
		{
			Destroy(gameObject);
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		GameData data = new GameData();
		// asigne all data to Playerdata...
		data.playerData = GetPlayerData(data);
		bf.Serialize(file, data);
		file.Close();
	}
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();	
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize(file);
			file.Close();
			// deliver all data to the game...
			SetPlayerData(data.playerData);
		}
	}
	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 80, 100, 40), "Load"))
		{
			Load();
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 40), "Save"))
		{
			Save();
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}

	PlayerData GetPlayerData(GameData data)
	{
		data.playerData = new PlayerData();
		data.playerData.transformSA = PlayerDataCollecter.collecter.PlayerTransform;
		data.playerData.attrSa = PlayerDataCollecter.collecter.AttrSA;
		data.playerData.flowchartSA = PlayerDataCollecter.collecter.FCSLSA;
		data.playerData.spellListSA = PlayerDataCollecter.collecter.Spells;
		return data.playerData;
	}
	void SetPlayerData(PlayerData data)
	{
		PlayerDataCollecter.collecter.PlayerTransform = data.transformSA;
		PlayerDataCollecter.collecter.AttrSA = data.attrSa;	
		PlayerDataCollecter.collecter.FCSLSA = data.flowchartSA;
		PlayerDataCollecter.collecter.Spells = data.spellListSA;
	}
}
[Serializable]
public class GameData
{
	public PlayerData playerData;
	public EnviromentData enviromentData;
}

[Serializable]
public class PlayerData
{
	public TransformSA transformSA;
	public AttributesSA attrSa;
	public FlowchartSA flowchartSA;
	public SpellListSA spellListSA;

	void DoSomething(){}

	
}
[Serializable]
public class EnviromentData
{
	public float time = 0.0f;
	public string[] name = null;
	public TransformSA[] transformSA = null;
	public AttributesSA[]attrSA = null;

	public void AddSomeBody(string name, TransformSA transformSA, AttributesSA attrSA)
	{
		if (this.name == null)
		{
			this.name = new string[1]{name};
			this.transformSA = new TransformSA[1]{transformSA};
			this.attrSA = new AttributesSA[1]{attrSA};
		}
		else 
		{
			string[] nAdd = new string[1]{name}; 
			string[] n = new string[this.name.Length + 1];
			this.name.CopyTo(n,0); nAdd.CopyTo(n,this.name.Length);
			this.name = n;
			TransformSA[] tAdd = new TransformSA[1]{transformSA}; 
			TransformSA[] t = new TransformSA[this.transformSA.Length + 1];
			this.transformSA.CopyTo(t,0); tAdd.CopyTo(t,this.transformSA.Length);
			this.transformSA = t;
			AttributesSA[] aAdd = new AttributesSA[1]{attrSA}; 
			AttributesSA[] a = new AttributesSA[this.attrSA.Length + 1];
			this.attrSA.CopyTo(a,0); aAdd.CopyTo(a,this.attrSA.Length);
			this.attrSA = a;
		}
	}
}