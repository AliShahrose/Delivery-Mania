using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject deliveryInfo;
    public AudioMixer audioMixer;
    public AudioSource click;
    [SerializeField] RectTransform fader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            click.Play();
            if (isPaused)
                resume();
            else
                pause();
        }
    }

    void pause()
    {
        pauseMenu.SetActive(true);
        deliveryInfo.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    void resume()
    {
        pauseMenu.SetActive(false);
        deliveryInfo.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }


    public void quit()
    {
        Time.timeScale = 1f;
        fader.gameObject.SetActive(true);
        pauseMenu.SetActive(false);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.5f).setEaseInOutQuad().setOnComplete(() => {
            Invoke("changeScene", 0.5f);
        });
    }

    private void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volumeMaster", volume);
    }

}
