using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class LoadingData
{
    public static int sceneToLoad;
}

public class Loader : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    void Start()
    {
        int index = LoadingData.sceneToLoad;
        StartCoroutine(StartLoad(index));
    }

    IEnumerator StartLoad(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            int intProgress = (int)(progress * 100);
            text.text = intProgress.ToString() + "%";
            yield return null;
        }
    }
}


