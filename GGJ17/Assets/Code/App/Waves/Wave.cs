using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

	private int mWest = 0, mSouth = 1, mEast = 2;
	private int mCardinalDirection;

	private float startTime, lifeTime;
	private bool incoming;
	public bool done;

	public Wave (int zCardinalDirection) {
		mCardinalDirection = zCardinalDirection;
		startTime = Time.time;
		incoming = true;
		done = false;
		lifeTime = GameDataBase.Instance.GetData(0).WaveSpeed * 0.5f;
	}

	public void Update() {
		if (incoming) {
			Income ();
			incoming = false;
		} else if (Time.time >= startTime + lifeTime){
			Recede ();
			done = true;
		}
	}

	void Income() {
		if (mCardinalDirection == mWest) {
			bool grass;
			for (int j = 0; j < TileManager.HEIGHT; j++) {
				grass = false;
				int i = 0;
				while (!grass) {
					var affectedTile = TileManager.GetTileAtPosition (i, j);
					if (affectedTile == null)
						return;

					grass = CheckGameEnd(affectedTile);
					grass = CheckSheep(affectedTile);

					if (grass)
						break;

					if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Sand) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						i++;
					} else if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Grass) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						grass = true;
					} else {
						i++;
					}
				}
			}
		} else if (mCardinalDirection == mSouth) {
			bool grass;
			for (int i = 0; i < TileManager.WIDTH; i++) {
				grass = false;
				int j = 0;
				while (!grass) {
					var affectedTile = TileManager.GetTileAtPosition (i, j);
					if (affectedTile == null)
						return;

					grass = CheckGameEnd(affectedTile);
					grass = CheckSheep(affectedTile);

					if (grass)
						break;

					if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Sand) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						j++;
					} else if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Grass) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						grass = true;
					} else {
						j++;
					}
				}
			}
		} else if (mCardinalDirection == mEast) {
			bool grass;
			for (int j = 0; j < TileManager.HEIGHT; j++) {
				grass = false;
				int i = TileManager.WIDTH-1;
				while (!grass) {
					var affectedTile = TileManager.GetTileAtPosition (i, j);
					if (affectedTile == null)
						return;

					grass = CheckGameEnd(affectedTile);
					grass = CheckSheep(affectedTile);

					if (grass)
						break;

					if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Sand) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						i--;
					} else if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Grass) {
						TileManager.ReplaceTile (i, j, Tile.eType.Water);
						grass = true;
					} else {
						i--;
					}
				}
			}
		}
	}

	void Recede() {
		if (mCardinalDirection == mWest) {
			for (int i = 0; i < TileManager.WIDTH; i++) {
				for (int j = TileManager.WIDTH-1; j>= 0; j--) {
					if (TileManager.GetTileAtPosition (j, i).TileType == Tile.eType.Water) {
						TileManager.ReplaceTile (j, i, Tile.eType.Sand);
					} 
				}
			}
		} else if (mCardinalDirection == mSouth) {
			for (int i = TileManager.WIDTH-1; i >= 0; i--) {
				for (int j = TileManager.HEIGHT-1; j>= 0; j--) {
					if (TileManager.GetTileAtPosition (i, j).TileType == Tile.eType.Water) {
						TileManager.ReplaceTile (i, j, Tile.eType.Sand);
					} 
				}
			}
		} else if (mCardinalDirection == mEast) {
			for (int i = 0; i < TileManager.HEIGHT; i++) {
				for (int j = 0; i < TileManager.WIDTH; j++) {
					if (TileManager.GetTileAtPosition (j, i).TileType == Tile.eType.Water) {
						TileManager.ReplaceTile (j, i, Tile.eType.Sand);
					} 
				}
			}
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
