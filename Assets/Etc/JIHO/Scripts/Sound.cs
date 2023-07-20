
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    [Header("�� ����")]
    public AudioClip audio;
    [Header("���� �̸�(������� �ش� �� �̸��̶� �����ϰ�)")]
    public string audioName;
}

public class Sound : MonoBehaviour
{
    public static Sound instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            audioDictionary = new Dictionary<string, AudioClip>();
            AudioDataInit();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioData[] audioDatas;

    public Dictionary<string, AudioClip> audioDictionary;

    [SerializeField] private AudioSource[] audioSources;

    
    private void AudioDataInit()
    {
        for (int i = 0; i < audioDatas.Length; i++) audioDictionary.Add(audioDatas[i].audioName, audioDatas[i].audio);
    }

    public void Play(AudioClip audioClip, bool _isBgm, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (_isBgm) // BGM ������� ���
        {

            if (audioSources[0].isPlaying)
                audioSources[0].Stop();

            audioSources[0].pitch = pitch;
            audioSources[0].clip = audioClip;
            audioSources[0].Play();
        }
        else // Effect ȿ���� ���
        {
            audioSources[1].pitch = pitch;
            audioSources[1].PlayOneShot(audioClip);

            if (audioSources[1].isPlaying)
            {
                audioSources[2].pitch = pitch;
                audioSources[2].PlayOneShot(audioClip);
            }
            
        }
    }


}
