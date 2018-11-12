using theDiary.EasyDNS.Windows.DNSService;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theDiary.EasyDNS.Windows
{

    public partial class FormMain : BorderlessForm, DNSService.IDNSServiceCallback
    {
        public FormMain()
            : base()
        {
            InitializeComponent();

            if (!Settings.Instance.FormSize.IsEmpty)
                this.Size = Settings.Instance.FormSize;

            this.imgMenu.Click += (s, e) => this.mainMenu.Show(Form.MousePosition);
            this.imgManageDNS.Click += (s, e) => this.manageDNSMenu.Show(Form.MousePosition);
            this.imgLogo.MouseDown += (s, e) => base.OnMouseDown(e);
            this.imgChangeDNS.Click += (s, e) => this.ChangeDeviceDNS();
            this.imgClose.Click += (s, e) => Application.Exit();
            this.exitToolStripMenuItem.Click += (s, e) => Application.Exit();
            this.Shown += this.Form1_Shown;
            this.FormClosing += (s, e) => Settings.Instance.FormSize = this.Size;

            Application.ApplicationExit += (s, e) => Settings.Save();

            this.newDNSSettingMenuItem.Click += (s, e) => this.ShowModalForm<DNSSettingForm>(null, this.HandleDNSSetting);
            this.editDNSSettingMenuItem.Click += (s, e) => this.ShowModalForm<DNSSettingForm>(this.SelectedDNSSetting, this.HandleDNSSetting);
            this.deleteDNSSettingMenuItem.Click += (s, e) =>
            {
                if (MessageBox.Show(this, $"Delete '{this.SelectedDNSSetting.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                Settings.Instance.DNSSettings.Remove(this.SelectedDNSSetting);
                var dnsSource = new BindingList<DNSSetting>(Settings.Instance.DNSSettings);
                this.cbDNSSettings.DataSource = new BindingSource() { DataSource = dnsSource };
            };
            this.cbDevices.SelectedIndexChanged += (s, e) => this.UpdateDeviceDNSStatus();
            this.cbDNSSettings.SelectedIndexChanged += (s, e) =>
            {
                this.lblNPrimaryDNS.Text = (this.SelectedDNSSetting != null && this.SelectedDNSSetting.SecondaryDNS != null) ? $"{this.SelectedDNSSetting.PrimaryDNS}" : string.Empty;
                this.lblNSecondaryDNS.Text = (this.SelectedDNSSetting != null && this.SelectedDNSSetting.SecondaryDNS != null) ? $"{this.SelectedDNSSetting.SecondaryDNS}" : string.Empty;
                this.imgChangeDNS.Visible = this.SelectedNetworkDevice != null && this.SelectedDNSSetting != null && !(this.SelectedDNSSetting.Equals(this.SelectedNetworkDevice.DNSConfiguration));
            };
        }

        #region Private Declarations & Constants
        private const string DefaultStatusMessage = "Ready";

        private DNSService.IDNSService proxy;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the selected <see cref="NetworkDevice"/>.
        /// </summary>
        public NetworkDevice SelectedNetworkDevice
        {
            get
            {
                NetworkDevice returnValue = null;
                this.Invoke(() => returnValue = this.cbDevices.SelectedItem as NetworkDevice);
                return returnValue;
            }
        }

        /// <summary>
        /// Gets the selected <see cref="DNSSetting"/>.
        /// </summary>
        public DNSSetting SelectedDNSSetting
        {
            get
            {
                DNSSetting returnValue = null;
                this.Invoke(() => returnValue = this.cbDNSSettings.SelectedItem as DNSSetting);
                return returnValue;
            }
        }
        #endregion

        #region Private Methods & Functions

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Settings.Instance.DNSSettings.Count == 0)
            {
                Settings.Instance.DNSSettings["Home"]  = new DNSSetting("Home", "192.168.0.1");
                Settings.Instance.DNSSettings["Google"] = new DNSSetting("Google", "8.8.8.8", "8.8.4.4");
                Settings.Save();
            }
            Task.Run(() => this.InitializeNetworkAdapters());
        }

        private void ChangeDeviceDNS(DNSSetting newSetting = null)
        {
            if (newSetting == null)
                newSetting = this.SelectedDNSSetting;

            Task.Run(() =>
            {
                this.UpdateStatusMessage($"Changing DNS to {newSetting.Name}...");
                this.proxy.ChangeDNS(newSetting, this.SelectedNetworkDevice.MACAddress.GetAddressBytes());
            });
        }

        private void UpdateDeviceDNSStatus(DNSConfiguration configuration = null)
        {
            Task.Run(() =>
            {
                BindDNSSettings();
                this.UpdateStatusMessage("Fetching DNS Configuration...");
                if (configuration == null)
                    configuration = this.proxy.GetDNSConfiguration(this.SelectedNetworkDevice.MACAddress.GetAddressBytes()).Result;
                this.Invoke(() =>
                {
                    this.lblCPrimaryDNS.Text = $"Primary: {configuration.PrimaryDNS}";
                    this.lblCSecondaryDNS.Text = (configuration.SecondaryDNS != null) ? $"Secondary: {configuration.SecondaryDNS}" : string.Empty;
                    this.pnlDeviceDNS.Visible = (this.SelectedNetworkDevice != null);
                });
                this.UpdateStatusMessage();
            });
        }

        /// <summary>
        /// Initializes the <see cref="IDNSService"/> proxy service.
        /// </summary>
        private void InitializeProxyService()
        {
            if (this.proxy != null)
                return;

            this.UpdateStatusMessage("Finding EasyDNS Service...");
            var serviceAddress = this.FindDNSServiceAddress();
            this.UpdateStatusMessage("Connecting to EasyDNS Service...");
            this.proxy = this.CreateProxy(serviceAddress);
        }

        private EndpointAddress FindDNSServiceAddress()
        {
            if (!Settings.Instance.EndPointAddressUri.IsNullOrEmpty())
                return new EndpointAddress(Settings.Instance.EndPointAddressUri);

            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(DNSService.IDNSService)));
            if (findResponse.Endpoints.Count == 0)
                return null;

            Settings.Instance.EndPointAddressUri = findResponse.Endpoints.Last().Address.Uri;
            Settings.Save();

            return findResponse.Endpoints.Last().Address;
        }

        private DNSService.IDNSService CreateProxy(EndpointAddress address)
        {
            return new DuplexChannelFactory<DNSService.IDNSService>(this, new NetTcpBinding(), address).CreateChannel();
        }

        private void UpdateStatusMessage(string message = DefaultStatusMessage)
        {
            this.UpdateStatusMessage(message, SystemColors.WindowText);
        }

        private void UpdateStatusMessage(string message, System.Drawing.Color color)
        {
            this.Invoke(() =>
            {
                this.Cursor = DefaultStatusMessage.Equals(message) || color != SystemColors.WindowText ? Cursors.Default : Cursors.AppStarting;
                this.lblStatus.ForeColor = color;
                this.lblStatus.Text = message;
            });
        }

        private DialogResult ShowModalForm<T>(dynamic arg = null, ModalValueHandler handler = null)
            where T : Form, new()
        {
            using (T form = new T())
            {
                var originalColor = this.BackColor;
                this.BackColor = Color.Silver;
                form.StartPosition = FormStartPosition.CenterParent;
                if (arg != null && form is IModalValue)
                    (form as IModalValue).Value = arg;

                DialogResult returnValue = form.ShowDialog(this);
                this.BackColor = originalColor;

                if (form is IModalValue && returnValue == DialogResult.OK)
                    handler?.Invoke(form, new EventArgs(), (form as IModalValue).Value);

                return returnValue;
            }
        }

        private void BindDNSSettings()
        {
            this.Invoke(() =>
            {
                this.cbDNSSettings.BeginUpdate();
                var dnsSource = new BindingList<DNSSetting>(Settings.Instance.DNSSettings);
                this.cbDNSSettings.DataSource = new BindingSource() { DataSource = dnsSource };
                if (this.SelectedNetworkDevice != null)
                    this.cbDNSSettings.SelectedItem = this.SelectedNetworkDevice.DNSConfiguration.GetDNSSetting();

                this.cbDNSSettings.EndUpdate();
                this.UpdateStatusMessage("Ready");
            });
        }

        private void InitializeNetworkAdapters(List<NetworkAdapterInfo> devices = null)
        {
            try
            {
                this.InitializeProxyService();
                this.UpdateStatusMessage("Fetching Devices...");
                if (devices.IsNullOrEmpty())
                    devices = this.proxy.GetNetworkAdapters();

                this.Invoke(() =>
                {
                    this.OnNetworkAdaptersChanged(devices);
                    if (this.SelectedNetworkDevice != null)
                        this.BindDNSSettings();
                });
            }
            catch (Exception error)
            {
                this.UpdateStatusMessage(error.Message, Color.Red);
            }
        }

        private void HandleDNSSetting(object sender, EventArgs e, dynamic value)
        {
            this.Invoke(() =>
            {
                this.cbDNSSettings.BeginUpdate();
                Settings.Instance.DNSSettings[value.Name] = value;
                var dnsSource = new BindingList<DNSSetting>(Settings.Instance.DNSSettings);
                this.cbDNSSettings.DataSource = new BindingSource() { DataSource = dnsSource };
                this.cbDNSSettings.EndUpdate();
            });
        }
        #endregion

        #region IDNSServiceCallback Methods & Functions
        public void OnNetworkAdaptersChanged(List<NetworkAdapterInfo> devices)
        {
            this.Invoke(() =>
            {
                this.cbDevices.BeginUpdate();
                this.cbDevices.Items.Clear();
                if (!devices.IsNullOrEmpty())
                {
                    foreach (var device in devices)
                        this.cbDevices.Items.Add(new NetworkDevice(device));
                    this.cbDevices.SelectedIndex = 0;
                }
                this.cbDevices.EndUpdate();
                if (this.SelectedNetworkDevice == null)
                {
                    this.imgChangeDNS.Visible = false;
                    this.UpdateStatusMessage("No network devices available or found", Color.Red);
                }

            });
        }
        
        void DNSService.IDNSServiceCallback.OnNetworkConfigurationChanged(NetworkAdapterInfo originalConfiguration, NetworkAdapterInfo newConfiguration)
        {
            Task.Run(() =>
            {
                this.Invoke(() =>
                {
                    this.UpdateStatusMessage("Flushing DNS Cache...");
                    FormMain.DnsFlushResolverCache();
                    this.UpdateStatusMessage("Fetching DNS Configuration...");
                    this.SelectedNetworkDevice.UpdateDNSConfiguration(newConfiguration.DNSConfiguration);
                    this.UpdateDeviceDNSStatus(newConfiguration.DNSConfiguration);
                    this.imgChangeDNS.Visible = !(this.SelectedDNSSetting.Equals(this.SelectedNetworkDevice.DNSConfiguration));
                    this.UpdateStatusMessage();
                });
            });
        }

        void DNSService.IDNSServiceCallback.OnPublicIPAddressChanged(IPAddress originalIPAddress, IPAddress newIPAddress)
        {

        }

        
        #endregion

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern UInt32 DnsFlushResolverCache();

        private delegate void ModalValueHandler(object sender, EventArgs e, dynamic value);
    }
}
