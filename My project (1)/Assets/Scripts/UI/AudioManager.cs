using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] bgmSounds, sfxSounds, ui_sounds;
    public AudioSource bgmSource, sfxSource, ui_Source;
    public Slider slider_BGM, slider_SFX, slider_UI;

    [Range(0,1)]
    public float volume_bgm;
    [Range(0, 1)]
    public float volume_sfx;
    [Range(0, 1)]
    public float volume_ui;

    private static AudioManager instance = null;

    public static AudioManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgmSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("BGM Sound Not Found!");
        }
        else
        {
            bgmSource.clip = s.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("BGM Sound Not Found!");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void PlayUIsound(string name)
    {
        Sound s = Array.Find(ui_sounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("BGM Sound Not Found!");
        }
        else
        {
            ui_Source.clip = s.clip;
            ui_Source.Play();
        }
    }
    private void Start()
    {
        PlayBGM("MainLobby");
    }

    private void Update()
    {
        if(slider_BGM != null && slider_SFX != null && slider_UI != null)
        {
            volume_bgm = slider_BGM.value;
            volume_sfx = slider_SFX.value;
            volume_ui = slider_UI.value;
        }
        //else if()
        //{

        //}
        else
        {

        }
    }

    public void ui_Sound_menuClick()
    {
        PlayUIsound("menu_click");
    }
    public void ui_Sound_menuMove()
    {
        PlayUIsound("menu_move");
    }
    public void ui_Sound_menuConfirm()
    {
        PlayUIsound("menu_confirm");
    }
    public void ui_Sound_menuTwinkle()
    {
        PlayUIsound("menu_twinkle");
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void ToggleuiSFX()
    {
        ui_Source.mute = !ui_Source.mute;
    }

    public void Volume_BGM()
    {
        bgmSource.volume = volume_bgm;
    }
    public void Volume_SFX()
    {
        sfxSource.volume = volume_sfx;
    }
    public void Volume_UI()
    {
        ui_Source.volume = volume_ui;
    }


}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

