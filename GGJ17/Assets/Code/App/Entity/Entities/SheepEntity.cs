using UnityEngine;
using System.Collections.Generic;

public class SheepEntity : Entity
{
	private RollingCube viewCube;
	private DogEntity dog;
	private float moveTimer;

	public SheepEntity (int zId, int zX, int zZ, DogEntity zDog) : base (zId, zX, zZ)
	{
		var gameData = GameDataBase.Instance.GetData (0);
		var prefab = gameData.SheepPrefab;

		var obj = Object.Instantiate (prefab);

		viewCube = obj.GetComponent <RollingCube>();
		viewCube.InitAtPoint (new Vector3 (zX, 0.0f, zZ));

		dog = zDog;

		_moveableTypes = new List<Tile.eType> ()
		{
			Tile.eType.Grass
		};
	}

	public override void Update (Game.eState zGameState)
	{
		moveTimer += Time.deltaTime;

		if (moveTimer > 1.0f)
		{
			MoveLeft ();
			viewCube.MoveToPoint (new Vector3 (PosX, 0.0f, PosZ));
			moveTimer -= 1.0f;
		}

		base.Update (zGameState);
	}

	protected override void Destroy ()
	{
		Object.Destroy (viewCube);

		base.Destroy ();
	}
}