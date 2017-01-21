﻿using UnityEngine;
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
	private WaveManager mWaveManager;

	private bool changed = false;

	private void Start()
	{
		Instance = this;

		mTileManager = new TileManager();
		mTileManager.SetupTiles();
		mTileManager.RenderTiles();

		new EntityManager();

		var testDog = new DogEntity (0, 0, 0);
		new SheepEntity (0, 2, 1, testDog);
		new SheepEntity (0, 3, 1, testDog);
		new SheepEntity (0, 4, 3, testDog);
		
		SetState(eState.Menu);
		mWaveManager = new WaveManager ();
	}

	private void Update()
	{
		switch (mState)
		{
		case eState.Game:
			mWaveManager.Update();
			EntityManager.UpdateEntities(mState);
			break;
		}
		mTileManager.RenderTiles ();
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
		GameStartTime = Time.time;
		Instance.mWaveManager.Reset();
		Instance.mTileManager.SetupTiles();
		Instance.mTileManager.RenderTiles();
		Instance.SetState(eState.Game);
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