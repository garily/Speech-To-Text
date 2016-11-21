using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Socket : MonoBehaviour
{

    //public static string audPath = @"D:\Downloads\myfile.wav";
    public static string txtPath = @"D:\STT\rcv.txt";



    [Serializable]
    public class openingConfMsg
    {
        public string action;
        public string contentXtype;
        public bool continuous;
        public bool interim_results;
    }

    public class closingConfMsg
    {
        public string action;
    }

    public class Alternative
    {
        public double confidence { get; set; }
        public string transcript { get; set; }
    }

    public class Result
    {
        public List<Alternative> alternatives { get; set; }
        public bool final { get; set; }
    }

    public class STTResult
    {
        public List<Result> results { get; set; }
        public int result_index { get; set; }
    }




    public static void stt(string audPath)
    {
        string wsURI = "wss://stream.watsonplatform.net/speech-to-text/api/v1/recognize";
        string username = ""; //credentials from Watson's Speech-to-text console
        string password = ""; //credentials from Watson's Speech-to-text console



        FileInfo audSend = new FileInfo(audPath);

        FileInfo txtRcv = new FileInfo(txtPath);
        if (!txtRcv.Exists)
        {
            txtRcv.CreateText();
        }



        using (var ws = new WebSocket(wsURI))
        {

            int a = 1;


            ws.SetCredentials(username, password, true);


            ws.OnOpen += (sender, e) =>
            {
                //string message = "{\"action\": \"start\", \"content-type\": \"audio/wav\", \"continuous\": true, \"interim_results\": true}";
                openingConfMsg openingMsg = new openingConfMsg();
                openingMsg.action = "start";
                openingMsg.contentXtype = "audio/wav";
                openingMsg.continuous = false;
                openingMsg.interim_results = false;

                string confmsg = JsonConvert.SerializeObject(openingMsg).Replace("X", "-");
                ws.Send(confmsg);
            };

            ws.OnOpen += (sender, e) =>
            {

                //sending audio clip
                ws.Send(audSend);


                /*FileStream fs = File.OpenRead(audPath);
                byte[] b = new byte[1024];
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    ws.Send(b);
                }*/
            };

            ws.OnOpen += (sender, e) =>
            {

                //message = "{\"action\": \"stop\"}";
                closingConfMsg closingMsg = new closingConfMsg();
                closingMsg.action = "stop";

                string confmsg = JsonConvert.SerializeObject(closingMsg);
                ws.Send(confmsg);

            };




            ws.OnMessage += (sender, e) =>
            {
                if (e.IsText)
                {
                    var sttresult = JsonConvert.DeserializeObject<STTResult>(e.Data);

                    foreach (var result in sttresult.results)
                    {
                        foreach (var alternative in result.alternatives)
                        {

                            Debug.Log("Result : " + alternative.transcript);
                            if (alternative.transcript != null)
                            {
                                StreamWriter tmp = txtRcv.AppendText();
                                tmp.Write(alternative.transcript);
                                tmp.Write("\n");
                                Debug.Log("Written to: " + txtPath);
                                tmp.Close();
                            }
                        }
                    }
                }
            };

            ws.OnClose += (sender, e) =>
            {
                ws.Close();
                if (a-- > 0) ws.Connect();

            };

            ws.Connect();

        }
    }

}
