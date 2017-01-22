using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDataBase : MonoBehaviour {
	public static SheepDataBase Instance;
	public List<SheepData> Sheep;
	private List<SheepData> _unusedSheep;

	void Awake()
	{
		Instance = this;
		ResetUnusedSheep ();
	}

	void ResetUnusedSheep()
	{
		if (_unusedSheep == null)
		{
			_unusedSheep = new List<SheepData> ();
		}
		for (int i = 0; i < Sheep.Count; i++)
		{
			_unusedSheep.Add (Sheep [i]);
		}
	}

	public SheepData GetSheep()
	{
		if (_unusedSheep.Count == 0)
		{
			ResetUnusedSheep ();
		}
		int index = Random.Range (0, _unusedSheep.Count);
		SheepData data = _unusedSheep [index];
		_unusedSheep.RemoveAt (index);
		return data;
	}
}
