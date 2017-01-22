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
		PosX = zX;
		PosZ = zZ;
		EntityManager.RegisterEntity(this);
	}

	public virtual void Update (Game.eState zGameState)
	{

	}

	public bool MoveLeft()
	{
		return MoveToPosition(PosX - 1, PosZ, _moveableTypes);
	}

	public bool MoveRight()
	{
		return MoveToPosition(PosX + 1, PosZ, _moveableTypes);
	}

	public bool MoveUp()
	{
		return MoveToPosition(PosX, PosZ + 1, _moveableTypes);
	}

	public bool MoveDown()
	{
		return MoveToPosition(PosX, PosZ - 1, _moveableTypes);
	}

	public int ManhattanDistance(Entity other)
	{
		return Mathf.Abs(PosX - other.PosX) + Mathf.Abs(PosZ - other.PosZ);
	}
		
	protected virtual bool CanMoveToPosition(int zX, int zZ, List<Tile.eType> zMoveableTypes)
	{
		if (zX < 0 || zZ < 0 || zX >= TileManager.WIDTH || zZ >= TileManager.HEIGHT)
		{
			//not moving because space is off the board
			return false;
		}

		if (EntityManager.GetEntityAtPosition(zX, zZ) != null)
		{
			//not moving because space is empty
			return false;
		}

		var tile = TileManager.GetTileAtPosition(zX, zZ);
		if (tile == null || !zMoveableTypes.Contains(tile.TileType))
		{
			//not moving because tile is impassable
			return false;
		}

		return true;
	}

	protected virtual bool MoveToPosition(int zX, int zZ, List<Tile.eType> zMoveableTypes)
	{
		if (CanMoveToPosition(zX, zZ, zMoveableTypes))
		{
			PosX = zX;
			PosZ = zZ;

			return true;
		}

		return false;
	}

	public virtual void Destroy(bool destroyImmediately = false)
	{
		EntityManager.DeregisterEntity(this);
	}
}