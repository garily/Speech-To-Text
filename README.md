# Speech-To-Text
<h3>Implementing Communication with Bluemix STT Engine</h3>

<p>This socket establishes communication with IBM Bluemix Watson's Speech-to-text engine via WebSocket-Sharp (WebSocket realized in C#) in Unity, which generally provides the user interface, as well as further possibilities for integrating this socket into other applications.</p>

<p>Note: My initial purpose for writing this socket was to use it in a VR Marionette project established by the Intersections Digital Studio of Emily Carr University of Art + Design, where meanwhile I was doing a Globalink&copy; Research internship funded under Mitacs&copy; and China Scholarship Council. Later the socket was rewritten via Max Cycling-74 (<a href="https://github.com/gl14916/Speech-to-Text_via_Max">source code on GitHub</a>) in a much more efficient way.</p>

<p>Languange preference in the socket: English (default).</p>

<p>Implementations: </p>
<ul>
    <li><p>WebSocket-Sharp:<br/>A C# implementation of the WebSocket protocol client and server</p>
    <p><a href="https://github.com/sta/websocket-sharp">https://github.com/sta/websocket-sharp</a></p></li>
    <li><p>Json.Net:<br>A JSON framework for .NET, used for parsing Json responses from Bluemix</p>
    <p><a href="https://github.com/JamesNK/Newtonsoft.Json">https://github.com/JamesNK/Newtonsoft.Json</a></p></li>
    <li><p>SavWav.cs by Calvin Rien:<br/>Script for saving audioclip as .wav files.</p>
    <p><a href="https://forum.unity3d.com/threads/writing-audiolistener-getoutputdata-to-wav-problem.119295/#post-806734">https://forum.unity3d.com/threads/writing-audiolistener-getoutputdata-to-wav-problem.119295/#post-806734</a></p></li>
</ul>

<h4>Instructions</h4>
<p><b>NOTICE</b> :To successfully establish communication with Bluemix STT, you'll need to have an account for Bluemix console and create an instance for STT, which assigns a credential username and password, and you'll need to use that for this socket</p>
<ol>
    <li>Download the complete repository</li>
    <li>Fill your credentials obtained from Bluemix STT instance in line 58 and 59 of Assets/Socket.cs</li>
    <li>Start the Assets/mic.unity scene</li>
    <li>Press "Record" and speak to the microphone</li>
    <li>Press "Stop" when finish</li>
    <li>Press "Save", which saves the recorded audio in D:\STT, and then communicates with Watson to get the result</li>
    <li>The result will be shown in the log of Unity, as well as written to D:\STT\rcv.txt</li>
</ol>
