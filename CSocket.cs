using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CustomSocket
{
    public class CSocket
    {
        Socket socket;
        
        public CSocket(IPAddress address, int port)
        {
             InitSocket(address, port);
        }

        private void InitSocket(IPAddress address, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddr = address;
            IPEndPoint remoteEP = new IPEndPoint(ipAddr, port);
            socket.Bind(remoteEP);
        }

        public Socket Accept()
        {
            return socket.Accept();
        }

        public void Connect(IPAddress address, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(address, port);
            socket.Connect(endPoint);
        }

        public Boolean isConnected()
        {
            return socket.Connected;
        }

        private Object ReceiveObject()
        {
            byte[] messageSize = new byte[4];
            socket.Receive(messageSize, 0, 4, SocketFlags.None);
            int inputSize = BitConverter.ToInt32(messageSize, 0);
            byte[] bytes = new byte[inputSize];
            int totalReceived = 0;
            do
            {
                int received = socket.Receive(bytes, totalReceived, inputSize - totalReceived, SocketFlags.Partial);
                totalReceived += received;
            } while (totalReceived != inputSize);
            Object ob = GetDeserializedObject(bytes);
            return ob;
        }


        private Object GetDeserializedObject(byte[] b)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(b, 0, b.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object deserialized = binForm.Deserialize(memStream);
            return deserialized;
        }

        private void SendObject(Object toSendObject)
        {
            byte[] serializedObject = GetSerializedObject(toSendObject);
            int messageSize = serializedObject.Length;
            byte[] size = BitConverter.GetBytes(messageSize);
            socket.Send(size);
            socket.Send(serializedObject);

        }

        private byte[] GetSerializedObject(Object com)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, com);
            return ms.ToArray();
        }


    }
}
