using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	private TileManager mTileManager;


	private void Start()
	{
		mTileManager = new TileManager();
		mTileManager.RenderTiles();
	}
}