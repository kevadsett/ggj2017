using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private static TileManager Instance;

	public static int WIDTH = 32;
	public static int HEIGHT = 19;

	private List<Tile> mTiles;
	private List<TileRenderer> mRenderTiles;


	public TileManager()
	{
		Instance = this;

		mTiles = new List<Tile>();
		mRenderTiles = new List<TileRenderer>();
		int id = 0;


		for (int i = 0; i < WIDTH; ++i)
		{
			for (int j = 0; j < HEIGHT; ++j)
			{
				CreateTile(id, i, j);
				id++;
			}
		}
	}

	private void CreateTile(int zId, int zX, int zZ)
	{
		var tileType = Tile.eType.Grass;

		if (zX == 0 || zX == WIDTH - 1 || zZ == 0)
		{
			tileType = Tile.eType.Sand;
		}

		var tile = new Tile(zId, zX, zZ, tileType);
		mTiles.Add(tile);

		var tilePrefab = TileLibrary.GetTile("grass");
		var renderTileObject = GameObject.Instantiate(tilePrefab) as GameObject;
		var renderTile = renderTileObject.AddComponent<TileRenderer>();
		renderTile.ID = zId;
		mRenderTiles.Add(renderTile);
	}

	public void RenderTiles()
	{
		foreach (TileRenderer renderTile in mRenderTiles)
		{
			var tile = mTiles[renderTile.ID];
			renderTile.Render(tile);
		}
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