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
    public partial class DNSSettingForm
        : BorderlessForm, IModalValue
    {
        public DNSSettingForm()
        {
            InitializeComponent();
            this.FormClosing += (s, e) => e.Cancel = this.DialogResult != DialogResult.Cancel && !this.ValidateChildren();
            this.imgClose.Click += (s, e) => this.btnCancel.PerformClick();
            this.txtName.Validating += this.ValidateName;

            this.Shown += (s, e) =>
            {
                string titleText = this.value == null? "New" : "Edit";
                this.lblTitle.Text = $"{titleText} DNS Configuration";
                if (this.value == null)
                    this.value = new DNSSetting();
            };
        }

        #region Private Declarations
        private DNSSetting value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="DNSSetting"/> instance to be edited.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the<see cref= "DNSSetting" /> instance to be edited, for the <see cref="IModalValue"/> implementation.
        /// </summary>
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
        #endregion

        #region Private Methods &* Functions
        private void ValidateName(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Value.Name))
                return;

            e.Cancel = true;
            this.errorProvider.SetError(this.txtName, "The Name is required.");
            this.txtName.Focus();
        }
        #endregion

        
    }
}
