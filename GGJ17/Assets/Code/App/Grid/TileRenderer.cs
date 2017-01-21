using UnityEngine;
using System.Collections;

public class TileRenderer : MonoBehaviour
{
	public int ID;

	private int grass = 0, sand = 1, water = 2;

	public void Render(Tile zTile)
	{
		//zTile.TileType;
		transform.position = new Vector3(zTile.PosX, 0, zTile.PosZ);

	}
}