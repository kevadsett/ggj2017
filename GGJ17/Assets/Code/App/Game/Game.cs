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
	private float mNextWaveTimer;

	private LevelData mCurrentLevel;

	private int mCurrentWaveIndex = 0;

	private DogEntity mDog;
	private List<SheepEntity> mSheepList;
	private List<SheepData> mDeadSheep;

	private float _intervalMultiplier = 1;

	private void Start()
	{
		Instance = this;

		mTileManager = new TileManager(MoundObject);
		mScoreKeeper = new Score();
		new EntityManager();

		mDog = new DogEntity (0, 8, 7);
		mSheepList = new List<SheepEntity> ();

		mCurrentLevel = LevelDataBase.Instance.GetLevel (0);

		mDeadSheep = new List<SheepData> ();
		
		SetState(eState.Menu);
		SplashAnimation.Appear();
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Menu:
			if (Input.GetKeyUp(KeyCode.Space))
			{
				SplashAnimation.Hide();
				SetupGame();
			}
			break;
		case eState.Game:			
			EntityManager.UpdateEntities (mState);

			var timePerRound = mCurrentLevel.WaveIntervalTime * _intervalMultiplier;
			var elapsedTime = (Time.time - mRoundStartedTime);
			var hornWarningTime = 2f;

			if (Input.GetKeyUp(KeyCode.Space))
			{
				if (elapsedTime < timePerRound - 2f)
				{
					mRoundStartedTime = Time.time - timePerRound + hornWarningTime;
				}
			}

			if (elapsedTime >= timePerRound - hornWarningTime)
			{
				Horn.TriggerAnimation ();
			}

			if (elapsedTime >= timePerRound)
			{
				UIManager.ShowHideTimer (false);
				mRoundStartedTime = Time.time + GameDataBase.Instance.GetData (0).TimeToAnimateWave;
				ToiletWave.TriggerWave ();
				mCurrentWaveIndex++;
			}
			if (mDog.HasMoved)
			{
				UIManager.UpdateUI (Instance.mState, LevelDataBase.Instance._levelIndex, (timePerRound - (Time.time - mRoundStartedTime)), true, InstructionUIFadeCurve);
			}
			else
			{
				UIManager.UpdateUI (Instance.mState, LevelDataBase.Instance._levelIndex, (timePerRound - (Time.time - mRoundStartedTime)));

			}
			UIManager.UpdateUI (Instance.mState, LevelDataBase.Instance._levelIndex, (timePerRound - (Time.time - mRoundStartedTime)));
			break;
		case eState.GameEnd:
			UIManager.UpdateUI(Instance.mState, LevelDataBase.Instance._levelIndex, 0);
			break;
		case eState.LevelSucceeded:
			mNextWaveTimer += Time.deltaTime;
			if (mNextWaveTimer >= 1.5f)
			{
				StartNextLevel ();
				UIManager.ShowHideTimer (true);
			}
			break;
		}
	}

	private void SetState(eState zNewState)
	{
		Instance.mState = zNewState;

		UIManager.UpdateUI(Instance.mState, LevelDataBase.Instance._levelIndex, (Time.time - mRoundStartedTime));
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

		MusicPlayer.FadeOut (1);
		MusicPlayer.Play (0);
	}

	public static void StartNextLevel()
	{
		Instance.mCurrentLevel = LevelDataBase.Instance.GetNextLevel ();
		Instance.SetupGame ();
	}

	public static void StartGame()
	{
		Instance._intervalMultiplier = 1;
		LevelDataBase.Instance.Reset ();
		Instance.mCurrentLevel = LevelDataBase.Instance.GetLevel (0);
		Instance.SetupGame();
		Score.Instance.ResetScore();
	}

	public static void ShowMainMenu()
	{
		Instance.SetState(eState.Menu);

		MusicPlayer.FadeOut (0);
	}

	public static void GameEnd()
	{
		Instance.SetState(eState.GameEnd);
	}

	public static void ResolveWave()
	{
		Instance.mDeadSheep.AddRange(EntityManager.DrownSheep());
		int sheepCount = EntityManager.GetSheepCount ();
		Instance.mScoreKeeper.AddScore(sheepCount);
		if (sheepCount == 0)
		{
			MusicPlayer.FadeOut (0);
		}
	}

	public static void FinishWave()
	{
		int sheepCount = EntityManager.GetSheepCount ();
	
		if (sheepCount == 0)
		{
			ResetEulogy.ResetPosition();
			UIManager.CreateEulogies (Instance.mDeadSheep);
			Instance.SetState (eState.GameEnd);
		}
		else if (Instance.mCurrentWaveIndex >= Instance.mCurrentLevel.WaveCount)
		{
			Instance.SetState (eState.LevelSucceeded);
			Instance.mNextWaveTimer = 0.0f;
		}

		var sound = "Success" + sheepCount;
		AudioPlayer.PlaySound (sound, Vector3.zero);
	}

	public static void MultiplyInterval()
	{
		Instance._intervalMultiplier *= GameDataBase.Instance.GetData ().RestartTimerMultiplier;
	}
}
