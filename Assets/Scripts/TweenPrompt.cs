using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPrompt : MonoBehaviour
{
    public float tweenInTime;
    public float tweenOutTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tweenIn()
    {
        LeanTween.cancel(gameObject);

        if (gameObject.name.StartsWith("MessageDelivery"))
        {
            transform.localScale = Vector3.one;
            LeanTween.scale(gameObject, Vector3.one * 2f, tweenInTime).setLoopPingPong();
        }
        else
        {
            transform.localScale = Vector3.one * 0f;
            LeanTween.scale(gameObject, Vector3.one, tweenInTime).setEaseSpring();
        }

    }
    public void tweenOut()
    {
        LeanTween.cancel(gameObject);

        if (gameObject.name.StartsWith("MessageDelivery"))
        {
            LeanTween.scale(gameObject, Vector3.one * 0f, tweenOutTime).setEaseInBack();
        }
        else
        {
        LeanTween.scale(gameObject, Vector3.one, 0f);
        LeanTween.scale(gameObject, Vector3.one * 0f, tweenOutTime).setEaseInBack();
        }

        transform.localScale = Vector3.one;

    }
}
