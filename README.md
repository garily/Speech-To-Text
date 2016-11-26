# Speech-To-Text
###Implementing Communication with Bluemix STT Engine

This socket establishes communication with IBM Bluemix Watson's Speech-to-text engine via WebSocket-Sharp (WebSocket realized in C#) in Unity, which generally provides the user interface, as well as further possibilities for integrating this socket into other applications.

Note: My initial purpose for writing this socket was to use it in a VR Marionette project established by the Intersections Digital Studio of Emily Carr University of Art + Design, where meanwhile I was doing a Globalink&copy; Research internship funded under Mitacs&copy; and China Scholarship Council. Later the socket was rewritten via Max Cycling-74 ([source code on GitHub](https://github.com/gl14916/Speech-to-Text_via_Max) in a much more efficient way.

__Note__: Languange preference for STT conversion in the socket: English (default).

##Implementations:  
*     [WebSocket-Sharp](https://github.com/sta/websocket-sharp):  
    A C# implementation of the WebSocket protocol client and server  
*     [Json.Net](https://github.com/JamesNK/Newtonsoft.Json):  
    A widely used JSON framework for .NET by Newtonsoft, used for parsing Json responses from Bluemix  
*     [SavWav.cs by Calvin Rien](https://forum.unity3d.com/threads/writing-audiolistener-getoutputdata-to-wav-problem.119295/#post-806734):  
    3rd party script for saving audioclip as .wav files

####Instructions
__NOTICE__ :To successfully establish communication with Bluemix STT, you'll need to have an account for Bluemix console and create an instance for STT, which assigns a credential username and password, and you'll need to use that for this socket  

1.    Download the complete repository
2.    Fill your credentials obtained from Bluemix STT instance in line 58 and 59 of Assets/Socket.cs
3.    Start the Assets/mic.unity scene
4.    Press "Record" and speak to the microphone
5.    Press "Stop" when finish
6.    Press "Save", which saves the recorded audio in D:\STT, and then communicates with Watson to get the result
7.    The result will be shown in the log of Unity, as well as written to D:\STT\rcv.txt

