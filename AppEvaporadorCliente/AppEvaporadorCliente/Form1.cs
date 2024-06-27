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
                readPVTimer.Start();
                //ReadNodeValue();
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Status: Falha ({ex.Message})";
                labelStatus.BackColor = Color.Red;
            }
        }

        private void readPVTimer_Tick(object sender, EventArgs e)
        {
            ReadPVNodeValue();
        }
        private void readMVTimer_Tick(object sender, EventArgs e)
        {
            ReadMVNodeValue();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {  
                readMVTimer.Start();
            }
            else
            {
                readMVTimer.Stop();
            }
            
        }

        private void ReadPVNodeValue()
        {

            // Cada um dos cinco try seguintes realiza a leitura da variável de saída do LT-001
            try
            {
                var nodeId = new NodeId("ns=2;i=292");
                var dataValue = session.ReadValue(nodeId);
                textBox4.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                labelStatus2.Text = $"Status: Leitura Realizada";
                labelStatus2.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                labelStatus2.Text = $"Status: Falha na leitura ({ex.Message})";
                labelStatus2.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=292");
                var dataValue = session.ReadValue(nodeId);
                textBox6.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label7.Text = $"Status: Leitura Realizada";
                label7.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label7.Text = $"Status: Falha na leitura ({ex.Message})";
                label7.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=292");
                var dataValue = session.ReadValue(nodeId);
                textBox8.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label11.Text = $"Status: Leitura Realizada";
                label11.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label11.Text = $"Status: Falha na leitura ({ex.Message})";
                label11.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=292");
                var dataValue = session.ReadValue(nodeId);
                textBox10.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label15.Text = $"Status: Leitura Realizada";
                label15.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label15.Text = $"Status: Falha na leitura ({ex.Message})";
                label15.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=292");
                var dataValue = session.ReadValue(nodeId);
                textBox12.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label19.Text = $"Status: Leitura Realizada";
                label19.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label19.Text = $"Status: Falha na leitura ({ex.Message})";
                label19.BackColor = Color.Red;
            }
            
            // Leitura da variável de saída TT-001 
            try
            {
                var nodeId = new NodeId("ns=2;i=262"); // TT-001
                var dataValue = session.ReadValue(nodeId);
                textBox14.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label23.Text = $"Status: Leitura Realizada";
                label23.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label23.Text = $"Status: Falha na leitura ({ex.Message})";
                label23.BackColor = Color.Red;
            }
        }

        private void ReadMVNodeValue()
        {
            // Aqui são realizadas as leituras das variáveis de entrada
            try
            {
                var nodeId = new NodeId("ns=2;i=301"); // SC-001
                var dataValue = session.ReadValue(nodeId);
                textBox3.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                labelStatus25.Text = $"Status: Leitura Realizada";
                labelStatus25.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                labelStatus25.Text = $"Status: Falha na leitura ({ex.Message})";
                labelStatus25.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=344"); // SC-002
                var dataValue = session.ReadValue(nodeId);
                textBox5.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label5.Text = $"Status: Leitura Realizada";
                label5.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label5.Text = $"Status: Falha na leitura ({ex.Message})";
                label5.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=309"); // SC-003
                var dataValue = session.ReadValue(nodeId);
                textBox7.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label9.Text = $"Status: Leitura Realizada";
                label9.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label9.Text = $"Status: Falha na leitura ({ex.Message})";
                label9.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=331"); // FV-001
                var dataValue = session.ReadValue(nodeId);
                textBox9.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label13.Text = $"Status: Leitura Realizada";
                label13.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label13.Text = $"Status: Falha na leitura ({ex.Message})";
                label13.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=317"); // FV-002
                var dataValue = session.ReadValue(nodeId);
                textBox11.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label17.Text = $"Status: Leitura Realizada";
                label17.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label17.Text = $"Status: Falha na leitura ({ex.Message})";
                label17.BackColor = Color.Red;
            }

            try
            {
                var nodeId = new NodeId("ns=2;i=270"); // TV-001
                var dataValue = session.ReadValue(nodeId);
                textBox13.Text = dataValue.Value != null ? dataValue.Value.ToString() : "null";
                label21.Text = $"Status: Leitura Realizada";
                label21.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                label21.Text = $"Status: Falha na leitura ({ex.Message})";
                label21.BackColor = Color.Red;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=301"); // SC-001
                var value = double.Parse(textBox3.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    labelStatus25.Text = "Status: Escrita realizada";
                    labelStatus25.BackColor = Color.LightGreen;
                }
                else
                {
                    labelStatus25.Text = $"Status: Falha na escrita ({results[0]})";
                    labelStatus25.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                labelStatus25.Text = $"Status: Falha na escrita ({ex.Message})";
                labelStatus25.BackColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=344"); // SC-002
                var value = double.Parse(textBox5.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    label5.Text = "Status: Escrita realizada";
                    label5.BackColor = Color.LightGreen;
                }
                else
                {
                    label5.Text = $"Status: Falha na escrita ({results[0]})";
                    label5.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                label5.Text = $"Status: Falha na escrita ({ex.Message})";
                label5.BackColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=309"); // SC-003
                var value = double.Parse(textBox7.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    label9.Text = "Status: Escrita realizada";
                    label9.BackColor = Color.LightGreen;
                }
                else
                {
                    label9.Text = $"Status: Falha na escrita ({results[0]})";
                    label9.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                label9.Text = $"Status: Falha na escrita ({ex.Message})";
                label9.BackColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=331"); // FV-001
                var value = double.Parse(textBox9.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    label13.Text = "Status: Escrita realizada";
                    label13.BackColor = Color.LightGreen;
                }
                else
                {
                    label13.Text = $"Status: Falha na escrita ({results[0]})";
                    label13.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                label13.Text = $"Status: Falha na escrita ({ex.Message})";
                label13.BackColor = Color.Red;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=317"); // FV-002
                var value = double.Parse(textBox11.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    label17.Text = "Status: Escrita realizada";
                    label17.BackColor = Color.LightGreen;
                }
                else
                {
                    label17.Text = $"Status: Falha na escrita ({results[0]})";
                    label17.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                label17.Text = $"Status: Falha na escrita ({ex.Message})";
                label17.BackColor = Color.Red;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var nodeId = new NodeId("ns=2;i=270"); // TV-001
                var value = double.Parse(textBox13.Text);
                var dataValue = new DataValue(new Variant(value));
                session.Write(null, new WriteValueCollection { new WriteValue { NodeId = nodeId, AttributeId = Attributes.Value, Value = dataValue } }, out StatusCodeCollection results, out DiagnosticInfoCollection diagnosticInfos);

                if (StatusCode.IsGood(results[0]))
                {
                    label21.Text = "Status: Escrita realizada";
                    label21.BackColor = Color.LightGreen;
                }
                else
                {
                    label21.Text = $"Status: Falha na escrita ({results[0]})";
                    label21.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                label21.Text = $"Status: Falha na escrita ({ex.Message})";
                label21.BackColor = Color.Red;
            }
        }
    }
}
