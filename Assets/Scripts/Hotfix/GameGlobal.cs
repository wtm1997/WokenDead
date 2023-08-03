using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class GameGlobal : MonoBehaviour
{
    public static GameGlobal Inst;

    private GameStateMgr _gameStateMgr;
    private LogicWorld _logicWorld;
    public LogicWorld LogicWorld => _logicWorld;

    public void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        UIManager.Inst.Init();
        
        _gameStateMgr = new GameStateMgr();
        _gameStateMgr.Init();
        _gameStateMgr.Start();
    }

    private void Update()
    {
        _gameStateMgr.Update();
    }

    private void OnDestroy()
    {
        _gameStateMgr.Release();
    }

    public void CreateLogicWorld()
    {
        _logicWorld = new LogicWorld();
        _logicWorld.Create();
    }

    public void UpdateLogicWorld()
    {
        _logicWorld.Update();
    }

    public void ReleaseLogicWorld()
    {
        _logicWorld.Release();
    }
}
