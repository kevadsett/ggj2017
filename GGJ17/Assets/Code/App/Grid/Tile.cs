using UnityEngine;
using System.Collections;

public class Tile
{
	public enum eType
	{
		Ground,
		House,
		Mound
	}

	public int ID { get; private set; }
	public int PosX { get; private set; }
	public int PosZ { get; private set; }
	public eType TileType;


	public Tile(int zId, int zX, int zZ, eType zType)
	{
		ID = zId;
		PosX = zX;
		PosZ = zZ;
		TileType = zType;
	}
}