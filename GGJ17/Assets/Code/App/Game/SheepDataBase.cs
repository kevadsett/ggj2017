using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDataBase : MonoBehaviour {
	public static SheepDataBase Instance;
	public List<SheepData> Sheep;

	void Awake()
	{
		Instance = this;
	}

	public SheepData GetSheep(int id)
	{
		for (int i = 0; i < Sheep.Count; i++)
		{
			if (Sheep [i].ID == id)
			{
				return Sheep [i];
			}
		}
		return null;
	}
}
