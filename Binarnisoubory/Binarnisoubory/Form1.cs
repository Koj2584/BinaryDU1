using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Binarnisoubory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("znaky.dat", FileMode.OpenOrCreate,FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs,Encoding.GetEncoding("windows-1250"));
            char []chars = { 'a', 'w', 'd', 'w', '?', 'l', '5', '#' };
            foreach (char c in chars)
            {
                bw.Write(c);
            }
            BinaryReader br = new BinaryReader(fs, Encoding.GetEncoding("windows-1250"));
            br.BaseStream.Position = 0;
            int pos = 0, zPos = -1, predZ = '\t', predchozi = '\t';
            while(br.BaseStream.Length > br.BaseStream.Position)
            {
                int znak = br.ReadChar();
                if (znak == '?')
                {
                    zPos = pos;
                    predZ = predchozi;
                }
                predchozi = znak;

                pos++;
            }
            fs.Close();

            MessageBox.Show("Pozice znaku: " + (zPos != -1 ? zPos.ToString() : "Znak nebyl nalezen!!") + "\nZnak před ?: " + (predZ != '\t' ? ((char)predZ).ToString() : "? je prvni nebo neexistuje"));
        }
    }
}
