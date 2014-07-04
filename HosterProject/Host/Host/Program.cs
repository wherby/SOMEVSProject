using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Runtime.Remoting;

namespace Host
{
    [Serializable]
    public class Program
    {

        static void Main(string[] args)
        {
            Marshalling();
        }


        public static void Marshalling()
        {
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            string callDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain ={0}", callDomainName);

            string exeAsembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly={0}", exeAsembly);

            AppDomain ad2 = null;
            Console.WriteLine("{0}Demo #1", Environment.NewLine);

            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAsembly, "Host.MarshalByRefType");
            //mbrt = new MarshalByRefType();

            Console.WriteLine("Type={0}", mbrt.GetType());

            Console.WriteLine("Is proxy={0}", RemotingServices.IsTransparentProxy(mbrt));

            mbrt.SomeMethod();

            AppDomain.Unload(ad2);
            try
            {
                mbrt.SomeMethod();
                Console.WriteLine("Successful call");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("call faied");
            }


            Console.WriteLine("{0}Demo #2", Environment.NewLine);
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAsembly, "Host.MarshalByRefType");
            MarshalByValType mbvt = mbrt.MethodWithReturn();

            Console.WriteLine("Is proxy={0}", RemotingServices.IsTransparentProxy(mbvt));
            Console.WriteLine("Returned obbject created" + mbvt.ToString());

            try
            {
                Console.WriteLine("Return object created" + mbvt.ToString());
                Console.WriteLine("Successful call");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Failed call");
            }

            Console.WriteLine("{0}Demo #3", Environment.NewLine);
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAsembly, "Host.MarshalByRefType");
            NonMarshalableType nmt = mbrt.MethodArgAndReturn(callDomainName);
        }

        
    }

    [Serializable]
    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(), Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in" + Thread.GetDomain().FriendlyName);
            MarshalByValType t = new MarshalByValType();
            return t;
        }

        public NonMarshalableType MethodArgAndReturn(String callingDomainName)
        {
            Console.WriteLine("Calling from '{0}' to '{1}'", callingDomainName, Thread.GetDomain().FriendlyName);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
    }

    [Serializable]
    public sealed class MarshalByValType : Object
    {
        private DateTime m_createTime = DateTime.Now;
        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in  {1}, created on {2:D}",this.GetType().ToString(),Thread.GetDomain().FriendlyName,m_createTime);
        }

        public override string ToString()
        {
            return m_createTime.ToLongDateString();
        }
    }

    //[Serializable]
    public sealed class NonMarshalableType : Object
    {
        public NonMarshalableType()
        {
            Console.WriteLine("Executing in "+Thread.GetDomain().FriendlyName);
        }
    }
}
