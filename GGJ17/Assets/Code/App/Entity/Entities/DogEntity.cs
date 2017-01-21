using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEntity : Entity {
	private GameObject _gameObject;

	public DogEntity (int zId, int zX, int zZ) : base (zId, zX, zZ)
	{
		var gameData = GameDataBase.Instance.GetData ();
		var prefab = gameData.DogPrefab;

		_gameObject = GameObject.Instantiate (prefab);
		_moveableTypes = new List<Tile.eType> ()
		{
			Tile.eType.Grass,
			Tile.eType.Sand
		};
	}

	public override void Update (Game.eState zGameState)
	{
		_gameObject.transform.position = new Vector3 (PosX, 0, PosZ);
		if (Input.GetKeyDown (KeyCode.W))
		{
			MoveUp ();
		}
		if (Input.GetKeyDown (KeyCode.S))
		{
			MoveDown ();
		}
		if (Input.GetKeyDown (KeyCode.A))
		{
			MoveLeft ();
		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			MoveRight ();
		}
	}

}
