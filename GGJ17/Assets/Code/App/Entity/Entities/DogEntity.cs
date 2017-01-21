using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEntity : Entity {
	private GameObject _gameObject;
	private DogView _view;

	public DogEntity (int zId, int zX, int zZ) : base (zId, zX, zZ)
	{
		var gameData = GameDataBase.Instance.GetData ();
		var prefab = gameData.DogPrefab;

		_gameObject = GameObject.Instantiate (prefab);

		_view = _gameObject.GetComponent<DogView> ();
		_view.InitAtPoint (new Vector3 (zX, 0.0f, zZ));

		_moveableTypes = new List<Tile.eType> ()
		{
			Tile.eType.Grass,
			Tile.eType.Sand
		};
	}

	public override void Update (Game.eState zGameState)
	{
		bool shouldAnimate = false;
		if (Input.GetKeyDown (KeyCode.W))
		{
			MoveUp ();
			shouldAnimate = true;
		}
		if (Input.GetKeyDown (KeyCode.S))
		{
			MoveDown ();
			shouldAnimate = true;
		}
		if (Input.GetKeyDown (KeyCode.A))
		{
			MoveLeft ();
			shouldAnimate = true;
		}
		if (Input.GetKeyDown (KeyCode.D))
		{
			MoveRight ();
			shouldAnimate = true;
		}
		if (shouldAnimate)
		{
			_view.MoveToPoint (new Vector3(PosX, 0, PosZ));
		}
	}

}
