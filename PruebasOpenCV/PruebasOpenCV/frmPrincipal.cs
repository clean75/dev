using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace OcultaCaras
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var haarCascade = new CascadeClassifier("dat/haarcascade_frontalface_default.xml");
            var lbpCascade = new CascadeClassifier("dat/lbpcascade_frontalface.xml");
            var haarTree = new CascadeClassifier("dat/haarcascade_frontalface_alt_tree.xml");
            Mat haarResult = DetectFace(haarCascade);
            Mat lbpResult = DetectFace(lbpCascade);
            Mat hatResult = DetectFace(haarTree);

            Cv2.ImShow("Faces by Haar", haarResult);
            Cv2.ImShow("Faces by LBP", lbpResult);
            Cv2.ImShow("Faces by Treee", haarResult);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private Mat DetectFace(CascadeClassifier cascade)
        {
            Mat result;

            using (var src = new Mat("c:/local/yalta.jpg", ImreadModes.Color))
            using (var gray = new Mat())
            {
                result = src.Clone();
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                //Rect[] faces = cascade.DetectMultiScale(
                //    gray, 1.08, 2, HaarDetectionType.ScaleImage, new Size(30, 30));
                Rect[] faces = cascade.DetectMultiScale(
                    gray, 1.01, 3, HaarDetectionType.ScaleImage);

                // Render all detected faces
                foreach (Rect face in faces)
                {
                    var center = new Point
                    {
                        X = (int)(face.X + face.Width * 0.5),
                        Y = (int)(face.Y + face.Height * 0.5)
                    };
                    var axes = new Size
                    {
                        Width = (int)(face.Width * 0.5),
                        Height = (int)(face.Height * 0.5)
                    };
                    Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(255, 0, 255), 4);
                }
            }
            return result;
        }
    }
}
