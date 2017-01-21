using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
	public int Id { get; private set; }
	public int PosX { get; private set; }
	public int PosZ { get; private set; }

	protected List<Tile.eType> _moveableTypes;

	public Entity(int zId, int zX, int zZ)
	{
		EntityManager.RegisterEntity(this);
	}

	public virtual void Update (Game.eState zGameState)
	{

	}

	public void MoveLeft()
	{
		MoveToPosition(PosX - 1, PosZ, _moveableTypes);
		Debug.Log (PosX + ", " + PosZ);
	}

	public void MoveRight()
	{
		MoveToPosition(PosX + 1, PosZ, _moveableTypes);
		Debug.Log (PosX + ", " + PosZ);
	}

	public void MoveUp()
	{
		MoveToPosition(PosX, PosZ + 1, _moveableTypes);
		Debug.Log (PosX + ", " + PosZ);
	}

	public void MoveDown()
	{
		MoveToPosition(PosX, PosZ - 1, _moveableTypes);
		Debug.Log (PosX + ", " + PosZ);
	}
		
	protected virtual void MoveToPosition(int zX, int zZ, List<Tile.eType> zMoveableTypes)
	{
		if (zX < 0 || zZ < 0 || zX >= TileManager.WIDTH || zZ >= TileManager.HEIGHT)
		{
			//not moving because space is off the board
			return;
		}

		if (EntityManager.GetEntityAtPosition(zX, zZ) != null)
		{
			//not moving because space is empty
			return;
		}

		var tile = TileManager.GetTileAtPosition(zX, zZ);
		if (tile == null || !zMoveableTypes.Contains(tile.TileType))
		{
			//not moving because tile is impassable
			return;
		}

		PosX = zX;
		PosZ = zZ;
	}

	protected virtual void Destroy()
	{
		EntityManager.DeregisterEntity(this);
	}
}