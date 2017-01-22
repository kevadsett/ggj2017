using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private static TileManager Instance;

	public static int WIDTH;
	public static int HEIGHT;

	private List<Tile> mTiles;

	private GameData mGameData;

	public TileManager()
	{
		Instance = this;

		mTiles = new List<Tile>();
	}
	public void SetupTiles()
	{
		mTiles.Clear();

		int id = 0;
		mGameData = GameDataBase.Instance.GetData ();
		WIDTH = (int)mGameData.GridDimensions.x;
		HEIGHT = (int)mGameData.GridDimensions.y;

		for (int i = 0; i < WIDTH; ++i)
		{
			for (int j = 0; j < HEIGHT; ++j)
			{
				var tileType = Tile.eType.Sand;
				CreateTile(id, i, j, tileType);
				id++;
			}
		}
	}
	
	public void SetupGround(GameObject groundQuad)
	{
		GameObject ground = GameObject.Instantiate (groundQuad);
		ground.transform.localScale = new Vector3(WIDTH, HEIGHT, 1);
	}
	

	private void CreateTile(int zId, int zX, int zZ, Tile.eType zType)
	{
		var tileType = zType;
		if (zZ == TileManager.HEIGHT - 1 && zX > 4 && zX < TileManager.WIDTH - 5)
		{
			tileType = Tile.eType.House;
		}
		var tile = new Tile(zId, zX, zZ, tileType);
		mTiles.Insert(zId,tile);
	}

	public static Tile GetTileAtPosition(int zX, int zZ)
	{
		var tiles = Instance.mTiles;

		foreach (Tile tile in tiles)
		{
			if (tile.PosX == zX && tile.PosZ == zZ)
			{
				return tile;
			}
		}

		Debug.LogError("Could not find tile at position " + zX + " " + zZ);
		return null;
	}
}