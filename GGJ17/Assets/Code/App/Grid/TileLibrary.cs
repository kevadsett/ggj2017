using UnityEngine;
using System.Collections;

public class TileLibrary : MonoBehaviour
{
	private static TileLibrary Instance;

	[SerializeField]
	private GameObject[] m_Tiles;


	private void Awake()
	{
		Instance = this;
	}

	public static GameObject GetTile(Tile.eType zTileType)
	{
		foreach (GameObject go in Instance.m_Tiles)
		{
			if (go.name == zTileType.ToString().ToLower())
				return go;
		}


		Debug.LogError("Cannot find tile of type " + zTileType);
		return null;
	}
}