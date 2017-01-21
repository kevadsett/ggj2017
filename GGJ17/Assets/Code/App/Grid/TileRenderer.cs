using UnityEngine;
using System.Collections;

public class TileRenderer : MonoBehaviour
{
	public int ID;

	public void Render(Tile zTile)
	{
		transform.position = new Vector3(zTile.PosX, 0, zTile.PosZ);
	}
}