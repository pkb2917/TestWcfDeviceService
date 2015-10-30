using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace TestWcfDeviceService.Inspections
{
    class CustomBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint serviceEndpoint,
         System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint,
        System.ServiceModel.Dispatcher.ClientRuntime behavior)
        {
            //Add the inspector
            behavior.MessageInspectors.Add(new MessageInpector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint,
        System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }
        public void Validate(ServiceEndpoint serviceEndpoint)
        {
        }
    }
}
