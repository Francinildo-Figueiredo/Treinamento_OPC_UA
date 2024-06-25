using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AppEvaporadorCliente
{
    public partial class Form1 : Form
    {
        //private ApplicationInstance application;
        private Session session;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var config = new ApplicationConfiguration()
            {
                ApplicationName = "AppEvaporadorClient",
                ApplicationUri = Utils.Format(@"urn:{0}:AppEvaporadorClient", System.Net.Dns.GetHostName()),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\MachineDefault", SubjectName = "AppEvaporadorClient" },
                    TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities" },
                    TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Applications" },
                    RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates" },
                    AutoAcceptUntrustedCertificates = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                TraceConfiguration = new TraceConfiguration()
            };
            config.Validate(ApplicationType.Client).GetAwaiter().GetResult();

            //if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            //{
               // config.CertificateValidator.CertificateValidation += (s, e) => { e.Accept = (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted); };
            //}

            var application = new ApplicationInstance
            {
                ApplicationName = "EvaporadorPlantaClient",
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = config
            };

            application.CheckApplicationInstanceCertificate(false, 2048).GetAwaiter().GetResult();
            string serverUrl = textBoxEndpoint.Text;

            try
            {
                //await application.LoadApplicationConfiguration(false);
                //await application.CheckApplicationInstanceCertificate(false, 0);

                var endpointDescription = CoreClientUtils.SelectEndpoint(serverUrl, false);
                var endpointConfiguration = EndpointConfiguration.Create(application.ApplicationConfiguration);
                var endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);
                session = await Session.Create(
                    application.ApplicationConfiguration,
                    endpoint,
                    false,
                    "AppEvaporadorCliente",
                    60000,
                    null,
                    null);

                labelStatus.Text = "Status: Conectado";
                labelStatus.BackColor = Color.LightGreen;
                ReadNodeValue();
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Status: Falha ({ex.Message})";
                labelStatus.BackColor = Color.Red;
            }
        }

        private void ReadNodeValue()
        {

            try
            {
                var nodeId = new NodeId("ns=2;i=294");
                var dataValue = session.ReadValue(nodeId);
                textBox4.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                labelStatus2.Text = $"Status: Sucesso!";
                labelStatus2.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                labelStatus2.Text = $"Status: Falha na leitura ({ex.Message})";
                labelStatus2.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=303");
                var dataValue = session.ReadValue(nodeId);
                textBox3.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                labelStatus25.Text = $"Status: Sucesso!";
                labelStatus25.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                labelStatus25.Text = $"Status: Falha na leitura ({ex.Message})";
                labelStatus25.BackColor = Color.Red;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
