using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace AudioLearn
{
    public class XAudioPlayer : MonoBehaviour
    {
        //Unity会自动为Public变量做序列化，序列化的意思是说再次读取Unity时序列化的变量是有值的，
        //不需要你再次去赋值，因为它已经被保存下来。什么样的值会被显示在面板上？已经被序列化
        //但是没有用HideInInspector标记的值。[HideInInspector] 表示将原本显示在面板上的序列化值隐藏起来。
        [HideInInspector]
        //public AudioSplitter _splitter;


        public ClipData _clips;

        //使用的播放器的名字
        public string _strAudioSOurceName = "bg";
        public AudioPlayConfig _audioPlayConfig = new AudioPlayConfig();
        public AudioStopConfig _audioStopConfig = new AudioStopConfig();

        //对应的——clips中的_vType
        public eAudioType _eplayType = eAudioType.PlayOnce;

        private float m_fStartTime = 0f;
        public float _fRandomDwltaTime = 0f;
        private bool m_bAutoPalying = false;

        //如果一直拿着 可能出现被其他音频同时引用的情况
        public PlayableAudio _lastPlay = null;
        private AudioClip m_lastClip = null;

        //for test
        public bool _testPlay = false;
        public bool _testStop = false;

        private void Start()
        {
            if(null == _audioPlayConfig._parent)
            {
                _audioPlayConfig._parent = transform;
            }
        }


    }
}
