﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hostee1
{
    [Serializable]
    class Test
    {
        static void Main(string[] args)
        {
            Hostee1 tebase = new Hostee1();
            tebase.PrepareEnv();
            tebase.Invoke();
        }

        static void Test1()
        {
            Hostee1 tebase = new Hostee1();
            tebase.PrepareEnv();
            tebase.Invoke();
        }
    }
}
