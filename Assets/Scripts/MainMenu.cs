using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;
using Dan.Main;

public class MainMenu : MonoBehaviour{

    [Header("Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject settingsScreen;

    [Header("References")]
    [SerializeField] private Animator blackScreen;
    [SerializeField] private GameObject loadingImage;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private GameObject bondFire;
    [SerializeField] private GameObject highscoreUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject spacebar;
    [SerializeField] private List<TextMeshProUGUI> scores;
    private bool isZooming;
    private bool isTransitioning;

    private void Start(){
        Screen.SetResolution(1920, 1080, true);

        if (PlayerPrefs.HasKey("AudioVolume")){
            LoadVolume();
        } else {
            SetVolume();
        }

        if (PlayerPrefs.HasKey("Highscore")){
            highscoreUI.SetActive(true);
            highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        } else {
            highscoreUI.SetActive(false);
        }

        InvokeRepeating("GetLeaderboard", 0f, 5f);
    }

    private void Update(){
        if (mainMenuMusic.volume < 1 && !isTransitioning){
            mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 1f, 1f * Time.deltaTime);
        } else if (mainMenuMusic.volume >= 0 && isTransitioning){
            mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 0f, 1f * Time.deltaTime);
        }

        if (isZooming){
            // Zoom in
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 0.01f, Time.deltaTime * 5);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(4.3f, 0.7f, 0), Time.deltaTime * 5);
        }
    }

    public void GetLeaderboard(){
        if (isZooming) return;

        LeaderboardCreator.GetLeaderboard("91695c89d9fc9f8a71297b6697ca92c329be0bc9f0515fe3a5a03ebd4488ba3d", ((msg) => {
            int loopLength = (msg.Length < scores.Count) ? msg.Length : scores.Count;
            for (int i = 0; i < loopLength; i++){
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void Play(){
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine(){
        bondFire.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < scores.Count; i++){
            scores[i].text = "";
        }

        isZooming = true;
        isTransitioning = true;

        yield return new WaitForSeconds(1f);

        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        loadingImage.GetComponent<Animator>().SetTrigger("FadeIn");
        spacebar.GetComponent<Animator>().SetTrigger("FadeIn");

        // Fake Loading Screen
        yield return new WaitForSeconds(5f); // Random delay for loading screen effect

        // After the scene is loaded, trigger fade-out animation
        loadingImage.GetComponent<Animator>().SetTrigger("FadeOut");
        spacebar.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("SampleScene");
    }

    public void Settings(){
        if (isTransitioning) return;
        mainMenuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void Quit(){
        if (isTransitioning) return;
        Application.Quit();
    }

    public void SetVolume(){
        float volume = audioSlider.value;
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }

    private void LoadVolume(){
        audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");

        SetVolume();
    }
}
