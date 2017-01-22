using UnityEngine;
using System.Collections.Generic;

public class TileManager
{
	private GameObject _moundObject;
	private static TileManager Instance;

	public static int WIDTH;
	public static int HEIGHT;

	private List<Tile> mTiles;

	private GameData mGameData;

	public TileManager(GameObject moundObject)
	{
		Instance = this;

		mTiles = new List<Tile>();
		_moundObject = moundObject;
	}
	public void SetupTiles(int moundTargetCount)
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
				var tileType = Tile.eType.Ground;
				CreateTile(id, i, j, tileType);
				id++;
			}
		}

		Tile[] moundTiles = new Tile[moundTargetCount];
		int createdMoundsCount = 0;
		while (createdMoundsCount < moundTargetCount)
		{
			int randomIndex = Random.Range (0, id);
			Tile tile = mTiles[randomIndex];
			if (tile.TileType == Tile.eType.Ground)
			{
				tile.TileType = Tile.eType.Mound;
				moundTiles [createdMoundsCount] = tile;
				createdMoundsCount++;
			}
		}
		for (int i = 0; i < moundTiles.Length; i++)
		{
			Tile tile = moundTiles [i];
			Debug.Log (tile);
			GameObject moundObject = GameObject.Instantiate (_moundObject);
			moundObject.transform.position = new Vector3 (tile.PosX, 0, tile.PosZ);
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