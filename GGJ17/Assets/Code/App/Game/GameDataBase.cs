using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBase : MonoBehaviour {
	public static GameDataBase Instance;
	public List<GameData> GameDataList;

	void Awake () {
		Instance = this;
	}

	public GameData GetData(int dataID = 0)
	{
		return GameDataList [dataID];
	}
}
