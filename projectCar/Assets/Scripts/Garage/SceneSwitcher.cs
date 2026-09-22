using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float speed = 1f;

    private void Awake()
    {
        canvasGroup.alpha = 1f;
    }
    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        while(canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= speed*Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
    IEnumerator FadeOut(string scene)
    {
        while(canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += speed*Time.unscaledDeltaTime;
            yield return null;
        }
        //Debug.Log("fadeOut");
        SceneManager.LoadScene(scene);
    }
    public void SceneLoader(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    public void Quit()
    {
        Application.Quit();
    }
}
