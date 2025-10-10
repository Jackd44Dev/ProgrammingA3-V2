using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioClip menuMusic;
    public AudioClip[] gameplayMusicPlaylist;
    AudioSource audioSource;
    public bool currentlyInMenu = true;

    void Awake() // sets music manager to persist thru scenes (otherwise music would get cut off each time a new floor is loaded, which would be awful)
    {
        if (MusicManager.instance != this && MusicManager.instance == null) // only allow one copy of MusicManager.instance to exist
        { 
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void startNewGame() // loads up a song from the playlist when starting a run
    {
        currentlyInMenu = false;
        playSongFromPlaylist();
    }

    public void backToMenu() // loads in the menu song after a run
    {
        currentlyInMenu = true;
        playMenuMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying) // if a song has ended, load another one in from the playlist (unless at the menu, then this basically loops the menu song)
        {
            if (currentlyInMenu)
            {
                playMenuMusic();
            }
            else
            {
                playSongFromPlaylist();
            }
        }
    }

    void playSongFromPlaylist() // randomly select a track to play, almost like a jukebox... almost
    {
        if (audioSource.isPlaying) { audioSource.Stop(); }
        AudioClip selectedSong = gameplayMusicPlaylist[Random.Range(0, gameplayMusicPlaylist.Length)];
        audioSource.PlayOneShot(selectedSong);
    }

    void playMenuMusic()
    {
        if (audioSource.isPlaying) { audioSource.Stop(); }
        audioSource.PlayOneShot(menuMusic);
    }
}
