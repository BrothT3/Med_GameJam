using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour{

    [Header("Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject settingsScreen;

    [Header("References")]
    [SerializeField] private Animator blackFade;
    [SerializeField] private GameObject loadingImage;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioSource mainMenuMusic;
    private bool isTransitioning;

    private void Start(){
        if (PlayerPrefs.HasKey("AudioVolume")){
            LoadVolume();
        } else {
            SetVolume();
        }
    }

    private void Update(){
        if (mainMenuMusic != null){
            if (mainMenuMusic.volume < 1 && !isTransitioning){
                mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 1f, 1f * Time.deltaTime);
            } else if (mainMenuMusic.volume >= 0 && isTransitioning){
                mainMenuMusic.volume = Mathf.MoveTowards(mainMenuMusic.volume, 0f, 1.5f * Time.deltaTime);
            }
        }
    }

    public void Play(){
        isTransitioning = true;
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine(){
        blackFade.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        // Trigger fade-in for the loading image
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
