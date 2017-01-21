using UnityEngine;
using System.Collections;

public class TileRenderer : MonoBehaviour
{
	public int ID;

	public void Render(int zX, int zZ)
	{
		transform.position = new Vector3(zX, 0, zZ);
	}
}