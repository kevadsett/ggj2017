using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public enum eState
	{
		Menu,
		Game,
		Waves,
		Dialogue,
	}


	private TileManager mTileManager;
	private EntityManager mEntityManager;
	private eState mState;


	private void Start()
	{
		mState = eState.Menu;

		mTileManager = new TileManager();
		mTileManager.RenderTiles();

		mEntityManager = new EntityManager();

		var testSheep = new SheepEntity (0, 0, 0);
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Menu:
			break;
		case eState.Game:
			//get input
			//pass input to dog
			break;
		case eState.Waves:
			break;
		case eState.Dialogue:
			break;
		}

		EntityManager.UpdateEntities (mState);
	}
}