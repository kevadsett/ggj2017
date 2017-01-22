using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static float GameStartTime = 0;

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
	private eState mState;
	private float mRoundStartedTime;

	private void Start()
	{
		Instance = this;

		mTileManager = new TileManager();
		mTileManager.SetupTiles(3);
		new EntityManager();

		var testDog = new DogEntity (0, 0, 0);
		new SheepEntity (0, 2, 1, testDog);
		new SheepEntity (0, 3, 1, testDog);
		new SheepEntity (0, 4, 3, testDog);
		
		SetState(eState.Menu);
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Menu:
			if (Input.GetKeyUp(KeyCode.Space))
			{
				SetupGame();
			}
			break;
		case eState.Game:
			EntityManager.UpdateEntities(mState);

			var timePerRound = GameDataBase.Instance.GetData(0).RoundTime;
			if ((Time.time - mRoundStartedTime) >= timePerRound)
			{
				mRoundStartedTime = Time.time + GameDataBase.Instance.GetData(0).TimeToAnimateWave;
				ToiletWave.TriggerWave();
			}
			UIManager.UpdateUI(Instance.mState, (timePerRound - (Time.time - mRoundStartedTime)));
			break;
		case eState.GameEnd:
			UIManager.UpdateUI(Instance.mState, 0);
			break;
		}
	}

	private void SetState(eState zNewState)
	{
		Instance.mState = zNewState;

		UIManager.UpdateUI(Instance.mState, (Time.time - mRoundStartedTime));
	}

	private void SetupGame()
	{
		//do initial game setup here
		//randomise sheep
		//place dog
		//reset tiles
		GameStartTime = Time.time;
		Instance.mTileManager.SetupTiles(3);
		Instance.SetState(eState.Game);
		mRoundStartedTime = Time.time;
	}

	public static void StartGame()
	{
		Instance.SetupGame();
	}

	public static void ShowMainMenu()
	{
		Instance.SetState(eState.Menu);
	}

	public static void GameEnd()
	{
		Instance.SetState(eState.GameEnd);
	}
}
