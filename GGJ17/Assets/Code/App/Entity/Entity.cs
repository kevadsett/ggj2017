using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
	public int Id { get; private set; }
	public int PosX { get; private set; }
	public int PosZ { get; private set; }


	public Entity(int zId, int zX, int zZ)
	{
		EntityManager.RegisterEntity(this);
	}

	public void MoveLeft(List<Tile.eType> zMoveableTypes)
	{
		MoveToPosition(PosX - 1, PosZ, zMoveableTypes);
	}

	public void MoveRight(List<Tile.eType> zMoveableTypes)
	{
		MoveToPosition(PosX + 1, PosZ, zMoveableTypes);
	}

	public void MoveUp(List<Tile.eType> zMoveableTypes)
	{
		MoveToPosition(PosX, PosZ + 1, zMoveableTypes);
	}

	public void MoveDown(List<Tile.eType> zMoveableTypes)
	{
		MoveToPosition(PosX, PosZ - 1, zMoveableTypes);
	}

	private void MoveToPosition(int zX, int zZ, List<Tile.eType> zMoveableTypes)
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
		if (tile == null || zMoveableTypes.Contains(tile.TileType))
		{
			//not moving because tile is impassable
			return;
		}

		PosX = zX;
		PosZ = zZ;
	}

	private void Destroy()
	{
		EntityManager.DeregisterEntity(this);
	}
}