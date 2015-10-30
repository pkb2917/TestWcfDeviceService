using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Schema;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Configuration;
using System.ServiceModel;
using System.Reflection;
using System.Threading;
namespace TestWcfDeviceService
{
    enum DeviceGroupBoxOptions
    {
        TdmInterface,
        DeviceInterface
    }
    enum Methods
    {
        DescribeDevice = 1,
        CheckForUpdates,
        ReportUpdateStatus,
        ReportDeviceComponentChanges,
        RemoteCommandReply,
        SendAlert,
        CheckMedia
    }
    public partial class frmTdmSimulator : Form
    {

        #region Constants

        const string TCP_PORT_KEY = "tcpListnerPort";
        const string TCP_IP_KEY = "tcpIp";
        const string CERT_PATH = "certificatePath";
        const string CERT_KEY = "CertficateKey";

        const string GRD_DEVICE_GUID = "DeviceGuid";
        const string GRD_LOGICAL_ID = "LogicalId";
        const string GRD_DEVICE_TYPE = "DeviceType";
        const string GRD_MANUFACTURER = "Manufacturer";
        const string GRD_MODEL = "Model";
        const string GRD_SERIAL_NUMBER = "SerialNumber";
        const string GRD_MAC_ADDRESS = "MacAddress";
        const string GRD_IP_ADDRESS = "IPAddress";
        const string GRD_SW_VERSION = "SoftwareVersion";

        const string GRD_IS_SNAPSHOT = "IsSnapShot";
        const string GRD_ALERT_ID = "AlertId";
        const string GRD_COMPONENT_SERIAL_NO = "ComponentSerialNo";
        const string GRD_COMPONENT_DEVICE_TYPE = "ComponentDeviceType";
        const string GRD_IS_ALERT_REMOVED = "IsAlertRemoved";
        const string GRD_ALERT_MESSAGE = "AlertMessage";

        const string GRD_RMCD_CHK = "RCSelect";
        const string GRD_RMCD_ID = "RCId";
        const string GRD_RMCD_NAME = "RCName";
        const string GRD_RMCD_INSTNCE_ID = "RCInstance";
        const string GRD_RMCD_DEVICEID = "RCDeviceId";
        const string GRD_RMCD_SERIAL_NUMBER = "RCSerialNumber";
        const string GRD_RMCD_LOGICALID = "RCLogicalId";
        const string GRD_RMCD_IP = "RCIPAddress";
        const string GRD_RMCD_DESC = "RCDesc";
        const string GRD_RMCD_MAC = "RCMac";
        const string GRD_RMCD_MODEL = "RCModel";
        const string GRD_RMCD_MANF = "RCManf";
        const string GRD_RMCD_DEVICE_TYPE = "RCDeviceType";
        const string GRD_RMCD_HOST = "RCHost";
        const string GRD_RMCD_SW_VER = "RCSwVersion";
        const string GRD_RMCD_SERVICE_STATUS = "RCServiceStatus";
        const string GRD_RMCD_ICON = "RCIcon";

        #endregion

        #region MemberData

        bool shownReplyMessage;
        public static string replyMessage;
        string _selectedXmlForAlertPerformance = string.Empty;
        int _NumberofPkg, _every, _executionDuration = 0;
        StringBuilder _message = new StringBuilder();
        Methods _testMethod;
        bool m_Success = true;
        BackgroundWorker _worker = new BackgroundWorker();
        BackgroundWorker _displayMessage = new BackgroundWorker();
        BackgroundWorker _tcp = new BackgroundWorker();
        BackgroundWorker _alertSendMultiple = new BackgroundWorker();
        //TdmServiceDeviceClient proxy = new TdmServiceDeviceClient();    
        TdmServiceDeviceClient proxy = new TdmServiceDeviceClient(System.Configuration.ConfigurationManager.AppSettings["Endpoint"] as string);
        System.Windows.Forms.Timer messageTimer = new System.Windows.Forms.Timer();
        BooleanType isUsedMedia = null;
        string mediaSerialNumber = null;
        int nCurrentRow_DescDevice, nCurrentRow_SendAlert;
        Random random = new Random();
        #endregion

        #region Constructor

        public frmTdmSimulator()
        {
            try
            {
                InitializeComponent();

                Thread LoggerThread = new Thread(() => { LogWriter(); });
                LoggerThread.IsBackground = true;
                LoggerThread.Name = "Logger_Thread";
                LoggerThread.Start();

                HostDeviceService();
                shownReplyMessage = false;
                _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
                _tcp.DoWork += new DoWorkEventHandler(_tcp_DoWork);
                _displayMessage.DoWork += new DoWorkEventHandler(_displayMessage_DoWork);
                _displayMessage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_displayMessage_RunWorkerCompleted);
                _alertSendMultiple.DoWork += new DoWorkEventHandler(_alertSendMultiple_DoWork);
                _alertSendMultiple.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_alertSendMultiple_RunWorkerCompleted);
                _tcp.RunWorkerAsync();
                proxy.Endpoint.Behaviors.Add(new TestWcfDeviceService.Inspections.CustomBehavior());
                messageTimer.Enabled = true;
                messageTimer.Interval = 3000;
                messageTimer.Start();
                messageTimer.Tick += new EventHandler(messageTimer_Tick);
                proxy.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(@"F:\TestCode\SSLExample\Misc\certificate.pfx", "KTYy77216");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void _alertSendMultiple_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtDuration.Enabled = true;
            txtEvery.Enabled = true;
            txtNumberOfPkgs.Enabled = true;
            cmdStartThread.Enabled = true;
            cmdSelectedPkg.Enabled = true;
        }

        void _alertSendMultiple_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                string pkgContent = string.Empty;
                if (System.IO.File.Exists(_selectedXmlForAlertPerformance))
                {
                    using (StreamReader myReader = new StreamReader(_selectedXmlForAlertPerformance))
                    {
                        pkgContent = myReader.ReadToEnd();
                    }
                }
                XmlSerializer serializer = new XmlSerializer(typeof(AlertPackageType));
                AlertPackageType alertPkgToSend = (AlertPackageType)serializer.Deserialize(new StringReader(pkgContent));
                do
                {
                    proxy.SendAlert(alertPkgToSend);
                    System.Threading.Thread.Sleep(_every * 1000);
                    TimeSpan timeSpan = DateTime.Now - startTime;
                    if (timeSpan.Minutes >= _executionDuration)
                        break;

                }
                while (true);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Member Function

        public static string Path_Current_App = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        System.Threading.ManualResetEvent _ManualResetEvent = new System.Threading.ManualResetEvent(false);

        public static volatile int rowiterator = 0;
        private string GetTDMConnectionString
        {
            get
            {
                string sConstr = string.Empty;
                try
                {
                    sConstr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TDMDBConnectionString"]);
                }
                catch { }
                return sConstr;
            }
        }

        private void InitPerformanceView()
        {
            #region Describe Device view

            this.dgvPerformance_DescDevice.Columns.Clear();
            this.dgvPerformance_DescDevice.Columns.Add(GRD_DEVICE_GUID, "Device GUID");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_LOGICAL_ID, "Logical ID");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_DEVICE_TYPE, "Device Type");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_MANUFACTURER, "Manufacturer");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_MODEL, "Model");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_SERIAL_NUMBER, "Serial#");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_MAC_ADDRESS, "MAC Address");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_IP_ADDRESS, "IP Address");
            this.dgvPerformance_DescDevice.Columns.Add(GRD_SW_VERSION, "Software Ver.");

            this.dgvPerformance_DescDevice.Columns[GRD_LOGICAL_ID].Width = 78;
            this.dgvPerformance_DescDevice.Columns[GRD_MODEL].Width = 73;
            this.dgvPerformance_DescDevice.Columns[GRD_DEVICE_TYPE].Width = 91;
            this.dgvPerformance_DescDevice.Columns[GRD_SERIAL_NUMBER].Width = 75;

            this.dgvPerformance_DescDevice.Rows.Add();
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_DEVICE_GUID].Value = Guid.NewGuid().ToString();
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_LOGICAL_ID].Value = "1";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_DEVICE_TYPE].Value = "FVD";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_MANUFACTURER].Value = "Xerox";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_MODEL].Value = "FVDm";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_SERIAL_NUMBER].Value = "11";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_MAC_ADDRESS].Value = "AAAAAAAAAAAA";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_IP_ADDRESS].Value = "127.0.0.1";
            this.dgvPerformance_DescDevice.Rows[0].Cells[GRD_SW_VERSION].Value = "1.43.0";
            nCurrentRow_DescDevice = 0;

            #endregion

            #region Alert view

            this.dgv_Performance_SendAlert.Columns.Clear();
            this.dgv_Performance_SendAlert.Columns.Add(GRD_DEVICE_GUID, "Device GUID");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_IS_SNAPSHOT, "Is SnapShot");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_ALERT_ID, "Alert ID");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_COMPONENT_SERIAL_NO, "Comp. Serial #");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_COMPONENT_DEVICE_TYPE, "Comp. Device Type");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_IS_ALERT_REMOVED, "Is Alert Removed");
            this.dgv_Performance_SendAlert.Columns.Add(GRD_ALERT_MESSAGE, "Message");

            this.dgv_Performance_SendAlert.Columns[GRD_IS_SNAPSHOT].Width = 73;
            this.dgv_Performance_SendAlert.Columns[GRD_COMPONENT_SERIAL_NO].Width = 102;
            this.dgv_Performance_SendAlert.Columns[GRD_COMPONENT_DEVICE_TYPE].Width = 130;
            this.dgv_Performance_SendAlert.Columns[GRD_IS_ALERT_REMOVED].Width = 120;
            this.dgv_Performance_SendAlert.Columns[GRD_ALERT_MESSAGE].Width = 93;

            this.dgv_Performance_SendAlert.Rows.Add();
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_DEVICE_GUID].Value = Guid.NewGuid().ToString();
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_IS_SNAPSHOT].Value = "false";
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_ALERT_ID].Value = Guid.NewGuid().ToString();
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_COMPONENT_SERIAL_NO].Value = "11";
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_COMPONENT_DEVICE_TYPE].Value = "FVD";
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_IS_ALERT_REMOVED].Value = "false";
            this.dgv_Performance_SendAlert.Rows[0].Cells[GRD_ALERT_MESSAGE].Value = "Test";
            nCurrentRow_SendAlert = 0;

            #endregion

            #region remote command view

            this.dgvPendingRemoteCommands.Columns.Clear();
            DataGridViewCheckBoxColumn chkBox = new DataGridViewCheckBoxColumn();
            chkBox.Name = GRD_RMCD_CHK;
            chkBox.HeaderText = "";
            this.dgvPendingRemoteCommands.Columns.Add(chkBox);
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_ID, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_NAME, "Command Name");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_INSTNCE_ID, "Instance Id");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_DEVICEID, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_SERIAL_NUMBER, "Serial Number");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_LOGICALID, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_IP, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_DESC, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_MAC, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_MODEL, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_MANF, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_DEVICE_TYPE, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_HOST, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_SW_VER, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_SERVICE_STATUS, "");
            this.dgvPendingRemoteCommands.Columns.Add(GRD_RMCD_ICON, "");


            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_CHK].Width = 25;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_CHK].ReadOnly = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_ID].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_NAME].Width = 130;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_NAME].ReadOnly = true;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_INSTNCE_ID].Width = 130;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_INSTNCE_ID].ReadOnly = true;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_DEVICEID].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_SERIAL_NUMBER].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_SERIAL_NUMBER].ReadOnly = true;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_LOGICALID].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_IP].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_DESC].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_MAC].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_MODEL].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_MANF].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_DEVICE_TYPE].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_HOST].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_SW_VER].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_SERVICE_STATUS].Visible = false;
            this.dgvPendingRemoteCommands.Columns[GRD_RMCD_ICON].Visible = false;

            this.dgvPendingRemoteCommands.Rows.Add();

            #endregion
        }

        private void AddRowAndFillTempDataForDescribeDevice()
        {
            int nMaxRowCount = string.IsNullOrEmpty(txtRowCount_performance_DescDevice.Text) ? 0 : Convert.ToInt32(txtRowCount_performance_DescDevice.Text);
            for (int i = 0; i < nMaxRowCount; i++)
            {
                this.dgvPerformance_DescDevice.Rows.Add();
                nCurrentRow_DescDevice += 1;
                if (this.dgvPerformance_DescDevice.Rows.Count > 13)
                    this.dgvPerformance_DescDevice.Columns[GRD_IP_ADDRESS].Width = 83;
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_DEVICE_GUID].Value = Guid.NewGuid().ToString();
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_LOGICAL_ID].Value = random.Next(1, 100000);
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_DEVICE_TYPE].Value = "FVD";
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_MANUFACTURER].Value = "Xerox";
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_MODEL].Value = "FVDm";
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_SERIAL_NUMBER].Value = random.Next(1, 100000000);
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_MAC_ADDRESS].Value = GenerateMACAddress();
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_IP_ADDRESS].Value =
                    string.Format("{0}.{1}.{2}.{3}", random.Next(1, 254), random.Next(1, 254), random.Next(1, 254), random.Next(1, 254));
                this.dgvPerformance_DescDevice.Rows[nCurrentRow_DescDevice].Cells[GRD_SW_VERSION].Value = "1.43.0";
            }
        }

        private void AddRowAndFillTempDataForSendAlert()
        {
            int nMaxRowCount = string.IsNullOrEmpty(txtRowCount_Performance_SendAlert.Text) ? 0 : Convert.ToInt32(txtRowCount_Performance_SendAlert.Text);
            for (int i = 0; i < nMaxRowCount; i++)
            {
                this.dgv_Performance_SendAlert.Rows.Add();
                nCurrentRow_SendAlert += 1;
                if (this.dgv_Performance_SendAlert.Rows.Count > 13)
                    this.dgv_Performance_SendAlert.Columns[GRD_ALERT_MESSAGE].Width = 60;
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_DEVICE_GUID].Value = Guid.NewGuid().ToString();
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_IS_SNAPSHOT].Value = "false";
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_ALERT_ID].Value = Guid.NewGuid().ToString();
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_COMPONENT_SERIAL_NO].Value = "11";
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_COMPONENT_DEVICE_TYPE].Value = "FVD";
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_IS_ALERT_REMOVED].Value = "false";
                this.dgv_Performance_SendAlert.Rows[nCurrentRow_SendAlert].Cells[GRD_ALERT_MESSAGE].Value = "test";
            }
        }

        private void SendDataExportPackageForPerformance()
        {
            List<DescribeDevicePackageType> _DescribeDevicePackageTypeLst = new List<DescribeDevicePackageType>();
            for (int i = 0; i <= nCurrentRow_DescDevice; i++)
            {
                if (this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_GUID].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_LOGICAL_ID].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_TYPE].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MANUFACTURER].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MODEL].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SERIAL_NUMBER].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MAC_ADDRESS].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_IP_ADDRESS].Value != null &&
                    this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SW_VERSION].Value != null &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_GUID].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_LOGICAL_ID].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_TYPE].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MANUFACTURER].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MODEL].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SERIAL_NUMBER].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MAC_ADDRESS].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SW_VERSION].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_IP_ADDRESS].Value)))
                {
                    DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
                    describeDevice.DeviceGUID = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_GUID].Value);
                    DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
                    deviceDescriptionType.Description = "Test";
                    deviceDescriptionType.DeviceGUID = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_GUID].Value);
                    deviceDescriptionType.IPAddress = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_IP_ADDRESS].Value);
                    deviceDescriptionType.LogicalDeviceID = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_LOGICAL_ID].Value);
                    deviceDescriptionType.MACAddress = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MAC_ADDRESS].Value);
                    deviceDescriptionType.Manufacturer = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MANUFACTURER].Value);
                    deviceDescriptionType.Model = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_MODEL].Value);
                    deviceDescriptionType.SerialNumber = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SERIAL_NUMBER].Value);
                    deviceDescriptionType.HostName = string.Format("Host {0}", deviceDescriptionType.SerialNumber);
                    deviceDescriptionType.SoftwareVersion = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_SW_VERSION].Value);
                    deviceDescriptionType.Type = Convert.ToString(this.dgvPerformance_DescDevice.Rows[i].Cells[GRD_DEVICE_TYPE].Value);
                    describeDevice.DeviceDescription = deviceDescriptionType;

                    DeviceStatusesType deviceStatusType = new DeviceStatusesType();
                    deviceStatusType.ServiceStatus = 1;
                    deviceStatusType.Status = 2;
                    deviceStatusType.StatusDate = DateTime.Now;

                    describeDevice.DeviceStatus = deviceStatusType;
                    try
                    {
                        _DescribeDevicePackageTypeLst.Add(describeDevice);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }

                }
            }


            try
            {
                int j = 0;
                foreach (var item in _DescribeDevicePackageTypeLst)
                {
                    j = j + 1;
                    Thread SendDescribeDevicePackageType = new Thread(() => { proxy.DescribeDevice(item); });
                    SendDescribeDevicePackageType.Name = "Thread" + j.ToString();
                    SendDescribeDevicePackageType.Start();
                    System.Threading.Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Set();
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Reset();
        }

        private void SendSendAlertPackageForPerformance()
        {
            List<AlertPackageType> lstAlertPackageType = new List<AlertPackageType>();
            for (int i = 0; i <= nCurrentRow_SendAlert; i++)
            {
                if (this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_DEVICE_GUID].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_SNAPSHOT].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_ID].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_SERIAL_NO].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_DEVICE_TYPE].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_ALERT_REMOVED].Value != null &&
                    this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_MESSAGE].Value != null &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_DEVICE_GUID].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_SNAPSHOT].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_ID].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_SERIAL_NO].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_DEVICE_TYPE].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_ALERT_REMOVED].Value)) &&
                    !string.IsNullOrEmpty(Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_MESSAGE].Value)))
                {
                    AlertPackageType _alertPackageType = new AlertPackageType();
                    _alertPackageType.DeviceGUID = Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_DEVICE_GUID].Value);

                    DeviceStatusesType deviceStatusType = new DeviceStatusesType();
                    deviceStatusType.ServiceStatus = 1;
                    deviceStatusType.Status = 1;
                    deviceStatusType.StatusDate = DateTime.Now;

                    _alertPackageType.DeviceStatus = deviceStatusType;

                    RemoteAlertInstanceType _remoteAlertInstanceType = new RemoteAlertInstanceType();
                    _remoteAlertInstanceType.ComponentDeviceType = Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_DEVICE_TYPE].Value);
                    _remoteAlertInstanceType.ComponentSerialNumber = Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_COMPONENT_SERIAL_NO].Value);
                    _remoteAlertInstanceType.ID = Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_ID].Value);
                    _remoteAlertInstanceType.IsAlertRemoved = Convert.ToBoolean(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_ALERT_REMOVED].Value);
                    _remoteAlertInstanceType.Message = Convert.ToString(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_ALERT_MESSAGE].Value);
                    _alertPackageType.RemoteAlerts = new RemoteAlertsInstanceType();
                    _alertPackageType.RemoteAlerts.IsSnapShot = Convert.ToBoolean(this.dgv_Performance_SendAlert.Rows[i].Cells[GRD_IS_SNAPSHOT].Value);
                    _alertPackageType.RemoteAlerts.IsSnapShotSpecified = true;
                    _alertPackageType.RemoteAlerts.RemoteAlert = new RemoteAlertInstanceType[1];
                    _alertPackageType.RemoteAlerts.RemoteAlert[0] = _remoteAlertInstanceType;
                    try
                    {
                        lstAlertPackageType.Add(_alertPackageType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }

            try
            {
                int j = 0;
                foreach (var item in lstAlertPackageType)
                {
                    j = j + 1;
                    Thread SendAlertPackageType = new Thread(() => { proxy.SendAlert(item); });
                    SendAlertPackageType.Name = "Thread" + j.ToString();
                    SendAlertPackageType.Start();
                    System.Threading.Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Set();
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Reset();

        }

        #region Generate MAC Address

        public string GenerateMACAddress()
        {
            System.Threading.Thread.Sleep(100);
            var sBuilder = new StringBuilder();
            var r = new Random();
            int number;
            byte b;
            for (int i = 0; i < 6; i++)
            {
                number = r.Next(0, 255);
                b = Convert.ToByte(number);
                if (i == 0)
                {
                    b = setBit(b, 6); //--> set locally administered
                    b = unsetBit(b, 7); // --> set unicast 
                }
                sBuilder.Append(number.ToString("X2"));
            }
            return sBuilder.ToString().ToUpper();
        }

        private byte setBit(byte b, int BitNumber)
        {
            if (BitNumber < 8 && BitNumber > -1)
            {
                return (byte)(b | (byte)(0x01 << BitNumber));
            }
            else
            {
                throw new InvalidOperationException(
                "Der Wert für BitNumber " + BitNumber.ToString() + " war nicht im zulässigen Bereich! (BitNumber = (min)0 - (max)7)");
            }
        }

        private byte unsetBit(byte b, int BitNumber)
        {
            if (BitNumber < 8 && BitNumber > -1)
            {
                return (byte)(b | (byte)(0x00 << BitNumber));
            }
            else
            {
                throw new InvalidOperationException(
                "Der Wert für BitNumber " + BitNumber.ToString() + " war nicht im zulässigen Bereich! (BitNumber = (min)0 - (max)7)");
            }
        }

        #endregion

        private string GetIp()
        {
            string sTcpPort = GetConfigValue(TCP_IP_KEY);
            if (string.IsNullOrEmpty(sTcpPort))
            {
                return "172.0.0.1";
            }
            else
            {
                return sTcpPort;
            }
        }

        string GetConfigValue(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                return null;
            }
            else
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
        }

        int GetTcpPort()
        {
            string sTcpPort = GetConfigValue(TCP_PORT_KEY);
            if (string.IsNullOrEmpty(sTcpPort))
            {
                return 5000;
            }
            else
            {
                return Convert.ToInt32(sTcpPort);
            }
        }

        private void TcpListner()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(GetIp()), GetTcpPort());
            _message.AppendLine(string.Format("TCP listening on {0}:{1}", GetIp(), GetTcpPort()));
            while (true)
            {
                listener.Start();
                TcpClient clientSocket = listener.AcceptTcpClient();
                _message.AppendLine(string.Format("TCP connection established"));
                if (clientSocket != null)
                {
                    HandleClient(clientSocket);
                }
                listener.Stop();
                _testMethod = Methods.RemoteCommandReply;
                _worker.RunWorkerAsync();
            }
        }

        string GetCertificateName()
        {
            string scertificate = GetConfigValue(CERT_PATH);
            if (string.IsNullOrEmpty(scertificate))
            {
                return "SignedCertificate.cer";
            }
            else
            {
                return scertificate;
            }
        }

        string GetCertificateKey()
        {
            string scertificateKey = GetConfigValue(CERT_KEY);
            if (string.IsNullOrEmpty(scertificateKey))
            {
                return "goforit";
            }
            else
            {
                return scertificateKey;
            }
        }

        void HandleClient(TcpClient client)
        {
            _message.AppendLine("Getting local certificate");
            X509Certificate certificate = new X509Certificate(GetCertificateName());
            _message.AppendLine("received local certificate");
            using (NetworkStream myNwStream = client.GetStream())
            {

                BinaryFormatter formatter = new BinaryFormatter();
                Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType Package = new Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType();
                byte[] buffer = new byte[2048];
                int bytes = -1;
                bytes = myNwStream.Read(buffer, 0, buffer.Length);
                MemoryStream ms = new MemoryStream(buffer);
                Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType mp = (Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType)formatter.Deserialize(ms);
                RemoteCommandResultType result = new RemoteCommandResultType();
                result.ID = mp.ID;
                result.InstanceID = mp.InstanceID;
                result.Message = "Command received";
                result.Status = 0;
                proxy.RemoteCommandReply(result);
                result = new RemoteCommandResultType();
                result.ID = mp.ID;
                result.InstanceID = mp.InstanceID;
                result.Message = "In Progress";
                result.Status = 1;
                proxy.RemoteCommandReply(result);
                result = new RemoteCommandResultType();
                result.ID = mp.ID;
                result.InstanceID = mp.InstanceID;
                result.Message = "Command Executed";
                result.Status = 2;
                proxy.RemoteCommandReply(result);
                ms.Close();
            }

            //using (SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
            //{
            //    sslStream.AuthenticateAsServer(certificate);
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    byte[] buffer = new byte[2048];
            //    int bytes = -1;
            //    bytes = sslStream.Read(buffer, 0, buffer.Length);
            //    if (bytes > 0)
            //    {
            //        _message.AppendLine(string.Format("Remote command received"));                    
            //        MemoryStream ms = new MemoryStream(buffer);
            //        Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType mp = (Xerox.XOS.RemoteDeviceManagement.RemoteCommandInstanceType)formatter.Deserialize(ms);
            //        RemoteCommandResultType result = new RemoteCommandResultType();
            //        result.ID = mp.ID;
            //        result.InstanceID = mp.InstanceID;
            //        result.Message = "Command received";
            //        result.Status = 0;
            //        proxy.RemoteCommandReply(result);
            //        result = new RemoteCommandResultType();
            //        result.ID = mp.ID;
            //        result.InstanceID = mp.InstanceID;
            //        result.Message = "In Progress";
            //        result.Status = 1;
            //        proxy.RemoteCommandReply(result);
            //        result = new RemoteCommandResultType();
            //        result.ID = mp.ID;
            //        result.InstanceID = mp.InstanceID;
            //        result.Message = "Command Executed";
            //        result.Status = 2;
            //        proxy.RemoteCommandReply(result);
            //        ms.Close();
            //        _message.AppendLine(string.Format("Remote command reply has been called"));
            //    }
            //}
        }

        public byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(GetCertificateKey(), new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            //cs.Close();
            return ms.ToArray();
        }

        public bool ValidateServerCertificate(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;

        }

        private void ShowMessage()
        {
            if (!shownReplyMessage)
            {
                _message.AppendLine(replyMessage);
                shownReplyMessage = true;
            }
            txtResult.Text = _message.ToString();
            txtResult.Text = txtResult.Text + "\r\n" + "================================================";
        }

        private void LoadXml(string methodName)
        {
            string sFileName = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\XML\\", methodName, ".xml");
            if (System.IO.File.Exists(sFileName))
            {
                using (StreamReader myReader = new StreamReader(sFileName))
                {
                    txtDetails.Text = myReader.ReadToEnd();
                }
            }
        }

        private bool validateXml()
        {
            //return true;
            string xsd_file = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\XSD\\DeviceDataExchange.xsd");
            XmlSchema xsd = new XmlSchema();
            xsd.SourceUri = xsd_file;



            XmlSchemaSet ss = new XmlSchemaSet();
            ss.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            ss.Add(null, xsd_file);
            if (ss.Count > 0)
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags = XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.Schemas.Add(ss);
                settings.Schemas.Compile();
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                try
                {
                    List<XmlDocument> _pkgList = GetPackgeXmlList();
                    if (_pkgList != null && _pkgList.Count > 0)
                    {
                        m_Success = true;
                        foreach (XmlDocument doc in _pkgList)
                        {
                            TextReader textReader = new StringReader(doc.OuterXml);
                            XmlTextReader r = new XmlTextReader(textReader);
                            using (XmlReader reader = XmlReader.Create(r, settings))
                            {
                                try
                                {
                                    while (reader.Read())
                                    {
                                    }
                                    m_Success = true;
                                }
                                catch
                                {
                                    m_Success = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_Success = false;
                    }
                }
                catch
                {
                    m_Success = false;
                }
            }
            return m_Success;
        }

        private string GetPackageHeader()
        {
            string _pkgHeader = "";
            switch (_testMethod)
            {
                case Methods.DescribeDevice:
                    _pkgHeader = "DescribeDevicePackageType";
                    break;
                case Methods.CheckForUpdates:
                    _pkgHeader = "ManifestPackageType";
                    break;
                case Methods.ReportUpdateStatus:
                    _pkgHeader = "UpdateStatusPackageType";
                    break;
                case Methods.ReportDeviceComponentChanges:
                    _pkgHeader = "ComponentPackageType";
                    break;
                case Methods.RemoteCommandReply:
                    _pkgHeader = "RemoteCommandResultType";
                    break;
                case Methods.SendAlert:
                    _pkgHeader = "AlertPackageType";
                    break;
                case Methods.CheckMedia:
                    _pkgHeader = "UsedMediaType";
                    break;
            }
            return _pkgHeader;
        }

        private List<XmlDocument> GetPackgeXmlList()
        {
            List<XmlDocument> docList = new List<XmlDocument>();
            XmlDocument doc = new XmlDocument();
            XmlDocumentFragment fragment = doc.CreateDocumentFragment();
            if (!string.IsNullOrEmpty(txtDetails.Text))
            {
                fragment.InnerXml = txtDetails.Text;
                XmlNodeList nodeList = fragment.SelectNodes(GetPackageHeader());
                foreach (XmlNode node in nodeList)
                {
                    if (node.OuterXml != null && !string.IsNullOrEmpty(node.OuterXml))
                    {
                        doc = new XmlDocument();
                        doc.LoadXml(node.OuterXml);
                        docList.Add(doc);
                    }
                }
            }
            return docList;
        }

        private bool InvokeMethods()
        {
            try
            {
                shownReplyMessage = false;
                List<XmlDocument> xmlPackages = GetPackgeXmlList();
                foreach (XmlDocument xmlDocument in xmlPackages)
                {
                    XmlSerializer serializer;
                    switch (_testMethod)
                    {
                        case Methods.DescribeDevice:
                            serializer = new XmlSerializer(typeof(DescribeDevicePackageType));
                            DescribeDevicePackageType describeDevice = (DescribeDevicePackageType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.DescribeDevice(describeDevice);
                            break;
                        case Methods.CheckForUpdates:
                            serializer = new XmlSerializer(typeof(ManifestPackageType));
                            ManifestPackageType manifestPkgType = (ManifestPackageType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.CheckForUpdates(manifestPkgType);
                            break;
                        case Methods.ReportUpdateStatus:
                            serializer = new XmlSerializer(typeof(UpdateStatusPackageType));
                            UpdateStatusPackageType updatePkg = (UpdateStatusPackageType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.ReportUpdateStatus(updatePkg);
                            break;
                        case Methods.ReportDeviceComponentChanges:
                            serializer = new XmlSerializer(typeof(ComponentPackageType));
                            ComponentPackageType componentPkg = (ComponentPackageType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.ReportDeviceComponentChanges(componentPkg);
                            break;
                        case Methods.RemoteCommandReply:
                            serializer = new XmlSerializer(typeof(RemoteCommandResultType));
                            RemoteCommandResultType commandResultPkg = (RemoteCommandResultType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.RemoteCommandReply(commandResultPkg);
                            break;
                        case Methods.SendAlert:
                            serializer = new XmlSerializer(typeof(AlertPackageType));
                            AlertPackageType alertPkg = (AlertPackageType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            proxy.SendAlert(alertPkg);
                            break;
                        case Methods.CheckMedia:
                            serializer = new XmlSerializer(typeof(UsedMediaType));
                            UsedMediaType mediaType = (UsedMediaType)serializer.Deserialize(new StringReader(xmlDocument.OuterXml));
                            if (mediaType != null)
                                mediaSerialNumber = mediaType.MediaSerialNumber;
                            Stopwatch stopWatch = Stopwatch.StartNew();
                            isUsedMedia = proxy.CheckMedia(mediaType);
                            stopWatch.Stop();
                            MessageBox.Show(string.Format("Check media status : {0} and time : {1}", isUsedMedia.DefaultValueSpecified.ToString(), stopWatch.ElapsedMilliseconds.ToString()));

                            break;
                    }
                }
                if (!_worker.IsBusy)
                {
                    _worker.RunWorkerAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
                return false;
            }
        }

        private void ShowInterfaceTree(DeviceGroupBoxOptions MyOption)
        {
            string xmlFile = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\XML\\", MyOption.ToString(), ".xml");
            DataSet dsMethods = new DataSet();
            if (File.Exists(xmlFile))
            {
                dsMethods.ReadXml(xmlFile);
                if (dsMethods.Tables.Count > 0)
                {
                    trvMethods.Nodes.Clear();
                    TreeNode parentNode = new TreeNode();
                    parentNode.Text = "TDM Methods";
                    parentNode.Name = "TDMMethods";
                    parentNode.ImageIndex = 0;
                    //parentNode.Tag = -1;
                    TreeNode childNode;
                    foreach (DataRow row in dsMethods.Tables[0].Rows)
                    {
                        childNode = new TreeNode();
                        childNode.Name = row["Name"].ToString();
                        childNode.Text = row["Name"].ToString();
                        childNode.ImageIndex = 1;
                        childNode.SelectedImageIndex = 1;
                        childNode.Tag = row["Id"].ToString();
                        parentNode.Nodes.Add(childNode);
                    }
                    trvMethods.Nodes.Add(parentNode);
                    trvMethods.ShowPlusMinus = false;
                    trvMethods.ShowRootLines = false;
                    trvMethods.ShowLines = false;
                    trvMethods.ExpandAll();
                    trvMethods.SelectedNode = parentNode;
                }
            }
        }

        private void HostDeviceService()
        {
            ServiceHost _selfHost = new ServiceHost(typeof(SoapCommunication.DeviceService));

            //X509Certificate2 servercert = _selfHost.Credentials.ServiceCertificate.Certificate;
            //_selfHost.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new CustomCertificateValidator(servercert);
            //_selfHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            ServicePointManager.ServerCertificateValidationCallback += customXertificateValidation;
            _selfHost.Open();
            _message.AppendLine("SOAP service hosted");
        }

        private static bool customXertificateValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            var certificate = (X509Certificate2)cert;

            // Inspect the server certficiate here to validate 
            // that you are dealing with the correct server.
            // If so return true, if not return false.
            return true;
        }

        #endregion

        #region Event Handler

        void messageTimer_Tick(object sender, EventArgs e)
        {
            if (!_displayMessage.IsBusy)
                _displayMessage.RunWorkerAsync();
        }

        void _displayMessage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowMessage();
        }

        void _displayMessage_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        void _tcp_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListner();
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_testMethod == Methods.CheckMedia && isUsedMedia != null)
            {
                _message.AppendLine("================");
                _message.AppendLine((isUsedMedia.DefaultValueSpecified) ? string.Format("Used media: {0}", mediaSerialNumber) : string.Format("Unused media: {0}", mediaSerialNumber));
                _message.AppendLine("================");
            }
            _message.AppendLine("Affected tables");
            _message.AppendLine("================");
            switch (_testMethod)
            {
                case Methods.DescribeDevice:
                    _message.AppendLine("DeviceInfo");
                    _message.AppendLine("AlertPackage");
                    _message.AppendLine("DeviceAlertDetail");
                    break;
                case Methods.CheckForUpdates:
                    _message.AppendLine("DeviceManifest");
                    _message.AppendLine("DeviceManifestCategory");
                    _message.AppendLine("DeviceManifestPackage");
                    _message.AppendLine("DeviceUpdateManifest");
                    break;
                case Methods.ReportUpdateStatus:
                    _message.AppendLine("DeviceInstallationStatus");
                    _message.AppendLine("UpdateStatusDeviceManifestCategory");
                    break;
                case Methods.ReportDeviceComponentChanges:
                    _message.AppendLine("DeviceComponents");
                    break;
                case Methods.RemoteCommandReply:
                    _message.AppendLine("RemoteCommandResult");
                    break;
                case Methods.SendAlert:
                    _message.AppendLine("AlertPackage");
                    _message.AppendLine("DeviceAlertDetail");
                    _message.AppendLine("PrinterAlert");
                    break;
                case Methods.CheckMedia:
                    _message.AppendLine("UsedMedia");
                    break;
            }
        }

        private void ValidationCallBack(Object sender, ValidationEventArgs args)
        {
            //Display the validation error.  This is only called on error
            m_Success = false; //Validation failed            
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (validateXml())
            {
                if (InvokeMethods())
                    MessageBox.Show("Method invocation completed");
            }
            else
                MessageBox.Show("Invalid Xml Data");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult Result = ofdXmlFile.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = ofdXmlFile.FileName;
                string sFileName = txtFile.Text;
                if (System.IO.File.Exists(sFileName))
                {
                    using (StreamReader myReader = new StreamReader(sFileName))
                    {
                        txtDetails.Text = myReader.ReadToEnd();
                    }
                }
            }

        }

        private void frmTdmSimulator_Load(object sender, EventArgs e)
        {
            ShowInterfaceTree(DeviceGroupBoxOptions.TdmInterface);
            InitPerformanceView();
        }

        private void trvMethods_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!string.IsNullOrEmpty((string)trvMethods.SelectedNode.Tag))
            {
                LoadXml(trvMethods.SelectedNode.Name.ToString());
                _testMethod = (Methods)Convert.ToInt32(trvMethods.SelectedNode.Tag);
                btnExecute.Text = "Test " + _testMethod.ToString();
                btnExecute.Enabled = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                txtDetails.Clear();
                btnExecute.Text = "Test ";
                btnExecute.Enabled = false;
                btnBrowse.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            _message.Clear();
        }

        private void txtRowCount_performance_DescDevice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRowCount_Performance_SendAlert_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmdAdd_performance_DescDevice_Click(object sender, EventArgs e)
        {
            AddRowAndFillTempDataForDescribeDevice();
        }

        private void cmdClearRows_Performance_DescDevice_Click(object sender, EventArgs e)
        {
            this.dgvPerformance_DescDevice.Rows.Clear();
            nCurrentRow_DescDevice = -1;
        }

        private void cmdInvokeMethod_performance_DescDevice_Click(object sender, EventArgs e)
        {
            SendDataExportPackageForPerformance();
        }

        private void cmdClear_Performance_SendAlert_Click(object sender, EventArgs e)
        {
            this.dgv_Performance_SendAlert.Rows.Clear();
            nCurrentRow_SendAlert = -1;
        }

        private void cmdRowAdd_Performance_SendAlert_Click(object sender, EventArgs e)
        {
            AddRowAndFillTempDataForSendAlert();
        }

        private void cmdSendAlert_Performance_Click(object sender, EventArgs e)
        {
            SendSendAlertPackageForPerformance();
        }

        private void cmdCheckLum_Click(object sender, EventArgs e)
        {
            for (long var1 = 1000; var1 <= 2000; var1++)
            {
                for (long var2 = 1111; var2 <= 2000; var2++)
                {
                    for (long var3 = 2000; var3 <= 3000; var3++)
                    {
                        for (long var4 = 2222; var4 <= 3333; var4++)
                        {
                            try
                            {
                                System.Diagnostics.Stopwatch myTimer = Stopwatch.StartNew();
                                UsedMediaType mediaType = new UsedMediaType();
                                mediaType.MediaExpiringDate = DateTime.Now;
                                mediaType.StationID = "Test";
                                mediaType.Type = 1;
                                mediaType.TypeSpecified = true;
                                mediaType.MediaSerialNumber = string.Format("{0}{1}{2}{3}", var1, var2, var3, var4);
                                proxy.CheckMedia(mediaType);
                                myTimer.Stop();
                                //if (myTimer.ElapsedMilliseconds > 50)
                                //MessageBox.Show(myTimer.ElapsedMilliseconds.ToString());
                            }
                            catch
                            { //MessageBox.Show("Error"); 
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Done");
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult Result = ofdXmlFile.ShowDialog();
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                _selectedXmlForAlertPerformance = ofdXmlFile.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _NumberofPkg = Convert.ToInt32(txtNumberOfPkgs.Text);
            _every = Convert.ToInt32(txtEvery.Text);
            _executionDuration = Convert.ToInt32(txtDuration.Text);
            txtNumberOfPkgs.Enabled = false;
            txtEvery.Enabled = false;
            txtDuration.Enabled = false;
            cmdStartThread.Enabled = false;
            cmdSelectedPkg.Enabled = false;
            _alertSendMultiple.RunWorkerAsync();
        }

        private void cmdRemoteCommandClear_Click(object sender, EventArgs e)
        {
            this.dgvPendingRemoteCommands.Rows.Clear();
            this.dgvPendingRemoteCommands.Rows.Add();
        }

        private void cmdGetQueuedRemoteCommand_Click(object sender, EventArgs e)
        {
            string sMsg = string.Empty;
            try
            {
                string sConStr = GetTDMConnectionString;
                if (!string.IsNullOrEmpty(sConStr))
                {
                    using (SqlConnection con = new SqlConnection(sConStr))
                    {
                        string sCmd = "select rcd_CommandId,drcd_Name,rcd_InstanceId,rcd_DeviceAssociationId,di_SerialNumber," +
                                        " di_LogicalDeviceId,di_IPAddress,di_Description,di_MACAddress" +
                                        ",di_Model,di_Manufacturer,di_DeviceType,di_HostName,di_SoftwareVersion," +
                                        " di_ServiceStatus,di_IconStatus from RemoteCommandDetails" +
                                        " INNER JOIN DeviceRemoteCommandDefinition on rcd_CommandId = drcd_CommandId" +
                                        " INNER JOIN DeviceInfo ON rcd_DeviceAssociationId = di_AssociationId" +
                                        " where rcd_StatusId = 10";
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(sCmd, con))
                        {
                            using (IDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader != null)
                                {
                                    int row = 0;
                                    while (reader.Read())
                                    {
                                        this.dgvPendingRemoteCommands.Rows.Add();
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_CHK].Value = false;
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_ID].Value = Convert.ToString(reader["rcd_CommandId"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_NAME].Value = Convert.ToString(reader["drcd_Name"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_INSTNCE_ID].Value = Convert.ToString(reader["rcd_InstanceId"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICEID].Value = Convert.ToString(reader["rcd_DeviceAssociationId"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SERIAL_NUMBER].Value = Convert.ToString(reader["di_SerialNumber"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_LOGICALID].Value = Convert.ToString(reader["di_LogicalDeviceId"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_IP].Value = Convert.ToString(reader["di_IPAddress"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DESC].Value = Convert.ToString(reader["di_Description"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MAC].Value = Convert.ToString(reader["di_MACAddress"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MODEL].Value = Convert.ToString(reader["di_Model"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MANF].Value = Convert.ToString(reader["di_Manufacturer"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICE_TYPE].Value = Convert.ToString(reader["di_DeviceType"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_HOST].Value = Convert.ToString(reader["di_HostName"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SW_VER].Value = Convert.ToString(reader["di_SoftwareVersion"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SERVICE_STATUS].Value = Convert.ToString(reader["di_ServiceStatus"]);
                                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_ICON].Value = Convert.ToString(reader["di_IconStatus"]);
                                        row++;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    sMsg = "Invalid TDM SQL connection string";
                }
            }
            catch (Exception ex)
            {
                sMsg = ex.ToString();
            }
            MessageBox.Show(sMsg);
        }

        private void cmdRemoteCommandExecute_Click(object sender, EventArgs e)
        {
            List<DescribeDevicePackageType> describeDeviceColl = new List<DescribeDevicePackageType>();
            List<RemoteCommandResultType> remoteCommandResltLst = new List<RemoteCommandResultType>();
            for (int row = 0; row < dgvPendingRemoteCommands.Rows.Count; row++)
            {
                try
                {
                    if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_CHK] != null &&
                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_CHK].Value != null &&
                        (bool)this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_CHK].Value == true &&
                        this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_ID].Value != null)
                    {
                        DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICEID].Value != null)
                        {
                            describeDevice.DeviceGUID = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICEID].Value);
                        }
                        DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DESC].Value != null)
                        {
                            deviceDescriptionType.Description = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DESC].Value);
                        }

                        deviceDescriptionType.DeviceGUID = describeDevice.DeviceGUID;
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_IP].Value != null)
                        {
                            deviceDescriptionType.IPAddress = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_IP].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_LOGICALID].Value != null)
                        {
                            deviceDescriptionType.LogicalDeviceID = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_LOGICALID].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MAC].Value != null)
                        {
                            deviceDescriptionType.MACAddress = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MAC].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MANF].Value != null)
                        {
                            deviceDescriptionType.Manufacturer = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MANF].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MODEL].Value != null)
                        {
                            deviceDescriptionType.Model = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_MODEL].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SERIAL_NUMBER].Value != null)
                        {
                            deviceDescriptionType.SerialNumber = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SERIAL_NUMBER].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_HOST].Value != null)
                        {
                            deviceDescriptionType.HostName = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_HOST].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SW_VER].Value != null)
                        {
                            deviceDescriptionType.SoftwareVersion = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_SW_VER].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICE_TYPE].Value != null)
                        {
                            deviceDescriptionType.Type = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_DEVICE_TYPE].Value);
                        }
                        describeDevice.DeviceDescription = deviceDescriptionType;

                        DeviceStatusesType deviceStatusType = new DeviceStatusesType();
                        deviceStatusType.ServiceStatus = 0;
                        deviceStatusType.Status = 4;
                        deviceStatusType.StatusDate = DateTime.Now;

                        describeDevice.DeviceStatus = deviceStatusType;
                        describeDeviceColl.Add(describeDevice);
                        RemoteCommandResultType resultType = new RemoteCommandResultType();
                        resultType.Status = 2;
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_ID].Value != null)
                        {
                            resultType.ID = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_ID].Value);
                        }
                        if (this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_INSTNCE_ID].Value != null)
                        {
                            resultType.InstanceID = Convert.ToString(this.dgvPendingRemoteCommands.Rows[row].Cells[GRD_RMCD_INSTNCE_ID].Value);
                        }
                        resultType.Message = "Reply send by simulator";
                        resultType.Status = 2;
                        remoteCommandResltLst.Add(resultType);
                    }
                }
                catch { }
            }
            //for (int i = 0; i < describeDeviceColl.Count; i++)
            //{
            //    RemoteCommandResultType _remoteCmdResult = new RemoteCommandResultType();
            //    _remoteCmdResult = remoteCommandResltLst[i];
            //    proxy.RemoteCommandReply(_remoteCmdResult);
            //    DescribeDevicePackageType _describeDevicePkgType = new DescribeDevicePackageType();
            //    _describeDevicePkgType = describeDeviceColl[i];
            //    proxy.DescribeDevice(_describeDevicePkgType);
            //}


            try
            {
                int j = 0;
                foreach (var item in describeDeviceColl)
                {
                    j = j + 1;
                    Thread SenddescribeDevicePackageType = new Thread(() => { LogAndSendDescribeDevicePackage(item, "Thread" + j.ToString()); });
                    SenddescribeDevicePackageType.Name = "Thread" + j.ToString();
                    SenddescribeDevicePackageType.Start();
                    System.Threading.Thread.Sleep(20);
                }
                j = 0;
                foreach (var item in remoteCommandResltLst)
                {
                    j = j + 1;
                    Thread SendRemoteCommandResult = new Thread(() => { LogAndSendRemoteCommandReplyPackage(item, "Thread" + j.ToString()); });
                    SendRemoteCommandResult.Name = "Thread" + j.ToString();
                    SendRemoteCommandResult.Start();
                    System.Threading.Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            System.Threading.Thread.Sleep(4000);
            _ManualResetEvent.Set();
            System.Threading.Thread.Sleep(4000);
            _ManualResetEvent.Reset();
        }

        private void cmdDeviceComponentChange_Performance_Click(object sender, EventArgs e)
        {
            List<ComponentPackageType> _componentPackageLst = new List<ComponentPackageType>();
            for (int row = 0; row < dgvReportDeviceComponentChange.Rows.Count; row++)
            {
                try
                {
                    if (
                        this.dgvReportDeviceComponentChange.Rows[row].Cells[0].Value != null &&
                        this.dgvReportDeviceComponentChange.Rows[row].Cells[1].Value != null &&
                        this.dgvReportDeviceComponentChange.Rows[row].Cells[2].Value != null &&
                        this.dgvReportDeviceComponentChange.Rows[row].Cells[3].Value != null &&
                        (Convert.ToString((dgvReportDeviceComponentChange.Rows[row].Cells[7] as DataGridViewComboBoxCell).FormattedValue.ToString()) == "Add" || Convert.ToString((dgvReportDeviceComponentChange.Rows[row].Cells[7] as DataGridViewComboBoxCell).FormattedValue.ToString()) == "Remove")
                        )
                    {
                        ComponentType[] Arrcomponents = new ComponentType[1];
                        ComponentPackageType ComponentPackage = new ComponentPackageType();
                        ComponentPackage.DeviceGUID = this.dgvReportDeviceComponentChange.Rows[row].Cells[0].Value.ToString();
                        ComponentType oComponentType = new ComponentType();
                        oComponentType.SerialNumber = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[1].Value);
                        oComponentType.Type = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[2].Value);
                        oComponentType.Manufacturer = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[3].Value);
                        oComponentType.Model = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[4].Value);
                        oComponentType.Description = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[5].Value);
                        oComponentType.UserName = Convert.ToString(this.dgvReportDeviceComponentChange.Rows[row].Cells[6].Value);
                        oComponentType.Action = Convert.ToString((this.dgvReportDeviceComponentChange.Rows[row].Cells[7] as DataGridViewComboBoxCell).FormattedValue.ToString()) == "Add" ? 0 : 1;
                        oComponentType.StatusDate = Convert.ToDateTime(this.dgvReportDeviceComponentChange.Rows[row].Cells[8].Value.ToString());
                        Arrcomponents[0] = oComponentType;
                        ComponentPackage.Components = Arrcomponents;
                        _componentPackageLst.Add(ComponentPackage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {
                int i = 0;
                foreach (var item in _componentPackageLst)
                {
                    i = i + 1;
                    Thread SendComponentPackageChange = new Thread(() => { proxy.ReportDeviceComponentChanges(item); });
                    SendComponentPackageChange.Name = "Thread" + i.ToString();
                    SendComponentPackageChange.Start();
                    System.Threading.Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Set();
            System.Threading.Thread.Sleep(2000);
            _ManualResetEvent.Reset();
        }


        public struct LogMessage
        {
            public string GUID;
            public string Message;

            public LogMessage(string guid, string message)
            {
                GUID = guid;
                Message = message;
            }
        };

        public static Queue<LogMessage> MessageQueue = new Queue<LogMessage>();
        public static void DumpLogMessage(LogMessage LogItem)
        {
            try
            {
                lock (MessageQueue)
                {
                    MessageQueue.Enqueue(LogItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void LogWriter()
        {

            try
            {
                while (true)
                {
                    lock (MessageQueue)
                    {
                        while (MessageQueue.Count > 0)
                        {
                            LogMessage LogItem = MessageQueue.Dequeue();
                            using (StreamWriter writer = new StreamWriter(Path_Current_App + "\\" + "Log.txt", true))
                            {
                                writer.WriteLine(DateTime.Now.ToString() + " : " +
                                    LogItem.GUID + " : " + LogItem.Message);
                            }
                            Thread.Sleep(50);
                        }
                    }
                    Thread.Sleep(500);

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cmdRowAdd_Performance_Component_Click_Click(object sender, EventArgs e)
        {
            dgvReportDeviceComponentChange.Rows.Clear();
            if (Convert.ToInt32(txtRowCount_Performance_Component.Text) > 10)
            {
                txtRowCount_Performance_Component.Text = "10";
            }
            int nMaxRowCount = string.IsNullOrEmpty(txtRowCount_Performance_Component.Text) ? 0 : Convert.ToInt32(txtRowCount_Performance_Component.Text);
            for (int i = 0; i < nMaxRowCount; i++)
            {
                this.dgvReportDeviceComponentChange.Rows.Add();
                this.dgvReportDeviceComponentChange.Rows[i].Cells[7].Value = "Add";
                this.dgvReportDeviceComponentChange.Rows[i].Cells[8].Value = "2015-09-10T17:10:28.4540793+05:30";
            }
        }

        private void txtRowCount_Performance_Component_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmdClearDeviceComponentChange_Performance_Click_Click(object sender, EventArgs e)
        {
            this.dgvReportDeviceComponentChange.Rows.Clear();
        }

        public void LogAndSendDescribeDevicePackage(DescribeDevicePackageType _DescribeDevicePackageType, string threadid)
        {
            _ManualResetEvent.WaitOne();
            try
            {
                //DumpLogMessage(new LogMessage(_DescribeDevicePackageType.DeviceGUID, "Started DescribeDevice for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
                proxy.DescribeDevice(_DescribeDevicePackageType);
                //DumpLogMessage(new LogMessage(_DescribeDevicePackageType.DeviceGUID, "Ended DescribeDevice for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LogAndSendComponentPackageChange(ComponentPackageType _ComponentPackageType, string threadid)
        {
            _ManualResetEvent.WaitOne();
            try
            {
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Started Device ComponentPackage change for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
                proxy.ReportDeviceComponentChanges(_ComponentPackageType);
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Ended Device ComponentPackage change for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LogAndSendAlertPackage(AlertPackageType _AlertPackageType, string threadid)
        {
            _ManualResetEvent.WaitOne();
            try
            {
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Started Device Alert for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
                proxy.SendAlert(_AlertPackageType);
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Ended Device Alert for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LogAndSendRemoteCommandReplyPackage(RemoteCommandResultType _RemoteCommandResultType, string threadid)
        {
            _ManualResetEvent.WaitOne();
            try
            {
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Started Device RemoteCommandReply for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
                proxy.RemoteCommandReply(_RemoteCommandResultType);
                //DumpLogMessage(new LogMessage(_ComponentPackageType.DeviceGUID, "Ended Device RemoteCommandReply for " + threadid + " @ " + System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void tbbulkdescribedevice_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sMsg = string.Empty;
        //        DataSet ds = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        string sConStr = GetTDMConnectionString;
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(sConStr))
        //            {
        //                using (SqlConnection con = new SqlConnection(sConStr))
        //                {
        //                    string sCmd = "select di_AssociationId,di_LogicalDeviceId,di_DeviceType,di_Manufacturer,di_Model,di_SerialNumber " +
        //                        ",di_MACAddress,di_HostName,di_IPAddress,di_Description,di_SoftwareVersion,di_InstanceID " +
        //                        " from deviceinfo with (nolock)";
        //                    con.Open();
        //                    using (SqlCommand cmd = new SqlCommand(sCmd, con))
        //                    {
        //                        cmd.CommandText = sCmd;
        //                        da.SelectCommand = cmd;
        //                        da.Fill(ds);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                sMsg = "Invalid TDM SQL connection string";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            sMsg = ex.ToString();
        //        }
        //        DataRow dr = ds.Tables[0].NewRow();
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            Random r = new Random();
        //            int rInt = r.Next(0, ds.Tables[0].Rows.Count);
        //            dr = ds.Tables[0].Rows[rInt];//Random DeviceGuid 

        //            for (int i = 0; i < 10000; i++)
        //            {
        //                foreach (DataRow _row in ds.Tables[0].Rows)
        //                {
        //                    DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
        //                    describeDevice.DeviceGUID = Convert.ToString(_row["di_AssociationId"]);
        //                    DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
        //                    deviceDescriptionType.Description = "Test";
        //                    deviceDescriptionType.DeviceGUID = Convert.ToString(_row["di_AssociationId"]);
        //                    deviceDescriptionType.IPAddress = Convert.ToString(_row["di_IPAddress"]);
        //                    deviceDescriptionType.LogicalDeviceID = Convert.ToString(_row["di_LogicalDeviceId"]);
        //                    deviceDescriptionType.MACAddress = Convert.ToString(_row["di_MACAddress"]);
        //                    deviceDescriptionType.Manufacturer = Convert.ToString(_row["di_Manufacturer"]);
        //                    deviceDescriptionType.Model = Convert.ToString(_row["di_Model"]);
        //                    deviceDescriptionType.SerialNumber = Convert.ToString(_row["di_SerialNumber"]);
        //                    deviceDescriptionType.HostName = Convert.ToString(_row["di_HostName"]);
        //                    deviceDescriptionType.SoftwareVersion = Convert.ToString(_row["di_SoftwareVersion"]);
        //                    deviceDescriptionType.Type = Convert.ToString(_row["di_DeviceType"]);
        //                    describeDevice.DeviceDescription = deviceDescriptionType;
        //                    DeviceStatusesType deviceStatusType = new DeviceStatusesType();
        //                    deviceStatusType.ServiceStatus = 1;
        //                    deviceStatusType.Status = 2;
        //                    deviceStatusType.StatusDate = DateTime.Now;
        //                    describeDevice.DeviceStatus = deviceStatusType;

        //                    describeDevice.RemoteAlerts = new RemoteAlertsInstanceType();
        //                    describeDevice.RemoteAlerts.IsSnapShotSpecified = true;
        //                    describeDevice.RemoteAlerts.IsSnapShot = true;

        //                    //describeDevice.RemoteAlerts = new RemoteAlertsInstanceType();
        //                    //RemoteAlertInstanceType _remoteAlertInstanceType = new RemoteAlertInstanceType();
        //                    //if (Convert.ToString(dr["di_DeviceType"]).ToLower() == "fvd")
        //                    //{
        //                    //    _remoteAlertInstanceType.ComponentDeviceType = Convert.ToString(dr["di_DeviceType"]);
        //                    //    _remoteAlertInstanceType.ComponentSerialNumber = Convert.ToString(dr["di_SerialNumber"]);
        //                    //    _remoteAlertInstanceType.ID = "7587CD17-2856-4B8F-BD8E-512D586F8B31";// Convert.ToString(dr["di_InstanceID"]);
        //                    //    _remoteAlertInstanceType.IsAlertRemoved = false;
        //                    //    _remoteAlertInstanceType.Message = "The print head temperature is below the required value.";
        //                    //    describeDevice.RemoteAlerts.RemoteAlert[0] = _remoteAlertInstanceType;
        //                    //}

        //                    proxy.DescribeDevice(describeDevice);
        //                }
        //                //DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
        //                //describeDevice.DeviceGUID = Convert.ToString(dr["di_AssociationId"]);
        //                //DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
        //                //deviceDescriptionType.Description = "Test";
        //                //deviceDescriptionType.DeviceGUID = Convert.ToString(dr["di_AssociationId"]);
        //                //deviceDescriptionType.IPAddress = Convert.ToString(dr["di_IPAddress"]);
        //                //deviceDescriptionType.LogicalDeviceID = Convert.ToString(dr["di_LogicalDeviceId"]);
        //                //deviceDescriptionType.MACAddress = Convert.ToString(dr["di_MACAddress"]);
        //                //deviceDescriptionType.Manufacturer = Convert.ToString(dr["di_Manufacturer"]);
        //                //deviceDescriptionType.Model = Convert.ToString(dr["di_Model"]);
        //                //deviceDescriptionType.SerialNumber = Convert.ToString(dr["di_SerialNumber"]);
        //                //deviceDescriptionType.HostName = Convert.ToString(dr["di_HostName"]);
        //                //deviceDescriptionType.SoftwareVersion = Convert.ToString(dr["di_SoftwareVersion"]);
        //                //deviceDescriptionType.Type = Convert.ToString(dr["di_DeviceType"]);
        //                //describeDevice.DeviceDescription = deviceDescriptionType;
        //                //DeviceStatusesType deviceStatusType = new DeviceStatusesType();
        //                //deviceStatusType.ServiceStatus = 1;
        //                //deviceStatusType.Status = 2;
        //                //deviceStatusType.StatusDate = DateTime.Now;
        //                //describeDevice.DeviceStatus = deviceStatusType;


        //                //proxy.DescribeDevice(describeDevice);
        //            }
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        MessageBox.Show(ex.Message); 
        //    }
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string sMsg = string.Empty;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                string sConStr = GetTDMConnectionString;
                try
                {
                    if (!string.IsNullOrEmpty(sConStr))
                    {
                        using (SqlConnection con = new SqlConnection(sConStr))
                        {
                            string sCmd = "select di_AssociationId,di_LogicalDeviceId,di_DeviceType,di_Manufacturer,di_Model,di_SerialNumber " +
                                ",di_MACAddress,di_HostName,di_IPAddress,di_Description,di_SoftwareVersion,di_InstanceID " +
                                " from deviceinfo with (nolock)";
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand(sCmd, con))
                            {
                                cmd.CommandText = sCmd;
                                da.SelectCommand = cmd;
                                da.Fill(ds);
                            }
                        }
                    }
                    else
                    {
                        sMsg = "Invalid TDM SQL connection string";
                    }
                }
                catch (Exception ex)
                {
                    sMsg = ex.ToString();
                }
                DataRow dr = ds.Tables[0].NewRow();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, ds.Tables[0].Rows.Count);
                    dr = ds.Tables[0].Rows[rInt];//Random DeviceGuid 
                    if (string.IsNullOrEmpty(txtDevice.Text))
                    {
                        MessageBox.Show("Please enter device count");
                    }

                    else
                    {
                        int devicecount = Convert.ToInt32(txtDevice.Text);

                        //for (int i = 0; i < devicecount; i++)
                        for (int i = 0; i < devicecount; i++)
                        {
                            foreach (DataRow _row in ds.Tables[0].Rows)
                            {
                                try
                                {


                                    DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
                                    describeDevice.DeviceGUID = Convert.ToString(_row["di_AssociationId"]);
                                    DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
                                    deviceDescriptionType.Description = "Test";
                                    deviceDescriptionType.DeviceGUID = Convert.ToString(_row["di_AssociationId"]);
                                    deviceDescriptionType.IPAddress = Convert.ToString(_row["di_IPAddress"]);
                                    deviceDescriptionType.LogicalDeviceID = Convert.ToString(_row["di_LogicalDeviceId"]);
                                    deviceDescriptionType.MACAddress = Convert.ToString(_row["di_MACAddress"]);
                                    deviceDescriptionType.Manufacturer = Convert.ToString(_row["di_Manufacturer"]);
                                    deviceDescriptionType.Model = Convert.ToString(_row["di_Model"]);
                                    deviceDescriptionType.SerialNumber = Convert.ToString(_row["di_SerialNumber"]);
                                    deviceDescriptionType.HostName = Convert.ToString(_row["di_HostName"]);
                                    deviceDescriptionType.SoftwareVersion = Convert.ToString(_row["di_SoftwareVersion"]);
                                    deviceDescriptionType.Type = Convert.ToString(_row["di_DeviceType"]);
                                    describeDevice.DeviceDescription = deviceDescriptionType;
                                    DeviceStatusesType deviceStatusType = new DeviceStatusesType();
                                    deviceStatusType.ServiceStatus = 1;
                                    deviceStatusType.Status = 2;
                                    deviceStatusType.StatusDate = DateTime.Now;
                                    describeDevice.DeviceStatus = deviceStatusType;

                                    describeDevice.DeviceGUID = describeDevice.DeviceGUID;
                                    describeDevice.RemoteAlerts = new RemoteAlertsInstanceType();
                                    describeDevice.RemoteAlerts.IsSnapShotSpecified = true;
                                    describeDevice.RemoteAlerts.IsSnapShot = true;

                                    //describeDevice.RemoteAlerts = new RemoteAlertsInstanceType();
                                    //RemoteAlertInstanceType _remoteAlertInstanceType = new RemoteAlertInstanceType();
                                    //if (Convert.ToString(dr["di_DeviceType"]).ToLower() == "fvd")
                                    //{
                                    //    _remoteAlertInstanceType.ComponentDeviceType = Convert.ToString(dr["di_DeviceType"]);
                                    //    _remoteAlertInstanceType.ComponentSerialNumber = Convert.ToString(dr["di_SerialNumber"]);
                                    //    _remoteAlertInstanceType.ID = "7587CD17-2856-4B8F-BD8E-512D586F8B31";// Convert.ToString(dr["di_InstanceID"]);
                                    //    _remoteAlertInstanceType.IsAlertRemoved = false;
                                    //    _remoteAlertInstanceType.Message = "The print head temperature is below the required value.";
                                    //    describeDevice.RemoteAlerts.RemoteAlert[0] = _remoteAlertInstanceType;
                                    //}

                                    proxy.DescribeDevice(describeDevice);

                                }
                                catch (Exception ex )
                                {
                                    MessageBox.Show(ex.ToString());
                                   
                                }
                            }
                            //DescribeDevicePackageType describeDevice = new DescribeDevicePackageType();
                            //describeDevice.DeviceGUID = Convert.ToString(dr["di_AssociationId"]);
                            //DeviceDescriptionType deviceDescriptionType = new DeviceDescriptionType();
                            //deviceDescriptionType.Description = "Test";
                            //deviceDescriptionType.DeviceGUID = Convert.ToString(dr["di_AssociationId"]);
                            //deviceDescriptionType.IPAddress = Convert.ToString(dr["di_IPAddress"]);
                            //deviceDescriptionType.LogicalDeviceID = Convert.ToString(dr["di_LogicalDeviceId"]);
                            //deviceDescriptionType.MACAddress = Convert.ToString(dr["di_MACAddress"]);
                            //deviceDescriptionType.Manufacturer = Convert.ToString(dr["di_Manufacturer"]);
                            //deviceDescriptionType.Model = Convert.ToString(dr["di_Model"]);
                            //deviceDescriptionType.SerialNumber = Convert.ToString(dr["di_SerialNumber"]);
                            //deviceDescriptionType.HostName = Convert.ToString(dr["di_HostName"]);
                            //deviceDescriptionType.SoftwareVersion = Convert.ToString(dr["di_SoftwareVersion"]);
                            //deviceDescriptionType.Type = Convert.ToString(dr["di_DeviceType"]);
                            //describeDevice.DeviceDescription = deviceDescriptionType;
                            //DeviceStatusesType deviceStatusType = new DeviceStatusesType();
                            //deviceStatusType.ServiceStatus = 1;
                            //deviceStatusType.Status = 2;
                            //deviceStatusType.StatusDate = DateTime.Now;
                            //describeDevice.DeviceStatus = deviceStatusType;


                            //proxy.DescribeDevice(describeDevice);
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
