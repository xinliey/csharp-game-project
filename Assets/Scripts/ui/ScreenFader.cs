using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance; // Singleton instance for easy access
    [SerializeField] public Color unTintedColor;
    [SerializeField] public Color tintedColor;
    public float speed = 0.5f;
    public Image image;    // Reference to the Image component
    public GameObject imagefade;
    float f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (image == null)
            image = GetComponent<Image>();
    }

    public void Tint()
    {
        imagefade.SetActive(true);
        StopAllCoroutines(); //to make sure there's no coroutine crash, it was doubling the process without this code
        f = 0f;
        StartCoroutine(FadeOut());
    }

    public void UnTint()
    {
       
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(FadeIn());
    }
    public IEnumerator FadeOut()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(unTintedColor, tintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();

        }
        //UnTint();
    }
    public IEnumerator FadeIn()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;
            yield return new WaitForEndOfFrame();

        }
    }
}
