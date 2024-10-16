using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public State currentState;
    public CheckPointState checkPointState;
    public MenuState menuState;
    public FlyingState flyingState;
    public TraversalManager TM;
    public GameObject Player;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        Application.targetFrameRate = 60;
        ChangeState(flyingState);
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteState(currentState);
    }
    void ExecuteState(State state)
    {
        state.Execute();
    }
    void ChangeState(State state)
    {
        if (currentState != null)
            currentState.End();
        state.Init();
        currentState = state;
    }
}
