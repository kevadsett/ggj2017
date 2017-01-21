using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private static TileManager Instance;

	public static int WIDTH;
	public static int HEIGHT;

	private List<Tile> mTiles;
	private List<TileRenderer> mRenderTiles;

	private GameData mGameData;

	public TileManager()
	{
		Instance = this;

		mTiles = new List<Tile>();
		mRenderTiles = new List<TileRenderer>();
	}

	public void SetupTiles()
	{
		mTiles.Clear();
		foreach (TileRenderer tile in mRenderTiles)
		{
			GameObject.Destroy(tile.gameObject);
		}
		mRenderTiles.Clear();

		int id = 0;
		mGameData = GameDataBase.Instance.GetData ();
		WIDTH = (int)mGameData.GridDimensions.x;
		HEIGHT = (int)mGameData.GridDimensions.y;

		for (int i = 0; i < WIDTH; ++i)
		{
			for (int j = 0; j < HEIGHT; ++j)
			{
				CreateTile(id, i, j, Tile.eType.Grass);
				id++;
			}
		}
	}

	private void CreateTile(int zId, int zX, int zZ, Tile.eType zType)
	{
		var tileType = zType; //Tile.eType.Grass;

		/*if (zX == 0 || zX == WIDTH - 1 || zZ == 0)
		{
			tileType = Tile.eType.Sand;
		}*/

		if (zZ == TileManager.HEIGHT - 1 && zX > 4 && zX < TileManager.WIDTH - 5)
		{
			tileType = Tile.eType.House;
		}

		var tile = new Tile(zId, zX, zZ, tileType);
		mTiles.Insert(zId,tile);

		var tilePrefab = TileLibrary.GetTile(tileType);
		var renderTileObject = GameObject.Instantiate(tilePrefab) as GameObject;
		var renderTile = renderTileObject.AddComponent<TileRenderer>();
		renderTile.ID = zId;
		mRenderTiles.Insert(zId,renderTile);
	}

	public static void ReplaceTile(int zX, int zZ, Tile.eType zType) {
		Tile oldTile = GetTileAtPosition (zX, zZ);
		int ID = oldTile.ID;
		int x = oldTile.PosX;
		int z = oldTile.PosZ;

		Instance.mTiles.RemoveAt (ID);
		GameObject.Destroy (Instance.mRenderTiles [ID].gameObject);
		Instance.mRenderTiles.RemoveAt (ID);

		Instance.CreateTile (ID, x, z, zType);
	}

	public void RenderTiles()
	{
		foreach (TileRenderer renderTile in mRenderTiles)
		{
			foreach (Tile tile in mTiles) {
				if (renderTile.ID == tile.ID) {
					renderTile.Render (tile);
				}
			}
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