using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageFaderMenu : MonoBehaviour
{
    public static ImageFaderMenu Instance;
    [SerializeField] Color unTintedColor = Color.clear;
    [SerializeField] Color tintedColor = Color.black;
    [SerializeField] float speed = 0.5f;
    [SerializeField] Image image;
    [SerializeField] GameObject imagefade;

    void Awake()
    {
       /* if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // root, not child
        }
        else
        {
            Destroy(gameObject);
            return;
        }
*/
        if (image == null) image = GetComponent<Image>();
        imagefade.SetActive(false);
    }

    public void Tint(string mainScene, string addScene)
    {
        imagefade.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeOut(mainScene, addScene));
    }

    public IEnumerator FadeOut(string mainScene, string essentialScene)
    {
        float f = 0f;
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            image.color = Color.Lerp(unTintedColor, tintedColor, f);
            yield return null;
        }

        /*AsyncOperation loadMain = SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Single); 
        yield return loadMain; 

        AsyncOperation loadEss = SceneManager.LoadSceneAsync(essentialScene, LoadSceneMode.Additive); 
        yield return loadEss; */

        SceneManager.LoadScene(mainScene,LoadSceneMode.Single);
        SceneManager.LoadScene(essentialScene, LoadSceneMode.Additive);



    }

    public IEnumerator FadeIn()
    {
        float f = 0f;
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            image.color = Color.Lerp(tintedColor, unTintedColor, f);
            yield return null;
        }
        imagefade.SetActive(false);
    }
}
