using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudnessDetectionMicrophone : MonoBehaviour
{
    /// <summary>
    /// This script will take an audio clip and a position in the clip and return the average loudness of the sample
    /// </summary>
    
    //how much of the clip do you want to sample
    public int sampleWindow = 64;
    
    //we can store out audio clip here from our microphone
    private AudioClip _microphoneClip;

    private void Start()
    {
        //this just runs our method to get the microphone input
        MicrophoneToAudioClip();
    }

    //this method gets the microphone input and converts it to an audio clip
    public void MicrophoneToAudioClip()
    {
        //get the first microphone connected to the computer
        //we can get the name of the microphone from the Microphone.devices array
        string device = Microphone.devices[0];
        
        //now we need to start recording from the microphone
        //this method takes three parameters, the first is the name of the microphone, the second is whether or not to loop the recording, the third is the length of the recording and the last one is the sample rate
        //this method works like this public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
        _microphoneClip = Microphone.Start(device,true, 20, AudioSettings.outputSampleRate);
        
    }
    
    //this takes the previous method we wrote and passes in our microphone data!
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), _microphoneClip);
    }

    // this takes two parameters, one is the audio clip you want to sample from, the other is the position in the clip
    public float GetLoudnessFromAudioClip(int clipPos, AudioClip clip)
    {
        //this is the position in the clip where you want to start sampling
        int startPos = clipPos - sampleWindow;
        
        //if the position is less than 0, we can't sample from that position, so we return nothing!
        if(startPos < 0)
            return 0;
        
        //this is the array that will hold the waveform data of our sample
        float[] waveData = new float[sampleWindow];
        
        //.GetData returns the waveform data of the clip
        //how it works is public bool GetData(float[] data, int offsetSamples)
        clip.GetData(waveData, startPos);
        
        //now we can compute the loudness of the sample
        float totalLoudness = 0;
        
        //this for loop will add up the absolute value of each sample in the sample window
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        
        //now we can return the average loudness of the sample
        return totalLoudness / sampleWindow;
        
    }
}
