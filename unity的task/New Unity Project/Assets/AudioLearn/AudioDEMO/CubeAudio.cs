using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeAudio : MonoBehaviour
{

    public AudioSource m_audioSource;
    public AudioClip m_clip_foot;
    public AudioClip m_clip_shoot;
    // Start is called before the first frame update
    public CubeAudio()
    {

    }
    void Start()
    {
        m_audioSource = gameObject.AddComponent<AudioSource>();
        m_clip_foot = AssetDatabase.LoadAssetAtPath<AudioClip>(@"Assets\AudioLearn\AudioDEMO\Bloody punch_first.wav");
        m_clip_shoot = AssetDatabase.LoadAssetAtPath<AudioClip>(@"Assets\AudioLearn\AudioDEMO\Thunder strikes 30 second- Loop_second.wav");

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))
        {
            m_audioSource.clip = m_clip_foot;
            m_audioSource.Play();
        }
        if (Input.GetMouseButtonDown(0))
        {
            m_audioSource.clip = m_clip_shoot;
            m_audioSource.Play();
        }
   
    }
}
