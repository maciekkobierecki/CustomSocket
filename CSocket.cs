using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomSocket
{
    public class CSocket
    {
        public const String LISTENING_FUNCTION = "listeningFunction";
        public const String CONNECT_FUNCTION = "connectFunction";
        public const String ACK_FUNCTION = "ackFunction";
        public const String NACK_FUNCTION = "nackFunction";
        Socket socket;

        public CSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public CSocket(IPAddress address, int port, String function)
        {
            InitSocket(address, port, function);
        }


        private void InitSocket(IPAddress address, int port, String function)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(address, port);
            if (function.Equals(LISTENING_FUNCTION))
                socket.Bind(remoteEP);
            else if (function.Equals(CONNECT_FUNCTION))
                socket.Connect(remoteEP);
            else
                throw new SocketException();
        }

        public CSocket(Socket socket)
        {
            this.socket = socket;
        }

        public CSocket Accept()
        {
            return new CSocket(socket.Accept());
        }

        public void Listen()
        {
            socket.Listen(0);
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

        public void SendACK()
        {
            SendObject(ACK_FUNCTION, new object());
        }

        public void SendNACK()
        {
            SendObject(NACK_FUNCTION, new object());
        }

        public Tuple<String, Object> ReceiveObject()
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
            Wrapper wrappedReceivedObject = (Wrapper)ob;
            Tuple<String, Object> tuple = new Tuple<string, object>(wrappedReceivedObject.getFunction(), wrappedReceivedObject.getObject());
            return tuple;
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

        public void SendObject(String function, Object toSendObject)
        {
            Wrapper wrapper = new Wrapper(function, toSendObject);
            byte[] serializedObject = GetSerializedObject(wrapper);
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

        public void Close()
        {
            socket.Close();
        }
    }
}
