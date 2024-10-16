using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public State currentState;
    public ExitCheckPointState exitCheckPointState;
    public EnterCheckPointState enterCheckPointState;
    public MenuState menuState;
    public FlyingState flyingState;
    public FinaleState FinaleState;
    public TraversalManager TM;
    public GameObject PlayerPrefab;
    public GameObject Player;
    public int Level = 0;
    public GameObject FlyingScreen;
    public GameObject CheckPointScreen;
    public Animator blackScreenAnim;
    public bool FadeOutComplete;
    public bool FadeInComplete;

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
    public void ChangeState(State state)
    {
        if (currentState != null)
            currentState.End();
        state.Init();
        currentState = state;
    }
}
