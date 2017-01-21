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

	public void MoveLeft()
	{
		MoveToPosition(PosX - 1, PosZ);
	}

	public void MoveRight()
	{
		MoveToPosition(PosX + 1, PosZ);
	}

	public void MoveUp()
	{
		MoveToPosition(PosX, PosZ + 1);
	}

	public void MoveDown()
	{
		MoveToPosition(PosX, PosZ - 1);
	}

	private void MoveToPosition(int zX, int zZ)
	{
		if (zX < 0 || zZ < 0 || zX >= TileManager.WIDTH || zZ >= TileManager.HEIGHT)
			return;

		PosX = zX;
		PosZ = zZ;
	}

	private void Destroy()
	{
		EntityManager.DeregisterEntity(this);
	}
}