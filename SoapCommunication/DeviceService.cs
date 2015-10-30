using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using XDMExpress.Process.DeviceCommunicationProxy;

namespace TestWcfDeviceService.SoapCommunication
{
    public class DeviceService : IDeviceService
    {
        public void RemoteCommandAsync(XDMExpress.Process.DeviceCommunicationProxy.RemoteCommandInstanceType remoteCommand)
        {
            //TdmServiceDeviceClient proxy = new TdmServiceDeviceClient();
            if (remoteCommand != null)
            {
                TabControl tabControl = Application.OpenForms["frmTdmSimulator"].Controls.OfType<TabControl>().First(x => x.Name == "tabControl");

               
                tabControl.Controls[1].Enabled = true;

                TextBox txtResult = tabControl.Controls[0].Controls.OfType<TextBox>().First(x => x.Name == "txtDetails");
                txtResult.Enabled = true;
                XmlSerializer xsSubmit = new XmlSerializer(typeof(RemoteCommandInstanceType));

                System.IO.StringWriter sww = new System.IO.StringWriter();
                XmlWriter writer = XmlWriter.Create(sww);
                xsSubmit.Serialize(writer, remoteCommand);
                string text = sww.ToString();
                txtResult.Text = text;

            }

            TdmServiceDeviceClient proxy = new TdmServiceDeviceClient(System.Configuration.ConfigurationManager.AppSettings["Endpoint"] as string);
            proxy.Endpoint.Behaviors.Add(new TestWcfDeviceService.Inspections.CustomBehavior());
            RemoteCommandResultType result = new RemoteCommandResultType();
            result.ID = remoteCommand.ID;
            result.InstanceID = remoteCommand.InstanceID; 
            result.Message = "Command received";
            result.Status = 2;
            proxy.RemoteCommandReply(result);
            //result = new RemoteCommandResultType();
            //result.ID = remoteCommand.InstanceID;
            //result.InstanceID = Guid.NewGuid().ToString();
            //result.Message = "In Progress";
            //result.Status = 1;
            //proxy.RemoteCommandReply(result);
            //result = new RemoteCommandResultType();
            //result.ID = remoteCommand.InstanceID;
            //result.InstanceID = Guid.NewGuid().ToString();
            //result.Message = "Command Executed";
            //result.Status = 2;
            //proxy.RemoteCommandReply(result);
        }
    }
}
