using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEntity : Entity {
	private GameObject _gameObject;
	public DogEntity (int zId, int zX, int zZ) : base (zId, zX, zZ)
	{
		var gameData = GameDataBase.Instance.GetData ();
		var prefab = gameData.SheepPrefab;
	}

	public void Update()
	{
		
	}

}
