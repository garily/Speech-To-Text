using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(AudioSource))]

public class Mic : MonoBehaviour
{
    private AudioSource aud;// = new AudioSource();
    private bool micConnected = false;
    private int minFreq;
    private int maxFreq;
    // Use this for initialization
    void Start()
    {
        //Debug.Log(System.Environment.Version);
        aud = this.GetComponent<AudioSource>();
        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
        if (Microphone.devices.Length <= 0)
        {
            //Throw a warning message at the console if there isn't  
            Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present  
        {

            micConnected = true;
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
            if (minFreq == 0 && maxFreq == 0)
            {
                //...meaning 44100 Hz can be used as the recording sampling rate  
                maxFreq = 44100;
            }
        }


    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 50), "\n\n\n\n\n\nmaxFreq = " + maxFreq + "\nminFreq = " + minFreq + "\nmicConnected = " + micConnected);
        //If there is a microphone  
        if (micConnected)
        {
            if (!Microphone.IsRecording(null))
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Record"))
                {
                    aud.clip = Microphone.Start(null, true, 10, maxFreq);
                }
            }
            else //Recording is in progress  
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Stop and Play!"))
                {
                    Microphone.End(null); //Stop the audio recording  
                    aud.Play(); //Playback the recorded audio  
                }


                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 50), "Recording in progress...");
            }
        }
        else
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Microphone not connected!");
        }
        if (GUI.Button(new Rect(Screen.width / 2 + 105, Screen.height / 2 - 25, 200, 50), "Save"))
        {
            if (File.Exists(@"D:\STT\myfile.wav"))
                File.Delete(@"D:\STT\myfile.wav");
            SavWav.Save(@"D:\STT\myfile.wav", aud.clip);
            Socket.stt(@"D:\STT\myfile.wav");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //aud.Play();

    }
}
