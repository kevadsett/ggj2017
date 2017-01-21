using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance;

	public enum eState
	{
		Splash,
		Menu,
		Game,
		GameEnd,
		Waves,
		Dialogue,
	}

	private TileManager mTileManager;
	private EntityManager mEntityManager;
	private eState mState;


	private void Start()
	{
		Instance = this;

		mTileManager = new TileManager();
		mTileManager.SetupTiles();
		mTileManager.RenderTiles();

		mEntityManager = new EntityManager();

		var testSheep = new SheepEntity (0, 0, 0);
		
		SetState(eState.Splash);
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Game:
			//update entities
			//update waves
			break;
		case eState.Waves:
			break;
		}

		EntityManager.UpdateEntities (mState);
	}

	private void SetState(eState zNewState)
	{
		Instance.mState = zNewState;

		UIManager.UpdateUI(Instance.mState);
	}

	public static void SetupGame()
	{
		//do initial game setup here
		//randomise sheep
		//place dog
		//reset tiles
		Instance.mTileManager.SetupTiles();
		Instance.mTileManager.RenderTiles();
		Instance.SetState(eState.Game);
	}

	public static void ShowMainMenu()
	{
		Instance.SetState(eState.Menu);
	}
}