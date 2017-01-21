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
		mTileManager.RenderTiles();

		mEntityManager = new EntityManager();

		var testSheep = new SheepEntity (0, 0, 0);
		
		SetState(eState.Splash);
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Splash:

			break;
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

	private void SetState(eState zNewState)
	{
		Instance.mState = zNewState;

		UIManager.UpdateUI(Instance.mState);
	}

	public static void SetupGame()
	{
		Instance.SetState(eState.Game);
	}

	public static void ShowMainMenu()
	{
		Instance.SetState(eState.Menu);
	}
}