using System;
using System.Collections.Generic;
using System.Text;

namespace TestForBall
{
//    class BallsConverter
//    {
//        string theGood(string[] convert)
//        {
//            List<List<int>> cLL = new List<List<int>>();
//            foreach (String temp in convert)
//            {
//                List<int> tempL = new List<int>();
//                char[] tempC = temp.ToCharArray();
//                foreach (char temp2 in temp)
//                {
//                    int temp3=0;
//                    if (temp2 >= 'A' && temp2 <= 'Z')
//                    {
//                        temp3 = temp2 - 'A';
//                    }
//                    if (temp2 >= 'a' && temp2 <= 'x')
//                    {
//                        temp3 = temp2 - 'a' + 26;
//                    }
//                    tempL.Add(temp3);
//                }
//                cLL.Add(tempL);
//            }
//            if (isGood(cLL)){ return "Good";}
//            else
//            {return "Bad";}
//        }

//        bool isGood(List<List<int>> CLL)
//        {
//            int N = CLL.Count;
//            for (int i = 0; i < N; i++)
//            {
//                for (int j = 0; j < N; j++)
//                {
//                    for (int k = 0; k < N; k++)
//                    {
//                        if ((CLL[i][CLL[j][k]] != CLL[j][CLL[i][k]]) || (CLL[k][CLL[j][i]] != CLL[j][CLL[i][k]]))
//                        {
//                            return false;
//                        }
//                    }
//                }
//            }
//            return true;
//        }

//        static void Main(string[] args)
//        {
//            string[] convert ={"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
//"ABCDEFGHIJKLMNOPQRSTUVWXYZabcd",
//"ACEGIKMOQSUWYacACEGIKMOQSUWYac",
//"ADGJMPSVYbADGJMPSVYbADGJMPSVYb",
//"AEIMQUYcCGKOSWaAEIMQUYcCGKOSWa",
//"AFKPUZAFKPUZAFKPUZAFKPUZAFKPUZ",
//"AGMSYAGMSYAGMSYAGMSYAGMSYAGMSY",
//"AHOVcFMTaDKRYBIPWdGNUbELSZCJQX",
//"AIQYCKSaEMUcGOWAIQYCKSaEMUcGOW",
//"AJSbGPYDMVAJSbGPYDMVAJSbGPYDMV",
//"AKUAKUAKUAKUAKUAKUAKUAKUAKUAKU",
//"ALWDOZGRcJUBMXEPaHSdKVCNYFQbIT",
//"AMYGSAMYGSAMYGSAMYGSAMYGSAMYGS",
//"ANaJWFSBObKXGTCPcLYHUDQdMZIVER",
//"AOcMaKYIWGUESCQAOcMaKYIWGUESCQ",
//"APAPAPAPAPAPAPAPAPAPAPAPAPAPAP",
//"AQCSEUGWIYKaMcOAQCSEUGWIYKaMcO",
//"AREVIZMdQDUHYLcPCTGXKbOBSFWJaN",
//"ASGYMASGYMASGYMASGYMASGYMASGYM",
//"ATIbQFYNCVKdSHaPEXMBUJcRGZODWL",
//"AUKAUKAUKAUKAUKAUKAUKAUKAUKAUK",
//"AVMDYPGbSJAVMDYPGbSJAVMDYPGbSJ",
//"AWOGcUMEaSKCYQIAWOGcUMEaSKCYQI",
//"AXQJCZSLEbUNGdWPIBYRKDaTMFcVOH",
//"AYSMGAYSMGAYSMGAYSMGAYSMGAYSMG",
//"AZUPKFAZUPKFAZUPKFAZUPKFAZUPKF",
//"AaWSOKGCcYUQMIEAaWSOKGCcYUQMIE",
//"AbYVSPMJGDAbYVSPMJGDAbYVSPMJGD",
//"AcaYWUSQOMKIGECAcaYWUSQOMKIGEC",
//"AdcbaZYXWVUTSRQPONMLKJIHGFEDCB"};
//            BallsConverter cv = new BallsConverter();
//            string result= cv.theGood(convert);
//        }
//    }

            
    class BallsConverter
    {

        string theGood(string[] convert)
        {
            int [,] CLL=new int [50,50];
            int N = convert.Length;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    char temp2 = convert[i][j];
                    int temp3 = 0;
                    if (temp2 >= 'A' && temp2 <= 'Z')
                    {
                        CLL[i,j] = temp2 - 'A';
                    }
                    if (temp2 >= 'a' && temp2 <= 'x')
                    {
                        CLL[i,j] = temp2 - 'a' + 26;
                    }
                }
            }
            for(int i=0;i<N;i++)
                for(int j=0;j<N;j++)
                    for (int k = 0; k < N; k++)
                    {
                        int x = CLL[CLL[i,j],k];
                        int y = CLL[i,CLL[j,k]];
                        if (x != y)
                            return "Bad";
                    }

            return "Good";
        }
    }
}




/*
 * 
Problem Statement
    
Rabbit Hanako invented a machine called the Balls Converter.  This machine accepts N types of balls. First, Hanako puts some balls into the machine. While the machine contains at least two balls, it chooses two different balls, throws them away, and creates a new ball. The types of the two balls that the machine throws away determine the type of the new ball. If it throws away a type-i ball and a type-j ball, the type of the new ball is described by convert[i][j]. If convert[i][j] is 'A', 'B', ..., 'Z', the type of the new ball is 0, 1, ..., 25, respectively. If convert[i][j] is 'a', 'b', ..., 'x', the type of the new ball is 26, 27, ..., 49, respectively.  If the type of the last ball can be always determined uniquely from the initial configuration of the box (regardless of which balls the machine throws away at each step), it's called a good machine. If the machine is good, return "Good". Otherwise return "Bad".
Definition
    
Class:
BallsConverter
Method:
theGood
Parameters:
string[]
Returns:
string
Method signature:
string theGood(string[] convert)
(be sure your method is public)
    

Constraints
-
convert will contain between 1 and 50 elements, inclusive.
-
Each element of convert will contain exactly N characters, where N is the number of elements in convert.
-
convert[i][j] will be equal to convert[j][i] for all valid i and j.
-
Each character in convert will be a letter corresponding to an integer between 0 and N-1, inclusive, where N is the number of elements in convert.
Examples
0)

    
{"AB", "BA"}
Returns: "Good"
The parity of the number of type-1 ball never changes. If the machine contains even number of type-1 balls at first, the last ball will be type-0. If the machine contains odd number of type-1 balls at first, the last ball will be type-1.
1)

    
{"BA", "AA"}
Returns: "Bad"
For example, if the machine contains one type-0 ball and two type-1 balls, the type of the last ball can't be uniquely determined. If two type-1 balls are chosen in the first operation, the last ball will be type-1. If one type-0 ball and one type-1 ball are chosen in the first operation, the last ball will be type-0.
2)

    
{"ACB", "CBA", "BAC"}
Returns: "Bad"

3)

    
{"AAAA", "ABBB", "ABCC", "ABCD"}
Returns: "Good"

4)

    
{"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
"ABCDEFGHIJKLMNOPQRSTUVWXYZabcd",
"ACEGIKMOQSUWYacACEGIKMOQSUWYac",
"ADGJMPSVYbADGJMPSVYbADGJMPSVYb",
"AEIMQUYcCGKOSWaAEIMQUYcCGKOSWa",
"AFKPUZAFKPUZAFKPUZAFKPUZAFKPUZ",
"AGMSYAGMSYAGMSYAGMSYAGMSYAGMSY",
"AHOVcFMTaDKRYBIPWdGNUbELSZCJQX",
"AIQYCKSaEMUcGOWAIQYCKSaEMUcGOW",
"AJSbGPYDMVAJSbGPYDMVAJSbGPYDMV",
"AKUAKUAKUAKUAKUAKUAKUAKUAKUAKU",
"ALWDOZGRcJUBMXEPaHSdKVCNYFQbIT",
"AMYGSAMYGSAMYGSAMYGSAMYGSAMYGS",
"ANaJWFSBObKXGTCPcLYHUDQdMZIVER",
"AOcMaKYIWGUESCQAOcMaKYIWGUESCQ",
"APAPAPAPAPAPAPAPAPAPAPAPAPAPAP",
"AQCSEUGWIYKaMcOAQCSEUGWIYKaMcO",
"AREVIZMdQDUHYLcPCTGXKbOBSFWJaN",
"ASGYMASGYMASGYMASGYMASGYMASGYM",
"ATIbQFYNCVKdSHaPEXMBUJcRGZODWL",
"AUKAUKAUKAUKAUKAUKAUKAUKAUKAUK",
"AVMDYPGbSJAVMDYPGbSJAVMDYPGbSJ",
"AWOGcUMEaSKCYQIAWOGcUMEaSKCYQI",
"AXQJCZSLEbUNGdWPIBYRKDaTMFcVOH",
"AYSMGAYSMGAYSMGAYSMGAYSMGAYSMG",
"AZUPKFAZUPKFAZUPKFAZUPKFAZUPKF",
"AaWSOKGCcYUQMIEAaWSOKGCcYUQMIE",
"AbYVSPMJGDAbYVSPMJGDAbYVSPMJGD",
"AcaYWUSQOMKIGECAcaYWUSQOMKIGEC",
"AdcbaZYXWVUTSRQPONMLKJIHGFEDCB"}
Returns: "Good"

5)

    
{"AAAAAFAAAAAAAAAAAAAAAAAXAAAAcAAAAAAAAAAnAAAAAAAvAA",
"ABBBBFBBBBBLBBBBQBBBBBBXYBBBcBBBBBBBBBBnBBBBBBBvwB",
"ABCCCFCCCCCLMCOCQRCCCCCXYCCCcCCCCCCCCCCnCCCCsCCvwC",
"ABCDEFGHDDDLMDODQRSDUVWXYDDbcdDfghDDklDnopDrsDDvwx",
"ABCEEFEEEEELMEOEQRSEUVWXYEEbcEEfEhEEklEnEpEEsEEvwE",
"FFFFFFFFFFFFFFFFFFFFFFFXFFFFcFFFFFFFFFFFFFFFFFFvFF",
"ABCGEFGGGGGLMGOGQRSGUVWXYGGbcGGfGhGGklGnGpGGsGGvwG",
"ABCHEFGHHHHLMHOHQRSHUVWXYHHbcdHfHhHHklHnHpHHsHHvwH",
"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx",
"ABCDEFGHJJJLMJOJQRSJUVWXYJJbcdJfghJJklJnopJrsJJvwx",
"ABCDEFGHKJKLMNOPQRSTUVWXYZKbcdefghijklmnopqrsKKvwx",
"ALLLLFLLLLLLLLLLLLLLLLLXLLLLcLLLLLLLLLLnLLLLLLLvwL",
"ABMMMFMMMMMLMMOMQMMMMMMXYMMMcMMMMMMMMMMnMMMMMMMvwM",
"ABCDEFGHNJNLMNOPQRSTUVWXYZNbcdefghijklmnopqrsNNvwx",
"ABOOOFOOOOOLOOOOQOOOOOOXYOOOcOOOOOOOOOOnOOOOOOOvwO",
"ABCDEFGHPJPLMPOPQRSTUVWXYZPbcdPfghiPklmnopPrsPPvwx",
"AQQQQFQQQQQLQQQQQQQQQQQXQQQQcQQQQQQQQQQnQQQQQQQvwQ",
"ABRRRFRRRRRLMRORQRRRRRRXYRRRcRRRRRRRRRRnRRRRRRRvwR",
"ABCSSFSSSSSLMSOSQRSSSSSXYSSScSSSSSSSSSSnSSSSsSSvwS",
"ABCDEFGHTJTLMTOTQRSTUVWXYTTbcdTfghiTklTnopTrsTTvwx",
"ABCUUFUUUUULMUOUQRSUUUWXYUUUcUUUUhUUklUnUUUUsUUvwU",
"ABCVVFVVVVVLMVOVQRSVUVWXYVVbcVVfVhVVklVnVVVVsVVvwV",
"ABCWWFWWWWWLMWOWQRSWWWWXYWWWcWWWWhWWkWWnWWWWsWWvwW",
"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXvXX",
"AYYYYFYYYYYLYYYYQYYYYYYXYYYYcYYYYYYYYYYnYYYYYYYvwY",
"ABCDEFGHZJZLMZOZQRSTUVWXYZZbcdZfghiZklZnopZrsZZvwx",
"ABCDEFGHaJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx",
"ABCbbFbbbbbLMbObQRSbUbWXYbbbcbbfbhbbklbnbbbbsbbvwb",
"cccccccccccccccccccccccXcccccccccccccccccccccccvcc",
"ABCdEFGddddLMdOdQRSdUVWXYddbcddfdhddkldndpddsddvwd",
"ABCDEFGHeJeLMeOPQRSTUVWXYZebcdefghijklmnoperseevwx",
"ABCffFfffffLMfOfQRSfUfWXYfffcffffhffklfnffffsffvwf",
"ABCgEFGHgggLMgOgQRSgUVWXYggbcdgfghggklgngpggsggvwg",
"ABChhFhhhhhLMhOhQRShhhhXYhhhchhhhhhhkhhnhhhhshhvwh",
"ABCDEFGHiJiLMiOiQRSiUVWXYiibcdifghiiklinopirsiivwx",
"ABCDEFGHjJjLMjOPQRSTUVWXYZjbcdjfghijklmnopjrsjjvwx",
"ABCkkFkkkkkLMkOkQRSkkkkXYkkkckkkkkkkkkknkkkkskkvwk",
"ABCllFlllllLMlOlQRSlllWXYlllcllllhllkllnllllsllvwl",
"ABCDEFGHmJmLMmOmQRSTUVWXYZmbcdmfghimklmnopmrsmmvwx",
"nnnnnFnnnnnnnnnnnnnnnnnXnnnncnnnnnnnnnnnnnnnnnnvnn",
"ABCoEFGHoooLMoOoQRSoUVWXYoobcdofghooklonoporsoovwx",
"ABCppFpppppLMpOpQRSpUVWXYppbcppfphppklpnpppSsppvwp",
"ABCDEFGHqJqLMqOPQRSTUVWXYZqbcdefghijklmnopqrsqqvwx",
"ABCrEFGHrrrLMrOrQRSrUVWXYrrbcdrfghrrklrnrSrrsrrvwx",
"ABsssFsssssLMsOsQRsssssXYssscssssssssssnsssssssvws",
"ABCDEFGHtJKLMNOPQRSTUVWXYZtbcdefghijklmnopqrsttvwx",
"ABCDEFGHuJKLMNOPQRSTUVWXYZubcdefghijklmnopqrstuvwx",
"vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv",
"AwwwwFwwwwwwwwwwwwwwwwwXwwwwcwwwwwwwwwwnwwwwwwwvww",
"ABCxEFGHxxxLMxOxQRSxUVWXYxxbcdxfghxxklxnxpxxsxxvwx"}
Returns: "Bad"

This problem statement is the exclusive and proprietary property of TopCoder, Inc. Any unauthorized use or reproduction of this information without the prior written consent of TopCoder, Inc. is strictly prohibited. (c)2003, TopCoder, Inc. All rights reserved.
 */
