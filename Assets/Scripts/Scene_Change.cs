using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    public string sceneName;

    public GameObject fadeIn;
    public GameObject fadeOut;

    private void Start()
    {
        fadeOut.SetActive(true);
    }
    public void LoadDesiredScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void SceneChange()
    {
        StartCoroutine(fadeInCoroutine());
    }

    IEnumerator fadeInCoroutine()
    {
        fadeIn.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        LoadDesiredScene();
    }
}
