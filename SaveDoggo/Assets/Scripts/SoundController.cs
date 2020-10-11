using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public PlayerController player;

    public AudioSource play;
    public AudioSource sfx;
    public AudioSource bgm;

    private Dictionary<string, AudioClip> sounds;


    public static SoundController SC;

    // Start is called before the first frame update
    void Awake()
    {
        SC = this;

        player = GetComponent<PlayerController>();

        play = GetComponents<AudioSource>()[0];
        play.loop = false;

        sfx = GetComponents<AudioSource>()[1];
        sfx.loop = false;

        bgm = GetComponents<AudioSource>()[2];
        bgm.volume = 0.5f;

        sounds = new Dictionary<string, AudioClip>();


        sounds.Add("CloseDoor", Resources.Load("SFX/Closing-Door_2", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogWalk", Resources.Load("SFX/Dog-Walking", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogSadLong", Resources.Load("SFX/Dog_Grieving_Long_02", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogSadShort1", Resources.Load("SFX/Dog_Grieving_Short", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogSadShort2", Resources.Load("SFX/Dog_Grieving_Short_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogShake", Resources.Load("SFX/Dog_ShakingBody", typeof(AudioClip)) as AudioClip);
        sounds.Add("KnockMetal1", Resources.Load("SFX/Knocking_Metal_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("KnockMetalHigh", Resources.Load("SFX/Knocking_Metal_HighPitch_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("KnockMetalLow", Resources.Load("SFX/Knocking_Metal_LowPitch_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("KnockWood1", Resources.Load("SFX/Knocking_Wood_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("KnockWood2", Resources.Load("SFX/Knocking_Wood_02", typeof(AudioClip)) as AudioClip);
        sounds.Add("MechRoom", Resources.Load("SFX/Mechanical Room", typeof(AudioClip)) as AudioClip);
        sounds.Add("OpenDoor1", Resources.Load("SFX/Opening-Door_1", typeof(AudioClip)) as AudioClip);
        sounds.Add("OpenDoor2", Resources.Load("SFX/Opening-Door_2", typeof(AudioClip)) as AudioClip);
        sounds.Add("RustleLong", Resources.Load("SFX/Rustling_Noise_Long_01", typeof(AudioClip)) as AudioClip);
        sounds.Add("RustleShort", Resources.Load("SFX/Rustling_Noise_Short_01", typeof(AudioClip)) as AudioClip);

        
        sounds.Add("DogConflict", Resources.Load("SFX/DogConfliction_mixdown", typeof(AudioClip)) as AudioClip);
        sounds.Add("Door", Resources.Load("SFX/Door_sound_mixdown_trimmed", typeof(AudioClip)) as AudioClip);
        sounds.Add("Beep", Resources.Load("SFX/Beep", typeof(AudioClip)) as AudioClip);
        sounds.Add("TimeWhir", Resources.Load("SFX/TimeMachine_01_mixdown", typeof(AudioClip)) as AudioClip);

        sounds.Add("BadBeep", Resources.Load("SFX/Negative_Beep_LowPitch_mixdown", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogGo", Resources.Load("SFX/Whistle_Short_Call_mixdown", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogCome", Resources.Load("SFX/Whistle_Long_Call_mixdown", typeof(AudioClip)) as AudioClip);
        sounds.Add("DogHappy", Resources.Load("SFX/Happy_Dog_Sound_mixdown", typeof(AudioClip)) as AudioClip);


    }

    public void Step(float vol)
    {
        AudioClip clip;
        //if (player.inWater)
        //{
        //    clip = footW[UnityEngine.Random.Range(0, footW.Length)];
        //    vol *= 0.6f;
        //}
        //else
        //{
        //    clip = footG[UnityEngine.Random.Range(0, footG.Length)];
        //}

        //play.PlayOneShot(clip, vol);
    }



    public void PlaySound(string soundID, float vol = 0.5f)
    {
        AudioClip clip = sounds[soundID];
        play.PlayOneShot(clip, vol);
    }

    public void PlaySfx(string soundID, float vol = 0.5f)
    {
        sfx.pitch = 1.0f;
        AudioClip clip = sounds[soundID];
        sfx.PlayOneShot(clip, vol);
    }

    public void StopBGM()
    {
        bgm.Pause();
    }

    public void PlayBGM()

    {
        bgm.Play();
    }

}
