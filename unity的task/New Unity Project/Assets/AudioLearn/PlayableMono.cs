using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Playables;

namespace AudioLearn
{


    public class PlayableMono : MonoBehaviour
    {
#if UNITY_EDITOR
        public PlayableMono _src = null;
#endif
        // Start is called before the first frame update

        virtual protected void Start()
        {

        }

        virtual public void Play(PlayableConfig config)
        {
            _Play(config);
        }

        virtual public void _Play(PlayableConfig config)
        {

        }

        virtual public void Stop(PlayableStopConfig config)
        {
            _Stop(config);
        }

        virtual public void _Stop(PlayableStopConfig config)
        {

        }

        virtual public void Refresh(PlayableMono src)
        {

        }
    }

    public class PlayableConfig
    {

    }

    public class PlayableStopConfig
    {
    }
    
    
}
