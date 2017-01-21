using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private static TileManager Instance;

	private int mWidth = 32;
	private int mHeight = 19;

	private List<Tile> mTiles;
	private List<TileRenderer> mRenderTiles;


	public TileManager()
	{
		Instance = this;

		mTiles = new List<Tile>();
		mRenderTiles = new List<TileRenderer>();
		int id = 0;


		for (int i = 0; i < mWidth; ++i)
		{
			for (int j = 0; j < mHeight; ++j)
			{
				var tileType = Tile.eType.Grass;
				var tile = new Tile(id, i, j, tileType);
				mTiles.Add(tile);

				var tilePrefab = TileLibrary.GetTile("grass");
				var renderTileObject = GameObject.Instantiate(tilePrefab) as GameObject;
				var renderTile = renderTileObject.AddComponent<TileRenderer>();
				renderTile.ID = id;
				mRenderTiles.Add(renderTile);
				id++;
			}
		}
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