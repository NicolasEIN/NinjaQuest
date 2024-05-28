using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour, ISoundManager
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource sfxSource; // Fonte para efeitos sonoros
    public AudioSource musicSource; // Fonte para música de fundo

    [Header("Volume Settings")]
    [Range(0, 1)]
    public float volume = 1f; // Controle de volume

    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sceneMusicClips = new Dictionary<string, AudioClip>();
    private AudioClip currentSceneClip;

    // Adicione os seguintes campos serializados para os efeitos sonoros
    [Header("Sound Clips")]
    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip scene1MusicClip;
    [SerializeField] private AudioClip scene2MusicClip;
    [SerializeField] private AudioClip scene3MusicClip;
    [SerializeField] private AudioClip areaSoundClip;
    [SerializeField] public AudioClip gameOverClip;
    [SerializeField] private AudioClip attackSoundClip;
    [SerializeField] private AudioClip deathSoundClip;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip EndGameSoundClip;

    // Tempo de duração do crossfade
    public float crossfadeDuration = 1.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sfxSource.volume = volume;
        musicSource.volume = volume;
        InitializeSounds();
        ActivateAudioSources();
        PlayInitialMusic();
    }

    private void InitializeSounds()
    {
        RegisterSound("ButtonClick", buttonClickClip);
        RegisterSceneMusic("MainMenu", scene1MusicClip);
        RegisterSceneMusic("HomeTown", scene2MusicClip);
        RegisterSceneMusic("BudaDungeon", scene3MusicClip);
        RegisterSound("AreaSound", areaSoundClip);
        RegisterSound("GameOver", gameOverClip);
        RegisterSound("EndGame", EndGameSoundClip);
        // Adicione aqui os registros para novos sons
        RegisterSound("Attack", attackSoundClip);
        RegisterSound("Death", deathSoundClip);
        RegisterSound("Damage", damageSoundClip);
    }

    public void RegisterSound(string soundName, AudioClip clip)
    {
        if (clip != null)
        {
            soundClips.Add(soundName, clip);
        }
        else
        {
            Debug.LogWarning(soundName + " não está atribuído.");
        }
    }

    public void RegisterSceneMusic(string sceneName, AudioClip clip)
    {
        if (clip != null)
        {
            sceneMusicClips.Add(sceneName, clip);
        }
        else
        {
            Debug.LogWarning(sceneName + " não está atribuído.");
        }
    }

    private void ActivateAudioSources()
    {
        sfxSource.enabled = true;
        musicSource.enabled = true;
    }

    private void PlayInitialMusic()
    {
        PlayMusic(SceneManager.GetActiveScene().name);
    }

    public void PlaySound(string soundName)
    {
        if (soundClips.ContainsKey(soundName))
        {
            sfxSource.PlayOneShot(soundClips[soundName]);
        }
    }

    public void PlayMusic(string sceneName)
    {
        if (sceneMusicClips.ContainsKey(sceneName))
        {
            currentSceneClip = sceneMusicClips[sceneName];
            musicSource.clip = currentSceneClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        sfxSource.volume = volume;
        musicSource.volume = volume;
    }

    public void RestoreSceneMusic()
    {
        if (currentSceneClip != null)
        {
            musicSource.clip = currentSceneClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        currentSceneClip = clip;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }  

    public void CrossfadeMusic(string musicName)
    {
        if (musicSource.isPlaying)
        {
            StartCoroutine(CrossfadeCoroutine(musicName));
        }
        else
        {
            PlayMusic(musicName);
        }
    }

    public void CrossfadeMusic(AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            StartCoroutine(CrossfadeCoroutine(newClip));
        }
        else
        {
            PlayNewClip(newClip);
        }
    }

    private IEnumerator CrossfadeCoroutine(string musicName)
    {
        float increment = Time.deltaTime / crossfadeDuration;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= increment;
            yield return null;
        }

        PlayMusic(musicName);

        while (musicSource.volume < volume)
        {
            musicSource.volume += increment;
            yield return null;
        }
    }

    private IEnumerator CrossfadeCoroutine(AudioClip newClip)
    {
        float increment = Time.deltaTime / crossfadeDuration;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= increment;
            yield return null;
        }

        PlayNewClip(newClip);

        while (musicSource.volume < volume)
        {
            musicSource.volume += increment;
            yield return null;
        }
    }

    private void PlayNewClip(AudioClip newClip)
    {
        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public float GetCrossfadeDuration()
    {
        return crossfadeDuration;
    }

    public void PlayAttackSound()
    {
        PlaySound("Attack");
    }

    public void PlayDeathSound()
    {
        PlaySound("Death");
    }

    public void PlayDamageSound()
    {
        PlaySound("Damage");
    }

    public void PlayGameOverSound()
    {
        PlaySound("GameOver");
    }

    public void PlayEndGameSound()
    {
        PlaySound("EndGame");
    }


}