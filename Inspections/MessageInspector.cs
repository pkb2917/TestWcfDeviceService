using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace TestWcfDeviceService.Inspections
{

    class MessageInpector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message
         reply, object correlationState)
        {
            frmTdmSimulator.replyMessage = reply.ToString();
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message
         request, System.ServiceModel.IClientChannel channel)
        {

            //Handle Request Validation here
            return null;
        }
    }
}
