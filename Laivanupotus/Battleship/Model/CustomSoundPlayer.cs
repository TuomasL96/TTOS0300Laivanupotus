using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Battleship.Properties;
using NAudio.Wave;

namespace Battleship.Model
{
    class CustomSoundPlayer
    {
        private static bool soundsEnabled = true;

        public static void PlayExplosion()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.explosion);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static void PlayRestart()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.menu_button);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static void PlayMiss()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.miss);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static void PlayWin()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.victory);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static void PlayLose()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.lose);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static void PlaySunk()
        {
            if (soundsEnabled)
            {
                WaveFileReader wave = new WaveFileReader(Resources.shipdestroyed);
                DirectSoundOut output = new DirectSoundOut();
                output.Init(new WaveChannel32(wave));
                output.Play();
            }
        }

        public static bool GetSoundStatus()
        {
            if (soundsEnabled)
                return true;
            else { return false; }
        }

        public static void SetSound(bool enableSounds)
        {
            soundsEnabled = enableSounds;
        }
    }
}
