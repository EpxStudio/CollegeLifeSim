using UnityEngine;
using System.Collections;

public class _GM : MonoBehaviour {

    public static _GM instance;
    public static GameObject player;
    public static Vector3 mouseLocation;
    public static bool leftClickSinglePress;
    public AudioClip backMusic;
    static bool playMusic = false;

    void Awake()
    {
        instance = this;
        if (backMusic != null)
        {
            transform.GetComponent<AudioSource>().clip = backMusic;
            transform.GetComponent<AudioSource>().volume = 0.7f;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playAudio();
    }

    void Update()
    {
        mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playAudio();
        }
    }

    void playAudio()
    {
        if (playMusic == false)
        {
            transform.GetComponent<AudioSource>().Play();
            playMusic = true;
        }
        else
        {
            transform.GetComponent<AudioSource>().Pause();
            playMusic = false;
        }
    }
}
