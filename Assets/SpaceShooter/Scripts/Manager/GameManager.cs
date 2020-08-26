using UnityEngine;
using UnityEngine.UI;

public enum GAMESTATE
{
    UI = 0,
    GAMEPLAY = 1
}
public enum AUDIOTYPE
{
    BUTTON,
    PLAYERSHOT,
    EXPLOSION
}

/// <summary>
/// Scripts handle the game state and the sound.
/// </summary>

public class GameManager : MonoBehaviour
{
    #region SERIALIZE FIELDS

    [SerializeField] AudioClip buttonAudio;

    [SerializeField] AudioClip playerShootAudio;

    [SerializeField] AudioClip explosionAudio;

    #endregion

    #region PUBLIC FIELDS

    [HideInInspector] public bool isAudioOff = false;

    [HideInInspector]  public bool isNebulaTheme = false;

    public const string  highestScoreKey = "HighestScore";

    #endregion

    #region PRIVATE FIELDS

    static GameManager instance;

    AudioSource source;

    GAMESTATE currentGameState;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);

        source = GetComponent<AudioSource>();

    }

    #endregion

    public static GameManager GetInstance()
    {
        if(instance==null)
        {
            GameObject gameManager = new GameObject("GameManager");

            instance = gameManager.AddComponent<GameManager>();
        }

        return instance;
    }

    #region PUBLIC METHODS

    public GAMESTATE GetGameState()
    {
        return currentGameState;
    }

    // Set the game current states
    public void SetGameState(GAMESTATE state)
    {
        currentGameState = state;
    }

    // Plays common audios
    public void PlayAudio(AUDIOTYPE type,bool isLoop=false, float volume=1)
    {
        source.mute = isAudioOff;
        switch (type)
        {
            case AUDIOTYPE.BUTTON:
                source.clip = buttonAudio;
                break;

            case AUDIOTYPE.PLAYERSHOT:
                source.clip = playerShootAudio;
                break;

            case AUDIOTYPE.EXPLOSION:
                source.clip = explosionAudio;
                break;
        }

        source.volume = volume;
        source.loop = isLoop;
        source.Play();
    }
    #endregion
}
