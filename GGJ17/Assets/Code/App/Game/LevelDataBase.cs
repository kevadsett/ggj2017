using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataBase : MonoBehaviour {
	public static LevelDataBase Instance;
	public List<LevelData> Levels;

	void Awake()
	{
		Instance = this;
	}

	public LevelData GetLevel(int index)
	{
		return Levels[index];
	}
}
