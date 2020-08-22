using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AudioLearn
{
    public class PlayableAudio : PlayableMono
    {

    }
    public enum eAudioPlayMode
    {
        //播放时接着上一次播放
        Add = 0,
        //播放时先停止上一次播放
        StopOther,
    }

    public enum eAudioTransfromMode
    {
        //播放时音源位置不移动，适用脚步声开枪
        Stay = 0,
        //播放时音源跟随父节点移动，适用换弹 子弹
        Follow,
    }


    [Serializable]
    public class AudioPlayConfig : PlayableConfig
    {
        public eAudioPlayMode _ePlay = eAudioPlayMode.Add;
        public eAudioTransfromMode _eTransform = eAudioTransfromMode.Follow;

        //播放声音时候需要跟随的父节点
        public Transform _parent;
        public bool _bLoop = false;
        [NonSerialized]
        public AudioClip _clip;
    }

    [Serializable]
    public class AudioStopConfig : PlayableStopConfig
    {
        //停止播放时，是否立即停止，是则立即停止，用于bgm，否则将loop设置为false，适用于开枪脚步
        public bool _bStopInmmediate = false;
    }

}


