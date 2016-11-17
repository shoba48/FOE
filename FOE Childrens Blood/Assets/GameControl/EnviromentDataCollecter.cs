using UnityEngine;
using System.Collections;

public class EnviromentDataCollecter : MonoBehaviour 
{
	public static EnviromentDataCollecter collecter;

	private EnviromentData _enviromentData;
	public EnviromentData EnviroData
	{
		get
		{
			return _enviromentData;
		}
		set
		{
			_enviromentData = value;
		}
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
}
