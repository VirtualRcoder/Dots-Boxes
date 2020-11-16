using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text ProgressText;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsychronously(sceneIndex));
    }

    IEnumerator LoadAsychronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            ProgressText.text = progress * 100f + "%";

            yield return null;
        }
    }
    
    
        
}

