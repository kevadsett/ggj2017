using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEntity : Entity {
	private GameObject _gameObject;
	private DogView _view;
	private GameData _gameData;
	private float _moveTimer;

	public DogEntity (int zId, int zX, int zZ) : base (zId, zX, zZ)
	{
		_gameData = GameDataBase.Instance.GetData ();
		var prefab = _gameData.DogPrefab;

		_gameObject = GameObject.Instantiate (prefab);

		_view = _gameObject.GetComponent<DogView> ();
		_view.InitAtPoint (new Vector3 (zX, 0.0f, zZ));

		_moveableTypes = new List<Tile.eType> ()
		{
			Tile.eType.Ground
		};
	}

	public override void Update (Game.eState zGameState)
	{
		_moveTimer += Time.deltaTime;

		if (_moveTimer > _gameData.DogMoveDuration * _gameData.FreeMoveThreshold)
		{
			if (TryToMove (PosX, PosZ))
			{
				_moveTimer = 0.0f;
			}
		}
	}

	private bool TryToMove (float x, float z)
	{
		bool shouldMove = false;
		if (Input.GetKeyDown (KeyCode.W))
		{
			MoveUp ();
			shouldMove = true;
		}
		else if (Input.GetKeyDown (KeyCode.S))
		{
			MoveDown ();
			shouldMove = true;
		}
		else if (Input.GetKeyDown (KeyCode.A))
		{
			MoveLeft ();
			shouldMove = true;
		}
		else if (Input.GetKeyDown (KeyCode.D))
		{
			MoveRight ();
			shouldMove = true;
		}

		if (shouldMove)
		{
			_view.MoveToPoint (new Vector3(PosX, 0, PosZ));
		}
		return shouldMove;
	}

	public override void Destroy (bool destroyImmediately)
	{
		Object.Destroy (_view.gameObject);
		base.Destroy ();
	}

}
