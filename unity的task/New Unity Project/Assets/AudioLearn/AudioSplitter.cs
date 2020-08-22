using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioLearn
{
    public class AudioSplitter
    {
        public AudioClip _srcClip;
        public ClipConfig[] _configs = new ClipConfig[] { };
        public ClipData _clips;

        public void DoSplit()
        {
            _clips = new ClipData();

            int len = _configs.Length;
            if (len < 0)
            {
                return;
            }

            float[] datas = new float[_srcClip.samples];
            bool b = _srcClip.GetData(datas, 0);
            if(!b)
            {
                Debug.LogError("can not get data from" + _srcClip.name);
                return;
            }

            for(int i=0; i<len; ++i)
            {
                var con = _configs[i];
                if(con._sIndex<=0 && con._eIndex<=0)
                {
                    con._sIndex = (int)(_srcClip.frequency * con._fStartTime);
                    con._eIndex = (int)(_srcClip.frequency * con._fEndTime);
                }
                //int clipLen = AudioClip.Create(con._name,clipLen,)
            }
        }


        [Serializable]
        public class ClipConfig
        {
            public string _name;
            public eAudioType _Type;
            public int _sIndex;
            public int _eIndex;

            public float _fStartTime;
            public float _fEndTime;

        }

    }
    public enum eAudioType
    {
        PlayOnce = 0,
        PlayLoop,
        PlayOnceRandom,

    }


    [Serializable]
    public class ClipData
    {
        public List<eAudioType> _vType = new List<eAudioType>();
        public List<SingleTypeClip> _vvClip = new List<SingleTypeClip>();
    }


    [Serializable]
    public class SingleTypeClip
    {
        public List<AudioClip> _vClip = new List<AudioClip>();
    }



}
