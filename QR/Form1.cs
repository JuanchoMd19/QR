using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(txtCodigo.Text.Trim(), out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var image = new Bitmap(imageTemporal, new Size(new Point(200, 200)));
            imgQR.BackgroundImage = image;
            image.Save("imagen.png", ImageFormat.Png);
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image image = (Image)imgQR.BackgroundImage.Clone();
            SaveFileDialog CajaDialogoGuardar = new SaveFileDialog();
            CajaDialogoGuardar.AddExtension = true;
            CajaDialogoGuardar.Filter = "Image PNG (*.png)|*.png";
            CajaDialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDialogoGuardar.FileName))
            {
                image.Save(CajaDialogoGuardar.FileName, ImageFormat.Png);
            }
            image.Dispose();
        }
    }
}
