using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager
{

    void RegisterSound(string soundName, AudioClip clip);
    void RegisterSceneMusic(string sceneName, AudioClip clip);
    void PlaySound(string soundName);
    void PlayMusic(string sceneName);
    void PlayMusic(AudioClip clip);
    void StopMusic();
    void SetVolume(float volume);
    void RestoreSceneMusic();
    float GetCrossfadeDuration();
    void CrossfadeMusic(string musicName);
    void CrossfadeMusic(AudioClip newClip);
    void PlayAttackSound();
    void PlayDeathSound();
    void PlayDamageSound();
    void PlayGameOverSound();

    //void RegisterSound(string soundName, AudioClip clip);
    //void RegisterSceneMusic(string sceneName, AudioClip clip);
    //void PlaySound(string soundName);
    //void PlayMusic(string sceneName);
    //void StopMusic();
    //void SetVolume(float volume);
    //void RestoreSceneMusic();
    //float GetCrossfadeDuration();
    //void CrossfadeMusic(string musicName);
    //void PlayAttackSound();
    //void PlayDeathSound();
    //void PlayDamageSound();
}

