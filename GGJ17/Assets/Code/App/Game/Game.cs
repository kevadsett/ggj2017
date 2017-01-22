using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	[SerializeField]
	private Transform m_WaterPlane;

	public static float GameStartTime = 0;
	public static Game Instance;
	public GameObject MoundObject;

	public enum eState
	{
		Splash,
		Menu,
		Game,
		GameEnd,
		Waves,
		Dialogue,
	}

	private Score mScoreKeeper;
	private TileManager mTileManager;
	private eState mState;
	private float mRoundStartedTime;

	private LevelData mCurrentLevel;

	private int mCurrentWaveIndex = 0;

	private DogEntity mDog;
	private List<SheepEntity> mSheepList;

	private void Start()
	{
		Instance = this;

		mTileManager = new TileManager(MoundObject);
		mScoreKeeper = new Score();
		new EntityManager();

		mDog = new DogEntity (0, 0, 0);
		mSheepList = new List<SheepEntity> ();

		mCurrentLevel = LevelDataBase.Instance.GetLevel (0);
		
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
			if (mCurrentWaveIndex < mCurrentLevel.WaveCount)
			{
				EntityManager.UpdateEntities (mState);

				var timePerRound = mCurrentLevel.WaveIntervalTime;
				if ((Time.time - mRoundStartedTime) >= timePerRound)
				{
					mRoundStartedTime = Time.time + GameDataBase.Instance.GetData(0).TimeToAnimateWave;
					ToiletWave.TriggerWave();
					Horn.TriggerAnimation();
					mCurrentWaveIndex++;
				}
				UIManager.UpdateUI (Instance.mState, (timePerRound - (Time.time - mRoundStartedTime)));
			}
			else
			{
				mRoundStartedTime = Time.time + GameDataBase.Instance.GetData(0).TimeToAnimateWave;
				ToiletWave.TriggerWave();

				mScoreKeeper.AddScore(EntityManager.GetSheepCount());
				StartNextLevel ();
				mRoundStartedTime = Time.time + GameDataBase.Instance.GetData(0).TimeToAnimateWave;
				ToiletWave.TriggerWave();
				Horn.TriggerAnimation();
			}

			if (Input.GetKeyUp(KeyCode.T))
			{
				GameEnd();
			}
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
		mCurrentWaveIndex = 0;
		GameStartTime = Time.time;
		for (int i = 0; i < mSheepList.Count; i++)
		{
			mSheepList [i].Destroy ();
		}
		mSheepList.Clear ();
		for (int i = 0; i < mCurrentLevel.SheepCount; i++)
		{
			mSheepList.Add(new SheepEntity (0, Random.Range(1, 16), Random.Range(1, 8), mDog));
		}
		Instance.mTileManager.SetupTiles(mCurrentLevel.SheepCount);
		Instance.SetState(eState.Game);
		mRoundStartedTime = Time.time;
	}

	private void StartNextLevel()
	{
		mCurrentLevel = LevelDataBase.Instance.GetNextLevel ();
		SetupGame ();
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

	public static void ResolveWave()
	{
		EntityManager.DrownSheep();
		Instance.mScoreKeeper.AddScore(EntityManager.GetSheepCount());
	}
}
