using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMgr : MonoBehaviour
{
    public Button Start_Btn;
    public Button Exit_Btn;

    public AudioClip button;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        if (Start_Btn != null)
            Start_Btn.onClick.AddListener(OnClickStartBtn);

        if (Exit_Btn != null)
            Exit_Btn.onClick.AddListener(OnClickExitBtn);
    }

    void OnClickStartBtn()
    {
        audioSource.PlayOneShot(button);
        Invoke("LoadGameScene", 0.5f);
    }


    void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    void OnClickExitBtn()
    {
        audioSource.PlayOneShot(button);
        Invoke("QuitApp", 0.5f);
    }

    void QuitApp()
    {
        Application.Quit();
    }
}
