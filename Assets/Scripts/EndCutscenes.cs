using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EndCutscenes : MonoBehaviour{

    [SerializeField] private GameObject dancingMan;
    [SerializeField] private GameObject sleepingMan;
    [SerializeField] private Animator blackScreen;
    [SerializeField] private Light2D globalLight;

    private bool isZooming;
    private bool isChanging;

    // Start is called before the first frame update
    private void Start(){
        StartCoroutine(EndCutscenesCoroutine());
    }

    private void Update(){
        if (isZooming){
            // Zoom in
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4f, Time.deltaTime * 1.5f);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0.5f, 0f, -10), Time.deltaTime * 1.5f);
        }

        if (isChanging){
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, 0.47f, Time.deltaTime * 1f);
        }
    }

    private IEnumerator EndCutscenesCoroutine(){
        yield return new WaitForSeconds(1f);

        isZooming = true;

        yield return new WaitForSeconds(2f);

        isChanging = true;

        yield return new WaitForSeconds(1f);

        dancingMan.GetComponent<Animator>().SetTrigger("Stop");

        yield return new WaitForSeconds(0.5f);

        sleepingMan.GetComponent<Animator>().SetTrigger("WakeUp");

        yield return new WaitForSeconds(3f);

        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }
}
