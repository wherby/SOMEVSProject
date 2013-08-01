
using System.Diagnostics;
using System;
namespace EMC.ESI.PowerShell.Monitor
{
    /// <summary>
    /// The TestLog class is used to generate log for test. The class is designed as singleton.
    /// </summary>
    public class TestLog
    {
        /// <summary>
        /// The singleton TestLog instance.
        /// </summary>
        private static TestLog logInstance = null;

        /// <summary>
        /// The constructor of TestLog, initialize the property for TestLog instance.
        /// </summary>
        private TestLog()
        {
            System.Diagnostics.Trace.Listeners.Clear();
            System.Diagnostics.Trace.AutoFlush = true;
            TraceListener defaultListerner = new ConsoleTraceListener();
            defaultListerner.TraceOutputOptions = (TraceOptions.DateTime | TraceOptions.Timestamp | TraceOptions.ProcessId);

            DateTime dataTime = DateTime.Now;
            string time = DateTime.Now.ToString("Hms");
            string date = DateTime.Now.ToString("MMddyy");
            string logfile = "c:\\log_" + date + "_" + time+".txt";

            TraceListener logListener = new TextWriterTraceListener(logfile);
            logListener.TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId;
            System.Diagnostics.Trace.Listeners.Add(defaultListerner);
            System.Diagnostics.Trace.Listeners.Add(logListener);            
        }

        /// <summary>
        /// Return a Testlog instance.
        /// </summary>
        /// <returns>Testlog instance.</returns>
        public static TestLog GetInstance()
        {
            if (logInstance == null)
            {
                logInstance = new TestLog();
                return logInstance;
            }
            else
            {
                return logInstance;
            }
        }

        #region Log information
        /// <summary>
        /// Logout INFO message for test. This type of message will be only used for Debug.
        /// </summary>
        /// <param name="msg">The message of the INFO message.</param>
        public void LogInfo(string msg)
        {
#if DEBUG
            Debug.WriteLine(String.Format("{0:G} [INFO]: {1}", DateTime.Now, msg));
#else

#endif
 
        }
        #endregion

        #region Log error message
        /// <summary>
        /// Logout ERROR message. This type of message will be used for both Debug and Release.
        /// </summary>
        /// <param name="msg">The error message</param>
        public void LogError(string msg)
        {
#if DEBUG
            Debug.WriteLine(String.Format("{0:G} [ERROR]: {1}", DateTime.Now, msg));
#else
                    string temp = DateTime.Now.ToString()  +" "+ msg;
                    Trace.WriteLine(temp);
#endif

        }
        #endregion 

        #region Log Warning message
        /// <summary>
        /// Logout Warning message. This type of message will be used for both Debug and Release.
        /// </summary>
        /// <param name="msg">The warning message</param>
        public void LogWarning(string msg)
        {
            string fullMSG = String.Format("{0:G} [Warning]: {1}", DateTime.Now, msg);
#if DEBUG
            Debug.WriteLine(fullMSG);
#else
                    Trace.WriteLine(fullMSG);
#endif
        }
        #endregion 

        #region Print out Test Case specified info
        /// <summary>
        /// Logout testcase information. This message will be used for both Debug and Release.
        /// </summary>
        /// <param name="testname">The information about test case.</param>
        public void LogTestCase(string testname)
        {
#if DEBUG
            Debug.WriteLine(String.Format("{0:G} [Test Case]: {1}", DateTime.Now, testname));
            Debug.WriteLine(Environment.NewLine);
#else
                    string temp = DateTime.Now.ToString()  +" "+ testname;
                    Trace.WriteLine(temp);
#endif

        }
        #endregion


    }
}