using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("The audio source used to play SFXs.")] public AudioSource _sfxSource;
    [Tooltip("The audio source used to play ambiant sounds.")] public AudioSource _ambiantSource; // it's no use
    [Tooltip("The audio source used to play music.")] public AudioSource _musicSource;

    [SerializeField] private AudioClip _theOneAndOnlyMusic;
    [SerializeField] private bool _hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment;

    // Singleton
    #region Singleton
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Player main");
                _instance = go.AddComponent<SoundManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        if (!_hasToPlayTheOneAndOnlyMusicBecauseItsTooMuchAUnMoment) return;
        PlayMusic(_theOneAndOnlyMusic);
    }

    public void PlaySound(AudioClip audioClip, bool isPitchRandom = false)
    {
        _sfxSource.pitch = (!isPitchRandom) ? 1 : 1 + Random.Range(-0.2f, 0.2f);
        _sfxSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip music)
    {
        _musicSource.clip = music;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
