using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool inCheckpoint;
    public EnterCheckPointState enterCheckPointState;
    public FlyingState flyingState;
    public FinaleState FinaleState;
    public TraversalManager TM;
    [Space(5)]
    public GameObject PlayerPrefab;
    public GameObject FlyingScreen;
    public GameObject CheckPointScreen;
    public GameObject FinaleScreen;
    public GameObject kite;
    [Space(5)]
    public RectTransform barFillTemporary;
    public RectTransform barFillPermanent;
    [SerializeField] private RectTransform scoreBar;
    [Space(5)]
    public Animator blackScreenAnim;
    [Space(5)]
    public TextMeshProUGUI scoreText;
    [Space(5)]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource barShakeSound;
    public GameObject[] feathers;
    public Color[] featherColors;
    [HideInInspector] public bool FadeOutComplete;
    [HideInInspector] public bool FadeInComplete;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public int Level = 0;

    private State currentState;
    private MenuState menuState;
    private ExitCheckPointState exitCheckPointState;
    private Vector3 originalPos;
    public List<GameObject> featherResults;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        Application.targetFrameRate = 60;
        ChangeState(flyingState);
        originalPos = scoreBar.anchoredPosition;
    }

    private void Update()
    {
        ExecuteState(currentState);

        if (backgroundMusic.volume < 0.1f){
            backgroundMusic.volume = Mathf.MoveTowards(backgroundMusic.volume, 0.1f, 0.1f * Time.deltaTime);
        }
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

    public void TriggerShake(){
        StartCoroutine(Shake());
        barShakeSound.Play();
    }

    private IEnumerator Shake(){
        float elapsed = 0.0f;
        float currentShakeAmount = 20f;

        while (elapsed < 0.25f){
            Vector2 randomPoint = originalPos + (Vector3)Random.insideUnitCircle * 20f;
            scoreBar.anchoredPosition = randomPoint;

            elapsed += Time.deltaTime;
            currentShakeAmount = Mathf.Lerp(20f, 0, elapsed / 0.25f);
            yield return null;
        }

        scoreBar.anchoredPosition = originalPos;
    }
}
