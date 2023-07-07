using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>, IReadData
{
    Dictionary<AudioName, AudioData> _audioDict = new Dictionary<AudioName, AudioData>();
    bool _isOpenMusic, _isOpenSound;

    public bool IsOpenSound => _isOpenSound;
    public bool IsOpenMusic => _isOpenMusic;

    private void Awake()
    {
        _audioDict = new Dictionary<AudioName, AudioData>();
        foreach (var audio in AudioStorage.Instance.Audios)
        {
            GameObject obj = new GameObject($"{audio.name}");
            obj.transform.SetParent(transform);
            _audioDict[audio.name] = new AudioData(audio, obj.AddComponent<AudioSource>());
        }
    }

    public void LoadData()
    {
        _isOpenMusic = Convert.ToBoolean(PlayerPrefs.GetInt($"{GlobalKey.Audio.Music}", 1));
        _isOpenSound = Convert.ToBoolean(PlayerPrefs.GetInt($"{GlobalKey.Audio.Sound}", 1));
    }

    public void Music()
    {
        _isOpenMusic = !_isOpenMusic;
        foreach (var name in _audioDict.Keys)
        {
            if (_audioDict[name].Audio.type == AudioType.Music)
                _audioDict[name].Source.mute = !_isOpenMusic;
        }
        PlayerPrefs.SetInt($"{GlobalKey.Audio.Music}", Convert.ToInt32(_isOpenMusic));
    }

    public void Sound()
    {
        _isOpenSound = !_isOpenSound;
        foreach (var name in _audioDict.Keys)
        {
            if (_audioDict[name].Audio.type == AudioType.Sound)
                _audioDict[name].Source.mute = !_isOpenSound;
        }
        PlayerPrefs.SetInt($"{GlobalKey.Audio.Sound}", Convert.ToInt32(_isOpenSound));
    }

    private void PlayAudio(AudioName name, float volume = 1)
    {
        AudioData audioData = _audioDict[name];
        bool mute = (audioData.Audio.type == AudioType.Music) ? !_isOpenMusic : !_isOpenSound;
        audioData.Play(mute, volume);
    }

    private void PlayAudioOnceShot(AudioName name, float volume = 1)
    {
        AudioData audioData = _audioDict[name];
        bool mute = (audioData.Audio.type == AudioType.Music) ? !_isOpenMusic : !_isOpenSound;
        audioData.PlayOnceShot(mute, volume);
    }

    private void PauseAudio(AudioName name)
    {
        _audioDict[name].Pause();
    }

    #region public function audio
    public void PlaySoundClickButton()
    {
        PlayAudioOnceShot(AudioName.Click);
    }
    public void PlaySoundConnectButton()
    {
        PlayAudioOnceShot(AudioName.Connect);
    }
    public void PlaySoundConnectFailButton()
    {
        PlayAudioOnceShot(AudioName.ConnectFail);
    }
    public void PlaySoundHintButton()
    {
        PlayAudioOnceShot(AudioName.Hint);
    }
    public void PlaySoundWinButton()
    {
        PlayAudioOnceShot(AudioName.Win);
    }
    public void PlaySoundLoseButton()
    {
        PlayAudioOnceShot(AudioName.Lose);
    }
    #endregion
}
