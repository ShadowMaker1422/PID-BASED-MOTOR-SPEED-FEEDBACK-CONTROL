using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ZedGraph;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            String[] Baudrate = {"1200","2400","4800","9600","19200","38400","57600","115200" };
            String[] Direction = { "Thuan", "Nghich" };
            comboBox1.Items.AddRange(Baudrate);
            comboBox3.Items.AddRange(Direction);
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            textBox3.Text = "0.1";//"7.93867";
            textBox4.Text = "0.1";// "6.61556";
            textBox5.Text = "0";// "2.3816";
            textBox6.Text = "100";
            comboBox3.Text = "Thuan";
            comboBox2.DataSource = SerialPort.GetPortNames();
            comboBox1.Text = "9600";
            GraphPane graph = zedGraphControl1.GraphPane;
            graph.Title.Text = "graph";
            graph.YAxis.Title.Text = "value";
            graph.XAxis.Title.Text = "time";
            RollingPointPairList list = new RollingPointPairList(500000);
            LineItem line = graph.AddCurve("data", list, Color.Red, SymbolType.None);
            graph.XAxis.Scale.Max = 10;
            graph.XAxis.Scale.Min = 0;
            graph.XAxis.Scale.MinorStep = 1;
            graph.XAxis.Scale.MajorStep = 1;
            graph.YAxis.Scale.Min = 0;
            graph.YAxis.Scale.Max = 300;
            graph.YAxis.Scale.MinorStep = 1;
            graph.YAxis.Scale.MajorStep = 1;
            zedGraphControl1.AxisChange();
           graph.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            
        }
       double tong = 0;
        public void draw(double line) {
           
            LineItem duongline = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            if (duongline == null) {
                return;
            }
            IPointListEdit list = duongline.Points as IPointListEdit;
            if  (list == null)
            {
                return;
            }
            list.Add(tong,line);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            tong += 0.1; 
       }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("NOT CONNECTED YET!");
            }
            else
            {
                serCOM.Write("OFF");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen) {
                MessageBox.Show("NOT CONNECTED YET!");
                    }
            else {
                serCOM.Write("ON");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("CONNECTED!");
                button3.Text = "DISCONNECT";
                serCOM.PortName = comboBox2.Text;
                serCOM.BaudRate = Convert.ToInt32(comboBox1.Text);
               
                serCOM.Open();
                serCOM.Write("0 0 0 0");
            }
            else
            {
                MessageBox.Show("DISCONNECTED!");
                button3.Text = "CONNECT";
                serCOM.Write("0 0 0 0");
                serCOM.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen) { 
                serCOM.Open();
            }
            serCOM.Write("0 0 0 0");
            serCOM.Close();
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("NOT CONNECTED YET!");
            }
            else { 
            String data = textBox1.Text;
            serCOM.Write(text: data);
                MessageBox.Show("UPDATED!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String data1 = "";
            data1 = serCOM.ReadLine();
            int len = data1.Length;
            if( len > 0){
                textBox2.Text = data1;
                double data2;
                if (double.TryParse(data1, out data2))
                {
                    // Conversion successful, use parsedValue
                    Invoke(new MethodInvoker(() => draw(data2)));
                }
                else
                {
                    MessageBox.Show("WRONG FORMAT!");
                    // Handle invalid input (e.g., show an error message)
                }
               // Invoke(new MethodInvoker(() => draw(Convert.ToDouble(data1))));
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("NOT CONNECTED YET!");
            }
            else
            {
                String data2 = textBox3.Text+" "+textBox4.Text+" "+textBox5.Text+" "+textBox6.Text+" "+comboBox3.Text;
                serCOM.Write(text: data2);
                MessageBox.Show("UPDATED!");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
