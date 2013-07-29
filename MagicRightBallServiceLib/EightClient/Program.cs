using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EightClient.ServiceReference;

namespace EightClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EightBallClient ball = new EightBallClient())
            {
                Console.WriteLine("Your Question");
                string question = Console.ReadLine();
                string answer = ball.ObtainAnswerToQuestion(question);
                Console.WriteLine("8 Ball says {0}", answer);
            }
            Console.ReadLine();
        }
    }
}
