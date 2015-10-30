using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace TestWcfDeviceService.SoapCommunication
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.xerox.com/gt/100/", ConfigurationName="IDeviceService")]
    public interface IDeviceService
    {

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://www.xerox.com/gt/100/:remoteCommandAsyncIn")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void RemoteCommandAsync(XDMExpress.Process.DeviceCommunicationProxy.RemoteCommandInstanceType RemoteCommandPackage);
    }
}
