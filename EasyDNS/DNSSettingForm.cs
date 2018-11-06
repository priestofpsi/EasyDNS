using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace theDiary.EasyDNS.Windows
{
    public partial class DNSSettingForm : BorderlessForm, IModalValue
    {
        public DNSSettingForm()
        {
            InitializeComponent();
            this.FormClosing += (s, e) => e.Cancel = this.DialogResult != DialogResult.Cancel && !this.ValidateChildren();
            this.lblClose.Click += (s, e) => this.btnCancel.PerformClick();
            this.txtName.Validating += this.ValidateName;
            this.primaryDNS.Validating += this.ValidateIPAddress;

            this.Shown += (s, e) =>
            {
                string titleText = this.value == null? "New" : "Edit";
                this.lblTitle.Text = $"{titleText} DNS Configuration";
                if (this.value == null)
                    this.value = new DNSSetting();

                //this.txtName.DataBindings.Add(new Binding("Text", this.Value, "Name", false, DataSourceUpdateMode.OnValidation));
                //this.primaryDNS.DataBindings.Add(new Binding("Text", this.Value, "PrimaryDNSText", false, DataSourceUpdateMode.OnValidation));
                //this.secondaryDNS.DataBindings.Add(new Binding("Text", this.Value, "SecondaryDNSText", false, DataSourceUpdateMode.OnValidation));
            };
        }

        private void ValidateIPAddress(object sender, CancelEventArgs e)
        {
            
            
        }

        private void ValidateName(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Value.Name))
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.txtName, "The Name is required.");
                this.txtName.Focus();
            }
        }

        private DNSSetting value;
        public DNSSetting Value
        {
            get
            {
                if (this.DialogResult != DialogResult.Cancel)
                {
                    this.value.Name = this.txtName.Text;
                    this.value.PrimaryDNS = this.primaryDNS.IPAddress;
                    this.value.SecondaryDNS = this.secondaryDNS.IPAddress;
                }
                return this.value;
            }
            set
            {
                this.value = value;
                this.txtName.Text = this.value.Name;
                this.primaryDNS.Text = this.value.PrimaryDNS?.ToString();
                this.secondaryDNS.Text = this.value.SecondaryDNS?.ToString();
            }
        }

        dynamic IModalValue.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = value;
            }
        }

        private void btnSave_Validating(object sender, CancelEventArgs e)
        {
            
        }
    }
}
