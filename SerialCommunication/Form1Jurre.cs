using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SerialCommunication
{
    public partial class Form1Jurre : Form
    {
        private SerialPort serialPortArduino;

        public Form1Jurre()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                serialPortArduino = new SerialPort();
                serialPortArduino.ReadTimeout = 1000;
                serialPortArduino.WriteTimeout = 1000;

                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();
                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;

                comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf("115200");

                buttonConnect.BackColor = Color.Blue;
                buttonConnect.ForeColor = Color.White;
            }
            catch (Exception)
            { }
        }

        private void cboPoort_DropDown(object sender, EventArgs e)
        {
            try
            {
                string selected = (string)comboBoxPoort.SelectedItem;
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();

                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);

                comboBoxPoort.SelectedIndex = comboBoxPoort.Items.IndexOf(selected);
            }
            catch (Exception)
            {
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (serialPortArduino.IsOpen)
            {
                try
                {
                    serialPortArduino.Close();
                    radioButtonVerbonden.Checked = false;
                    buttonConnect.Text = "Connect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error closing port: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    serialPortArduino.PortName = comboBoxPoort.SelectedItem.ToString();
                    serialPortArduino.BaudRate = int.Parse(comboBoxBaudrate.SelectedItem.ToString());
                    serialPortArduino.DataBits = (int)numericUpDownDatabits.Value;

                    if (radioButtonParityEven.Checked)
                        serialPortArduino.Parity = Parity.Even;
                    else if (radioButtonParityOdd.Checked)
                        serialPortArduino.Parity = Parity.Odd;
                    else if (radioButtonParityMark.Checked)
                        serialPortArduino.Parity = Parity.Mark;
                    else if (radioButtonParitySpace.Checked)
                        serialPortArduino.Parity = Parity.Space;
                    else
                        serialPortArduino.Parity = Parity.None;

                    if (radioButtonStopbitsTwo.Checked)
                        serialPortArduino.StopBits = StopBits.Two;
                    else if (radioButtonStopbitsOnePointFive.Checked)
                        serialPortArduino.StopBits = StopBits.OnePointFive;
                    else if (radioButtonStopbitsNone.Checked)
                        serialPortArduino.StopBits = StopBits.None;
                    else
                        serialPortArduino.StopBits = StopBits.One;

                    if (radioButtonHandshakeXonXoff.Checked)
                        serialPortArduino.Handshake = Handshake.XOnXOff;
                    else if (radioButtonHandshakeRTSXonXoff.Checked)
                        serialPortArduino.Handshake = Handshake.RequestToSendXOnXOff;
                    else if (radioButtonHandshakeRTS.Checked)
                        serialPortArduino.Handshake = Handshake.RequestToSend;
                    else
                        serialPortArduino.Handshake = Handshake.None;

                    serialPortArduino.RtsEnable = checkBoxRtsEnable.Checked;
                    serialPortArduino.DtrEnable = checkBoxDtrEnable.Checked;

                    serialPortArduino.Open();
                    radioButtonVerbonden.Checked = true;
                    buttonConnect.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port: " + ex.Message);
                }
            }
        }

        private void checkBoxDigital2_Checked(object sender, EventArgs e)
        {
            try
            {
                if (!serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Serial port is not open. Please connect first.", "Connection Error");
                    checkBoxDigital2.Checked = false;
                    return;
                }

                string command = checkBoxDigital2.Checked ? "set d2 high" : "set d2 low";
                serialPortArduino.WriteLine(command);
             
            }
            catch (IOException ex)
            {
                MessageBox.Show("IO Error: " + ex.Message);
                checkBoxDigital2.Checked = false;
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Timeout Error: " + ex.Message);
                checkBoxDigital2.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                checkBoxDigital2.Checked = false;
            }
        }

        private void checkBoxDigital3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Serial port is not open. Please connect first.", "Connection Error");
                    checkBoxDigital3.Checked = false;
                    return;
                }

                string command = checkBoxDigital3.Checked ? "set d3 high" : "set d3 low";
                serialPortArduino.WriteLine(command);

            }
            catch (IOException ex)
            {
                MessageBox.Show("IO Error: " + ex.Message);
                checkBoxDigital3.Checked = false;
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Timeout Error: " + ex.Message);
                checkBoxDigital3.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                checkBoxDigital3.Checked = false;
            }
        }

        private void checkBoxDigital4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Serial port is not open. Please connect first.", "Connection Error");
                    checkBoxDigital4.Checked = false;
                    return;
                }

                string command = checkBoxDigital4.Checked ? "set d4 high" : "set d4 low";
                serialPortArduino.WriteLine(command);

            }
            catch (IOException ex)
            {
                MessageBox.Show("IO Error: " + ex.Message);
                checkBoxDigital4.Checked = false;
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Timeout Error: " + ex.Message);
                checkBoxDigital4.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                checkBoxDigital4.Checked = false;
            }
        }

        private void trackBarPWM9_Scroll(object sender, EventArgs e)
        {
            try
            {
                // 1. Controleer of de seriële connectie bestaat én open is
                if (serialPortArduino != null && serialPortArduino.IsOpen)
                {
                    // 2. Haal de waarde uit de trackbar
                    int value = trackBarPWM9.Value;

                    // 3. Bouw het commando
                    string command = $"set pwm9 {value}";

                    // 4. Verstuur het commando
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële verbinding met de Arduino.");
                }
            }
            catch (Exception ex)
            {
                // 5. Foutafhandeling
                MessageBox.Show($"Fout bij het versturen van PWM9-waarde: {ex.Message}");

            }
        }

        private void trackBarPWM10_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino != null && serialPortArduino.IsOpen)
                {
                    int value = trackBarPWM10.Value;
                    string command = $"set pwm10 {value}";
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële verbinding met de Arduino.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het versturen van PWM10-waarde: {ex.Message}");
            }
        }

        private void trackBarPWM11_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino != null && serialPortArduino.IsOpen)
                {
                    int value = trackBarPWM11.Value;
                    string command = $"set pwm11 {value}";
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële verbinding met de Arduino.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het versturen van PWM11-waarde: {ex.Message}");
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerOefening3.Enabled = (tabControl.SelectedIndex == 3);
            timerOefening4.Enabled = (tabControl.SelectedIndex == 4);
        }

        private void timerOefening3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    serialPortArduino.ReadExisting();
                    string commando = "get d5";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital5.Checked = (antwoord == "1");

                    commando = "get d6";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital6.Checked = (antwoord == "1");

                    commando = "get d7";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital7.Checked = (antwoord == "1");
                }
            }
            catch (Exception exeption)
            {
                labelStatus.Text = "Error: " + exeption.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";

            }

        }

        private void timerOefening4_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    serialPortArduino.ReadExisting();
                    string commando = "get a0";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    labelAnalog0.Text = antwoord;
                    int value = Int32.Parse(antwoord);
                    labelAnalog0.Text = value.ToString();


                }
            }
            catch (Exception exeption)
            {
                labelStatus.Text = "Error: " + exeption.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }
    }
}
