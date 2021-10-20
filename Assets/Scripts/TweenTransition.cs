using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TweenTransition : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    // Start is called before the first frame update
    void Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1,1,1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEaseInOutQuad().setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }

    public void makeTransition()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.5f).setEaseInOutQuad().setOnComplete(() => {
            Invoke("changeScene", 0.5f);
        });
    }

    private void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
