using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace MagicRightBallServiceLib
{
    public class MagicRightBallService:IEightBall 
    {
        public MagicRightBallService()
        {
            Console.WriteLine("The Eight Ball Service Start ");
        }

        public string ObtainAnswerToQuestion(string userQuestion)
        {
            string[] answers = { "Yes", "No" };

            Random r = new Random();
            return string.Format("{0}?{1}", userQuestion, answers[r.Next(answers.Length)]);
        }
    }

    [ServiceContract]
    public interface IEightBall
    {
        [OperationContract]
        string ObtainAnswerToQuestion(string userQuestion);
    }
}
