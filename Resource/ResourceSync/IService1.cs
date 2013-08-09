using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ResourceSync
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace="www.Intertech.com")]
    public interface IResource
    {
        [OperationContract]
        bool GetResource(string resourceID);

        [OperationContract]
        void ReleaseResource(string resourceID);

        [OperationContract]
        void ResetAllResource();

        [OperationContract]
        void SetAllDebugTrace();

        //[OperationContract]
        //int Add(int x, int y);

        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    //// Use a data contract as illustrated in the sample below to add composite types to service operations
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
