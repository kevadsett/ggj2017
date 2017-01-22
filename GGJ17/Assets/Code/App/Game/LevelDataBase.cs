﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataBase : MonoBehaviour {
	public static LevelDataBase Instance;
	public List<LevelData> Levels;
	private int _levelIndex;

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
		_levelIndex = (_levelIndex + 1) % Levels.Count;
		return Levels[_levelIndex];
	}
}