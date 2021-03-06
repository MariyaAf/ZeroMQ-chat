﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZeroMQ;

namespace Examples
{
    static partial class Program
    {
        public static void Main(string[] args)
        {

            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Server");
                Console.WriteLine();
                args = new string[] { "" };
            }

            string name = args[0];

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:8080");

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        Console.WriteLine("Received {0}", request.ReadString());

                        // Do some work
                        Thread.Sleep(10);

                        // Send
                        responder.Send(new ZFrame(name));
                    }
                }
            }
        }
    }
}