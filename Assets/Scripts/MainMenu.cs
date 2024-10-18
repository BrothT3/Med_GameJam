using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

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
    private bool isZooming;
    private bool isTransitioning;

    private void Start(){
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
    }

    private void Update(){
        if (mainMenuMusic.volume < 1 && !isTransitioning){
            mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 1f, 1f * Time.deltaTime);
        } else if (mainMenuMusic.volume >= 0 && isTransitioning){
            mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 0f, 1.5f * Time.deltaTime);
        }

        if (isZooming){
            // Zoom in
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 0.01f, Time.deltaTime * 5);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(4.3f, 0.7f, 0), Time.deltaTime * 5);
        }
    }

    public void Play(){
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine(){
        bondFire.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        isZooming = true;
        isTransitioning = true;

        yield return new WaitForSeconds(1f);

        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        loadingImage.GetComponent<Animator>().SetTrigger("FadeIn");

        // Fake Loading Screen
        yield return new WaitForSeconds(Random.Range(1f, 2f)); // Random delay for loading screen effect

        // After the scene is loaded, trigger fade-out animation
        loadingImage.GetComponent<Animator>().SetTrigger("FadeOut");

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
