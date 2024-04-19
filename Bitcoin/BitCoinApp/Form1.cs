using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BitcoinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetRates_Click(object sender, EventArgs e)
        {
            if (currencyCombo.SelectedItem != null)
                if (currencyCombo.SelectedItem.ToString() == "EUR")
                {
                    ResultLabel.Visible = true;
                    ResultTextBox.Visible = true;
                    BitCoinRates bitcoin = GetRates();
                    float result;
                    if (bitcoin != null)
                    {
                        if (float.TryParse(amountOfCoinBox.Text, out result))
                        {
                            float x = result * bitcoin.bpi.EUR.rate_float;
                            ResultTextBox.Text = $"{x.ToString()} {bitcoin.bpi.EUR.code}";
                        }
                        else
                        {
                            ResultTextBox.Text = "Peab olema number";
                        }

                    }

                }
            if (currencyCombo.SelectedItem.ToString() == "USD")
            {
                ResultLabel.Visible = true;
                ResultTextBox.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result;
                if (bitcoin != null)
                {
                    if (float.TryParse(amountOfCoinBox.Text, out result))
                    {
                        float x = result * bitcoin.bpi.USD.rate_float;
                        ResultTextBox.Text = $"{x.ToString()} {bitcoin.bpi.USD.code}";
                    }
                    else
                    {
                        ResultTextBox.Text = "Peab olema number";
                    }
                    
                }

            }
            else
            { ResultLabel.Visible = false; }
        }

        public static BitCoinRates GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRates bitcoin;
            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRates>(response);
            }

            return bitcoin;
        }
    }
}
