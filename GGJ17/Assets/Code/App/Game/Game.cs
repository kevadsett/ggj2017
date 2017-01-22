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

	public AnimationCurve InstructionUIFadeCurve;

	public enum eState
	{
		Splash,
		Menu,
		Game,
		GameEnd,
		Waves,
		Dialogue,
		LevelSucceeded
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
			EntityManager.UpdateEntities (mState);

			var timePerRound = mCurrentLevel.WaveIntervalTime;
			var elapsedTime = (Time.time - mRoundStartedTime);
			if (elapsedTime >= timePerRound - 2f)
			{
				Horn.TriggerAnimation ();
			}

			if (elapsedTime >= timePerRound)
			{
				mRoundStartedTime = Time.time + GameDataBase.Instance.GetData (0).TimeToAnimateWave;
				ToiletWave.TriggerWave ();
				mCurrentWaveIndex++;
			}
			if (mDog.HasMoved)
			{
				UIManager.UpdateUI (Instance.mState, (timePerRound - (Time.time - mRoundStartedTime)), true, InstructionUIFadeCurve);
			}
			else
			{
				UIManager.UpdateUI (Instance.mState, (timePerRound - (Time.time - mRoundStartedTime)));
			}

			if (Input.GetKeyUp(KeyCode.T))
			{
				GameEnd();
			}
			break;
		case eState.GameEnd:
			UIManager.UpdateUI(Instance.mState, 0);
			break;
		case eState.LevelSucceeded:
			if (Input.GetKeyUp (KeyCode.Space))
			{
				StartNextLevel ();
			}
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
			if (mSheepList != null)
			{
				mSheepList [i].Destroy (true);
			}
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

	public static void StartNextLevel()
	{
		Instance.mCurrentLevel = LevelDataBase.Instance.GetNextLevel ();
		Instance.SetupGame ();
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
		int sheepCount = EntityManager.GetSheepCount ();
		Instance.mScoreKeeper.AddScore(sheepCount);

	}

	public static void FinishWave()
	{
		int sheepCount = EntityManager.GetSheepCount ();
	
		if (sheepCount == 0)
		{
			Instance.SetState (eState.GameEnd);
		}
		else if (Instance.mCurrentWaveIndex >= Instance.mCurrentLevel.WaveCount)
		{
			Instance.SetState (eState.LevelSucceeded);
		}

		var sound = "Success" + sheepCount;
		AudioPlayer.PlaySound (sound, Vector3.zero);
	}
}
