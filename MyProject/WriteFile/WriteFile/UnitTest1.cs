using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;

namespace WriteFile
{

    public class UnitTest1
    {
        static byte[] data = new byte[100];
        public static void Main()
        {
            DateTime dt=System.DateTime.Now;
            FileStream fs = new FileStream(@"c:\testwritting.txt", FileMode.Create);

            Thread writingThread = new Thread(new ParameterizedThreadStart(ThreadWriting));
            object o = fs;
            writingThread.Start(o);

            while(1==1)
            {
            if (Console.ReadLine() != "Y")
            {
                Thread.Sleep(1000);
            }
            else
            {
                writingThread.Abort();
                fs.Flush();
                fs.Close();
                break;
            }
            }

        }

        static void ThreadWriting(object fs)
        {
            FileStream fs3 = (FileStream)fs;
            while(1==1)
            {
                data = Encoding.Unicode.GetBytes(System.DateTime.Now.ToString());

                fs3.Write(data, 0, data.Length);
                Thread.Sleep(10000);
            }
        }
    }
}
