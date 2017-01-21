using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private int mWidth = 32;
	private int mHeight = 19;

	private List<Tile> mTiles;
	private List<TileRenderer> mRenderTiles;

	public TileManager()
	{
		mTiles = new List<Tile>();
		mRenderTiles = new List<TileRenderer>();
		int id = 0;

		for (int i = 0; i < mWidth; ++i)
		{
			for (int j = 0; j < mHeight; ++j)
			{
				var tile = new Tile(id, i, j);
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
			renderTile.Render(tile.PosX, tile.PosZ);
		}
	}
}