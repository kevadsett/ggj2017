using UnityEngine;
using System.Collections.Generic;

public class SheepEntity : Entity
{
	private RollingCube viewCube;
	private DogEntity dog;
	private float moveTimer;
	private GameData gameData;

	public SheepEntity (int zId, int zX, int zZ, DogEntity zDog) : base (zId, zX, zZ)
	{
		gameData = GameDataBase.Instance.GetData (0);
		dog = zDog;

		var obj = Object.Instantiate (gameData.SheepPrefab);
		viewCube = obj.GetComponent <RollingCube>();
		viewCube.InitAtPoint (new Vector3 (zX, 0.0f, zZ));

		_moveableTypes = new List<Tile.eType> ()
		{
			Tile.eType.Ground,
			Tile.eType.Mound
		};
	}

	public override void Update (Game.eState zGameState)
	{
		moveTimer += Time.deltaTime;

		if (moveTimer > gameData.SheepMoveDuration && ManhattanDistance (dog) <= 1)
		{
			int dX = PosX - dog.PosX, dZ = PosZ - dog.PosZ;

			TryToMove (PosX + dX, PosZ + dZ);
			moveTimer = 0.0f;
		}

		base.Update (zGameState);
	}

	private void TryToMove (int zX, int zZ)
	{
		bool shallObey = Random.Range (0, 100) < gameData.SheepObediencePercent;

		if (shallObey && MoveToPosition (zX, zZ, _moveableTypes))
		{
			// hurray! we could move the way we wanted to!
		}
		else
		{
			int r = Random.Range (0, 4);

			List<System.Func<bool>> possibleMoves = null;
			if (r == 0) possibleMoves = new List<System.Func<bool>> { MoveUp, MoveDown, MoveLeft, MoveRight };
			if (r == 1) possibleMoves = new List<System.Func<bool>> { MoveDown, MoveUp, MoveRight, MoveLeft };
			if (r == 2) possibleMoves = new List<System.Func<bool>> { MoveLeft, MoveRight, MoveUp, MoveDown };
			if (r == 3) possibleMoves = new List<System.Func<bool>> { MoveRight, MoveLeft, MoveDown, MoveUp };

			for (int i = 0; i < possibleMoves.Count; i++)
			{
				if (possibleMoves [i]() == true) break;
			}
		}

		AudioPlayer.PlaySound ("SheepRoll", new Vector3 (PosX, 0.0f, PosZ));
		viewCube.MoveToPoint (new Vector3 (PosX, 0.0f, PosZ));
	}

	public bool IsSafe()
	{
		var tile = TileManager.GetTileAtPosition(PosX, PosZ);

		return (tile.TileType == Tile.eType.Mound);
	}

	public override void Destroy (bool destroyImmediately = false)
	{
		if (destroyImmediately)
		{
			if (viewCube != null)
			{
				Object.Destroy (viewCube.gameObject);
			}
		}
		else
		{
			viewCube.transform.SetParent(ToiletWave.Instance.transform);
			viewCube.drowned = true;
			ToiletWave.Instance.CarrySheep(viewCube);
		}

		base.Destroy ();
	}
}
