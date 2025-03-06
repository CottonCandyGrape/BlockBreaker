using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    Dictionary<string, AudioClip> adClipDict = new Dictionary<string, AudioClip>();

    AudioSource[] sfxSrcList = new AudioSource[5];
    const int MaxSfxCnt = 5;
    int sfxCnt = 0;

    public static SoundMgr Inst = null;

    void Awake()
    {
        Inst = this;
        SetSfxSrc();
        SetClipDict();
    }

    void Start()
    {

    }

    void SetClipDict()
    {
        AudioClip audioClip = null;
        object[] temp = Resources.LoadAll("Sounds");
        for (int i = 0; i < temp.Length; i++)
        {
            audioClip = temp[i] as AudioClip;
            if (adClipDict.ContainsKey(audioClip.name))
                continue;
            adClipDict.Add(audioClip.name, audioClip);
        }
    }

    void SetSfxSrc()
    {
        for (int i = 0; i < MaxSfxCnt; i++)
        {
            GameObject newSoundObj = new GameObject();
            newSoundObj.transform.SetParent(this.transform);
            newSoundObj.transform.localPosition = Vector3.zero;
            AudioSource a_AudioSrc = newSoundObj.AddComponent<AudioSource>();
            a_AudioSrc.playOnAwake = false;
            a_AudioSrc.loop = false;
            newSoundObj.name = "SoundEffObj";

            sfxSrcList[i] = a_AudioSrc;
        }
    }

    public void PlaySfxSound(string fileName)
    {
        AudioClip clip = null;
        if (adClipDict.ContainsKey(fileName))
            clip = adClipDict[fileName];

        if (clip == null)
        {
            Debug.Log("Clip is null");
            return;
        }

        if (sfxSrcList[sfxCnt] != null)
        {
            sfxSrcList[sfxCnt].volume = 1.0f;
            sfxSrcList[sfxCnt].PlayOneShot(clip, 1.0f);
        }

        sfxCnt++;
        if (MaxSfxCnt <= sfxCnt) sfxCnt = 0;
    }
}
