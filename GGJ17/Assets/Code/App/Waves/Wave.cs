using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

	private int mWest = 0, mSouth = 1, mEast = 2;
	private int mCardinalDirection, mDepth, incoming;
	private bool[] mGrass;

	private float startTime, lifeTime;
	public bool done;

	public Wave (int zCardinalDirection) {
		mCardinalDirection = zCardinalDirection;
		if (zCardinalDirection == mWest || zCardinalDirection == mEast) {
			mGrass = new bool[TileManager.HEIGHT];
			incoming = TileManager.HEIGHT-1;
		} else {
			mGrass = new bool[TileManager.WIDTH];
			incoming = TileManager.WIDTH-2;
		}
		startTime = Time.time;

		done = false;
		lifeTime = GameDataBase.Instance.GetData(0).WaveSpeed * 0.5f;
		mDepth = 0;
	}

	public void Update() {
		if (mDepth == -1) {
			done = true;
			return;
		}
		if (incoming > 0) {
			Income ();
		} else if (Time.time >= startTime + lifeTime){
			Recede ();
		}

	}

	void Income() {
		if (mCardinalDirection == mWest) {
			TileManager.ReplaceTile (mDepth, 0, Tile.eType.Water);
			for (int i = 1; i < TileManager.HEIGHT; i++) {
				var tile = TileManager.GetTileAtPosition (mDepth, i);
				if (CheckGameEnd(tile) || CheckSheep(tile))
				{
					incoming--;
					continue;
				}

				if (TileManager.GetTileAtPosition (mDepth, i).TileType == Tile.eType.Water) {
					incoming--;
					continue;
				}
				if (mGrass [i] == true) {
					continue;
				}
				if (TileManager.GetTileAtPosition (mDepth, i).TileType == Tile.eType.Sand) {
					TileManager.ReplaceTile (mDepth, i, Tile.eType.Water);
				} else if (TileManager.GetTileAtPosition (mDepth, i).TileType == Tile.eType.Grass) {
					TileManager.ReplaceTile (mDepth, i, Tile.eType.Water);
					mGrass [i] = true;
					incoming--;
				}
			}
			mDepth++;
		} else if (mCardinalDirection == mSouth) {
			TileManager.ReplaceTile (0, mDepth, Tile.eType.Water);
			TileManager.ReplaceTile (TileManager.WIDTH-1, mDepth, Tile.eType.Water);
			for (int i = 1; i < TileManager.WIDTH-1; i++) {
				var tile = TileManager.GetTileAtPosition (i, mDepth);
				if (CheckGameEnd(tile) || CheckSheep(tile))
				{
					incoming--;
					continue;
				}

				if (TileManager.GetTileAtPosition (i,mDepth).TileType == Tile.eType.Water) {
					incoming--;
					continue;
				}
				if (mGrass [i] == true) {
					continue;
				}
				if (TileManager.GetTileAtPosition (i, mDepth).TileType == Tile.eType.Sand) {
					TileManager.ReplaceTile (i, mDepth, Tile.eType.Water);
				} else if (TileManager.GetTileAtPosition (i, mDepth).TileType == Tile.eType.Grass) {
					TileManager.ReplaceTile (i, mDepth, Tile.eType.Water);
					mGrass [i] = true;
					incoming--;
				}
			}
			mDepth++;
		} else if (mCardinalDirection == mEast) {
			TileManager.ReplaceTile (TileManager.WIDTH-mDepth-1, 0, Tile.eType.Water);
			for (int i = 0; i < TileManager.HEIGHT; i++) {
				var tile = TileManager.GetTileAtPosition (TileManager.WIDTH-mDepth-1,i);
				if (CheckGameEnd(tile) || CheckSheep(tile))
				{
					incoming--;
					continue;
				}

				if (TileManager.GetTileAtPosition (TileManager.WIDTH-mDepth-1,i).TileType == Tile.eType.Water) {
					incoming--;
					continue;
				}
				if (mGrass [i] == true) {
					continue;
				}
				if (TileManager.GetTileAtPosition (TileManager.WIDTH-mDepth-1, i).TileType == Tile.eType.Sand) {
					TileManager.ReplaceTile (TileManager.WIDTH-mDepth-1, i, Tile.eType.Water);
				} else if (TileManager.GetTileAtPosition (TileManager.WIDTH-mDepth-1, i).TileType == Tile.eType.Grass) {
					TileManager.ReplaceTile (TileManager.WIDTH-mDepth-1, i, Tile.eType.Water);
					mGrass [i] = true;
					incoming--;
				}
			}
			mDepth++;
		}
	}

	void Recede() {
		if (mCardinalDirection == mWest) {
			for (int j = 0; j < TileManager.HEIGHT; j++) {
				if (TileManager.GetTileAtPosition (mDepth, j).TileType == Tile.eType.Water) {
					TileManager.ReplaceTile (mDepth, j, Tile.eType.Sand);
				}
			}
			mDepth--;
		} else if (mCardinalDirection == mSouth) {
			for (int j = 0; j < TileManager.WIDTH; j++) {
				if (TileManager.GetTileAtPosition (j,mDepth).TileType == Tile.eType.Water) {
					TileManager.ReplaceTile (j,mDepth, Tile.eType.Sand);
				}
			}
			mDepth--;
		} else if (mCardinalDirection == mEast) {
			for (int j = 0; j < TileManager.HEIGHT; j++) {
				if (TileManager.GetTileAtPosition (TileManager.WIDTH-mDepth-1, j).TileType == Tile.eType.Water) {
					TileManager.ReplaceTile (TileManager.WIDTH-mDepth-1, j, Tile.eType.Sand);
				}
			}
			mDepth--;
		}
	}

	private bool CheckGameEnd(Tile zTile)
	{
		if (zTile.TileType == Tile.eType.House)
		{
			Game.GameEnd();
			return true;
		}

		return false;
	}

	private bool CheckSheep(Tile zTile)
	{
		var entity = EntityManager.GetEntityAtPosition(zTile.PosX, zTile.PosZ);

		if (entity != null && entity is SheepEntity)
		{
			return true;
		}

		return false;
	}
}
