using UnityEngine;
using System.Collections;

public class Tile
{
	public int ID { get; private set; }
	public int PosX { get; private set; }
	public int PosZ { get; private set; }


	public Tile(int zId, int zX, int zZ)
	{
		ID = zId;
		PosX = zX;
		PosZ = zZ;
	}
}