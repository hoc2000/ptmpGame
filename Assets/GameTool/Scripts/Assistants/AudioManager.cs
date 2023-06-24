using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Berry.Utils;
namespace My.Tool
{
    [RequireComponent(typeof(AudioListener))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {

        AudioSource music;
        AudioSource sfx;
        //public float musicVolume
        //{
        //    get
        //    {
        //        //   if (!PlayerPrefs.HasKey("Music Volume"))
        //        //        return 0.5f;
        //        return PlayerPrefs.GetFloat("Music Volume", 1f);
        //    }
        //    set
        //    {
        //        PlayerPrefs.SetFloat("Music Volume", value);
        //    }
        //}
        public float musicVolume;
        public float sfxVolume;
        public bool IsMute
        {
            get
            {
                return PlayerPrefs.GetInt("IsMute", 0) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("IsMute", value ? 1 : 0);
            }
        }

        //public float sfxVolume
        //{
        //    get
        //    {
        //        //   if (!PlayerPrefs.HasKey("SFX Volume"))
        //        //          return 1f;
        //        return PlayerPrefs.GetFloat("SFX Volume", 1f);
        //    }
        //    set
        //    {
        //        PlayerPrefs.SetFloat("SFX Volume", value);
        //    }
        //}

        public List<MusicTrack> tracks = new List<MusicTrack>();
        public List<Sound> sounds = new List<Sound>();
        Sound GetSoundByName(string name)
        {
            return sounds.Find(x => x.name == name);
        }

        static List<string> mixBuffer = new List<string>();
        static float mixBufferClearDelay = 0.05f;


        private float music_volume_max = 0.3f;

        internal string currentTrack;


        public override void Awake()
        {

            sfx = GetComponent<AudioSource>(); ;
            music = gameObject.AddComponent<AudioSource>();

            base.Awake();
        }
        private void Start()
        {

            Gamedata.I.RegisterSaveData();
            SaveGameManager.I.Load();
            // Initialize
            //sfxVolume = 1f;
            //musicVolume = 1f;
            //        Debug.Log(sfxVolume + "" + musicVolume);
            MusicOn();
            EffectOn();

            AudioListener.pause = IsMute;

            StartCoroutine(MixBufferRoutine());
            //Invoke("Init", 1f);


        }
        void Init()
        {
            MusicOn();
            EffectOn();

            AudioListener.pause = IsMute;

            StartCoroutine(MixBufferRoutine());
        }
        // Coroutine responsible for limiting the frequency of playing sounds
        IEnumerator MixBufferRoutine()
        {
            float time = 0;

            while (true)
            {
                time += Time.unscaledDeltaTime;
                yield return 0;
                if (time >= mixBufferClearDelay)
                {
                    mixBuffer.Clear();
                    time = 0;
                }
            }
        }

        // Launching a music track
        int idMusic;
        public void PlayMusic(string trackName = null)
        {
            if (trackName == null)
            {
                idMusic = Random.Range(0, 4);
                trackName = "BG" + idMusic.ToString();
            }
            if (trackName != "")
                currentTrack = trackName;
            AudioClip to = null;
            foreach (MusicTrack track in tracks)
                if (track.name == trackName)
                    to = track.track;
            StartCoroutine(CrossFade(to));
        }

        // A smooth transition from one to another music
        IEnumerator CrossFade(AudioClip to)
        {
            float delay = 0.3f;
            if (music.clip != null)
            {
                while (delay > 0)
                {
                    music.volume = delay * musicVolume * music_volume_max;
                    delay -= Time.unscaledDeltaTime;
                    yield return 0;
                }
            }
            music.clip = to;
            if (to == null)
            {
                music.Stop();
                yield break;
            }
            delay = 0;
            if (!music.isPlaying) music.Play();
            while (delay < 0.3f)
            {
                music.volume = delay * musicVolume * music_volume_max;
                delay += Time.unscaledDeltaTime;
                yield return 0;
            }
            music.volume = musicVolume * music_volume_max;
            music.loop = true;

            StartCoroutine(ChangeMusic());
        }

        // A single sound effect
        public void Shot(string clip)
        {
            Sound sound = GetSoundByName(clip);
            if (sound != null && !mixBuffer.Contains(clip))
            {
                if (sound.clips.Count == 0) return;
                mixBuffer.Add(clip);
                sfx.pitch = 1f;
                sfx.PlayOneShot(sound.clips[UnityEngine.Random.Range(0, sound.clips.Count)]); ;//.GetRandom());

            }
        }
        public float pitch = 1f;
        public void ShotPitch(string clip, float pitch)
        {
            Sound sound = GetSoundByName(clip);
            if (sound != null && !mixBuffer.Contains(clip))
            {
                if (sound.clips.Count == 0) return;
                mixBuffer.Add(clip);
                sfx.pitch = pitch;
                sfx.PlayOneShot(sound.clips[0]);//.GetRandom());

            }
        }
        // Turn on/off music
        public void MusicOn()
        {
            if (Gamedata.I.Music)
            {
                ChangeMusicVolume(1f);

            }
            else
            {
                ChangeMusicVolume(0f);
            }


        }

        public void EffectOn()
        {
            if (Gamedata.I.Sound)
            {
                ChangeSFXVolume(1f);

            }
            else
            {
                ChangeSFXVolume(0f);
            }

        }

        public void ChangeMusicVolume(float v)
        {
            musicVolume = v;
            music.volume = musicVolume * music_volume_max;
        }

        public void Mute(bool value)
        {
            AudioListener.pause = value;
            IsMute = value;
        }

        public void ChangeSFXVolume(float v)
        {
            sfxVolume = v;
            sfx.volume = sfxVolume;
        }

        [System.Serializable]
        public class MusicTrack
        {
            public string name;
            public AudioClip track;
        }

        [System.Serializable]
        public class Sound
        {
            public string name;
            public List<AudioClip> clips = new List<AudioClip>();
        }

        IEnumerator ChangeMusic()
        {
            switch (idMusic)
            {
                case 0:
                    yield return new WaitForSeconds(71f);
                    PlayMusic();
                    break;
                case 1:
                    yield return new WaitForSeconds(77f);
                    PlayMusic();
                    break;
                case 2:
                    yield return new WaitForSeconds(88f);
                    PlayMusic();
                    break;
                case 3:
                    yield return new WaitForSeconds(70f);
                    PlayMusic();
                    break;
            }
        }
    }
}