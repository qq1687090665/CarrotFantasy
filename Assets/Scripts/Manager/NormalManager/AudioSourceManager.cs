using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责控制音乐的播放和停止以及游戏中各种音效的播放
/// </summary>
public class AudioSourceManager
{
    private AudioSource[] audioSource;//0.播放BGMusic 1.播放特效音
    private bool playEffectMusic = true;
    private bool playBGMusic = true;

    //构造函数
    public AudioSourceManager()
    {
        audioSource = GameManager.Instance.GetComponents<AudioSource>();
        LoadSettings();
    }

    //从PlayerPrefs加载音频设置
    private void LoadSettings()
    {
        playBGMusic = PlayerPrefs.GetInt("BGMusicOn", 1) == 1;
        playEffectMusic = PlayerPrefs.GetInt("EffectMusicOn", 1) == 1;
    }

    //保存音频设置到PlayerPrefs
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("BGMusicOn", playBGMusic ? 1 : 0);
        PlayerPrefs.SetInt("EffectMusicOn", playEffectMusic ? 1 : 0);
        PlayerPrefs.Save();
    }

    //获取当前BGM开关状态
    public bool IsBGMusicOn()
    {
        return playBGMusic;
    }

    //获取当前音效开关状态
    public bool IsEffectMusicOn()
    {
        return playEffectMusic;
    }

    //播放背景音乐
    public void PlayBGMusic(AudioClip audioClip)
    {
        if (!playBGMusic) return;
        if (!audioSource[0].isPlaying || audioSource[0].clip != audioClip)
        {
            audioSource[0].clip = audioClip;
            audioSource[0].Play();
        }
    }

    //播放音效
    public void PlayEffectMusic(AudioClip audioClip)
    {
        if (playEffectMusic)
        {
            audioSource[1].PlayOneShot(audioClip);
        }
    }

    public void CloseBGMusic()
    {
        audioSource[0].Stop();
    }

    public void OpenBGMusic()
    {
        if (playBGMusic && audioSource[0].clip != null)
        {
            audioSource[0].Play();
        }
    }

    public void CloseOrOpenBGMusic()
    {
        playBGMusic = !playBGMusic;
        SaveSettings();
        if (playBGMusic)
        {
            OpenBGMusic();
        }
        else
        {
            CloseBGMusic();
        }
    }

    public void CloseOrOpenEffectMusic()
    {
        playEffectMusic = !playEffectMusic;
        SaveSettings();
    }
    //按钮音效播放
    public void PlayButtonAudioClip()
    {
        PlayEffectMusic(GameManager.Instance.factoryManager.audioClipFactory.GetSingleResources("Main/Button"));
    }
    //翻书音效播放
    public void PlayPagingAudioClip()
    {
        PlayEffectMusic(GameManager.Instance.factoryManager.audioClipFactory.GetSingleResources("Main/Paging"));
    }

}
