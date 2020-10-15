using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ClassLibrary;

namespace ConsoleApp
{
    class TCPService
    {
        private TcpClient _tcpClient;

        public TCPService(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }
        
        internal void SendReceiveData()
        {
            Stream stream = _tcpClient.GetStream();
            StreamReader streamReader = new StreamReader(stream);
            StreamWriter streamWriter = new StreamWriter(stream)
            {
                AutoFlush = true
            };

            string message = streamReader.ReadLine();
            string answer = String.Empty;

            while (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Client: " + message);

                string[] messageStrings = ConvertMessageToArray(message);

                switch (messageStrings[0])
                {
                    case "get":
                        answer = JsonSerializer.Serialize(GetBook(messageStrings[1]));
                        break;
                    case "getAll":
                        answer = JsonSerializer.Serialize(GetBooks());
                        break;
                    case "save":
                        answer = "Saved";
                        SaveBook(JsonSerializer.Deserialize<Book>(messageStrings[1]));
                        break;
                    default:
                        answer = "Invalid Request";
                        break;
                }

                try
                {
                    streamWriter.WriteLine(answer);
                    message = streamReader.ReadLine();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    break;
                }
            }

            Console.WriteLine("Closing Connection");
            stream.Close();
            _tcpClient.Close();
        }

        private List<Book> GetBooks()
        {
            return BookList.Books;
        }

        private Book GetBook(string isbn13)
        {
            return BookList.Books.FirstOrDefault(data => data.Isbn13 == isbn13);
        }

        private void SaveBook(Book book)
        {
            BookList.Books.Add(book);
        }

        private string[] ConvertMessageToArray(string message)
        {
            string[] tempArray = message.Split("|");

            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = tempArray[i].Trim();
            }

            return tempArray;
        }
    }
}
