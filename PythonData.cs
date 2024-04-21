using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class PythonData : MonoBehaviour
{
    public int port = 12345;

    private TcpListener listener;

    private Vector3 wrist_position;
    private Vector3 elbow_position;

    public GameObject wrist;
    public GameObject elbow;

    void Start()
    {

        Debug.Log("hello im here");
        // Start listening for incoming connections on the specified port
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        // Start accepting incoming connections asynchronously
        listener.BeginAcceptTcpClient(HandleConnection, null);

        
    }

    private void Update()
    {
        elbow.transform.position = elbow_position;
        wrist.transform.position = wrist_position;
    }

    private void HandleConnection(IAsyncResult result)
    {
        // Accept the incoming connection
        TcpClient client = listener.EndAcceptTcpClient(result);
        NetworkStream stream = client.GetStream();

        // Read the incoming message
        byte[] buffer = new byte[2048];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string message = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);


        Debug.Log(message);
        // Process the received message
        string[] parts = message.Split(',');
        float leftWristX = float.Parse(parts[0]);
        float leftWristY = float.Parse(parts[1]);
        float leftWristZ = float.Parse(parts[2]);


        float leftElbowX = float.Parse(parts[3]);
        float leftElbowY = float.Parse(parts[4]);
        float leftElbowZ = float.Parse(parts[5]);
        // Do something with the received coordinates
        Debug.Log("Received Left Wrist coordinates: (" + leftElbowY + ", " + leftElbowY + ", " + leftElbowY + ")");

        // Update the position of the GameObject
        wrist_position = new Vector3(leftWristX, -leftWristY, leftWristZ);
        elbow_position = new Vector3(leftElbowX, -leftElbowY, leftElbowZ);
        //wrist.transform.position = new Vector3(leftWristX, -leftWristY, leftWristZ);
        //elbow.transform.position = new Vector3(leftElbowX,leftElbowY, leftElbowZ);
        // Close the connection
        stream.Close();
        client.Close();

        // Start listening for the next incoming connection
        listener.BeginAcceptTcpClient(HandleConnection, null);
    }

    void OnDestroy()
    {
        if (listener != null)
        {
            listener.Stop();
        }
    }
}
