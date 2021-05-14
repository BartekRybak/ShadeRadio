using System;
using System.Threading;
using NAudio;
using NAudio.Wave;

namespace ShadeRadio
{
    class Player
    {
        private string url;
        private WaveOutEvent waveOut;

        public Player(string url)
        {
            this.url = url;
            if(waveOut != null)
            {
                waveOut.Dispose();
            }
            
            waveOut = new WaveOutEvent();
            waveOut.Init(new MediaFoundationReader(url));
        }

        public void Play()
        {
            if(url != string.Empty)
            {
                waveOut.Play();
            }
        }

        public void Pause()
        {
            if (waveOut != null)
            {
                if (GetPlaybackState() == PlaybackState.Playing)
                {
                    waveOut.Pause();
                }
            }
        }

        public void Stop()
        {
            if(waveOut != null)
            {
                if (GetPlaybackState() == PlaybackState.Playing)
                {
                    waveOut.Stop();
                }
            }
        }

        public void SetVolume(int volume)
        {
            if((volume >= 0) && (volume <= 100))
            {
                waveOut.Volume = (float)volume / 100;
            }
            else
            {
                throw new Exception("Volume range is 0-100");
            }
        }

        public int GetVolume()
        {
            return Convert.ToInt32(waveOut.Volume * 100);
        }

        public PlaybackState GetPlaybackState()
        {
            return waveOut.PlaybackState;
        }
    }
}
