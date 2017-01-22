using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataBase : MonoBehaviour {
	public static LevelDataBase Instance;
	public List<LevelData> Levels;
	public int _levelIndex;

	void Awake()
	{
		Instance = this;
	}

	public LevelData GetLevel(int index)
	{
		_levelIndex = index;
		return Levels[index];
	}

	public LevelData GetNextLevel()
	{
		_levelIndex++;
		if (_levelIndex >= Levels.Count)
		{
			_levelIndex = 0;
			Game.MultiplyInterval ();
		}
		return Levels[_levelIndex];
	}

	public void Reset()
	{
		_levelIndex = 0;
	}
}
