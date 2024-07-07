using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Form1 : Form
    {
        private RenderLayer renderLayer;

        public Form1()
        {
            InitializeComponent();
            renderLayer = new RenderLayer(pictureBox1);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            renderLayer.Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            renderLayer.AddFromFile(filePath);
            renderLayer.Render();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (MouseEventArgs)e;

            if (selectButton.Checked)
            {
                renderLayer.GetVertexAt(new Coordinate2D(mouseArgs.X, mouseArgs.Y, 1), true);
            }
            else if (addButton.Checked)
            {
                Coordinate2D transformedMouseCoords = new Coordinate2D(mouseArgs.X, mouseArgs.Y, 1);
                renderLayer.AddVertex(transformedMouseCoords);
            }
            else if (deleteButton.Checked)
            {
                renderLayer.DeleteVertexAt(new Coordinate2D(mouseArgs.X, mouseArgs.Y, 1));
            }

            renderLayer.Render();
        }

        private void deleteButton_CheckedChanged(object sender, EventArgs e)
        {
            if (deleteButton.Checked)
            {
                if (renderLayer.SelectedCount > 0 && MessageBox.Show($"Delete {renderLayer.SelectedCount} Vertices?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                renderLayer.DeleteSelected();
                renderLayer.Render();
            }

        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            float moveXFloat = Convert.ToSingle(moveX.Value);
            float moveYFloat = Convert.ToSingle(moveY.Value);

            Coordinate2D moveVector = new(moveXFloat, Convert.ToSingle(moveYFloat), 0);
            renderLayer.MoveSelected(moveVector);
            renderLayer.Render();
        }

        private void zoomInput_ValueChanged(object sender, EventArgs e)
        {
            renderLayer.Zoom = Convert.ToInt32(zoomInput.Value);
            renderLayer.Render();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            renderLayer.CenterObject();
            renderLayer.Render();
        }

        private void selectButton_CheckedChanged(object sender, EventArgs e)
        {
            splitButton.Enabled = selectButton.Checked;
            splitInput.Enabled = selectButton.Checked;
        }

        private void splitButton_Click(object sender, EventArgs e)
        {
            string invalidSelectionText = "Please select 2 connected vertices to indicate which edge should be split.\n" +
                    "Note: Selection order matters for split location.\n";
            if (renderLayer.SelectedCount != 2)
            {
                MessageBox.Show(invalidSelectionText + $"\nCurrently Selected: {renderLayer.SelectedCount}",
                    "Invalid Selection",
                    MessageBoxButtons.OK);
                return;
            }
            else
            {
                bool success = renderLayer.SplitSelected(Convert.ToSingle(splitInput.Value));
                if (!success)
                {
                    MessageBox.Show(invalidSelectionText + "\nCurrently selected vertices do not build an edge.",
                        "Invalid Selection",
                        MessageBoxButtons.OK);
                }
                renderLayer.Render();
            }
        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            //initialize the bezier curve from obj file
//            //clears old bezier, if any
//            ObjectReader objReader = new();
//            List<Coordinate2D> coordList = objReader.ReadFile("BezierCurve_1.obj");
//            foreach (Coordinate2D coord in coordList)
//            {
//                textBox1.AppendText(coord.ToString() + Environment.NewLine);
//            }
//            this.bezier = new(coordList);
//        }
//
//        private void DrawBezierCurve(Bezier bezier, Graphics g, float zoom, Coordinate2D displacementVector)
//        {
//            //Draw the bezier curve
//            //initialize pens
//            Pen redPen = new Pen(Color.Red)
//            {
//                Width = 2
//            };
//            Pen blackPen = new Pen(Color.Black)
//            {
//                Width = 2
//            };
//            //draw curve
//            List<Coordinate2D> curvePoints = new();
//            for (float t = -1; t <= 2; t += 0.01f)
//            {
//                Coordinate2D curvePoint = bezier.GetCurvePoint(t);
//                curvePoints.Add(curvePoint + displacementVector);
//            }
//            for (int i = 0; i < curvePoints.Count - 1; i++)
//            {
//                Point point1 = new Point(Convert.ToInt32(curvePoints[i].x * zoom), Convert.ToInt32(curvePoints[i].y * zoom));
//                Point point2 = new Point(Convert.ToInt32(curvePoints[i + 1].x * zoom), Convert.ToInt32(curvePoints[i + 1].y * zoom));
//                int radius = Convert.ToInt32(1 * zoom);
//                g.DrawLine(blackPen, point1, point2);
//            }
//        }
//
//        private void button3_Click(object sender, EventArgs e)
//        {
//            //add control point
//            float x = float.Parse(numericUpDown1.Value.ToString());
//            float y = float.Parse(numericUpDown2.Value.ToString());
//            Coordinate2D newPoint = new(x, y, 1);
//            textBox1.AppendText(newPoint.ToString() + Environment.NewLine);
//            bezier.AddPoint(newPoint);
//        }
//
//        private void button4_Click(object sender, EventArgs e)
//        {
//            //remove control point
//            int index = Convert.ToInt32(numericUpDown3.Value);
//            try
//            {
//                bezier.RemovePoint(index);
//            }
//            catch (ArgumentOutOfRangeException ex)
//            {
//                textBox1.AppendText(ex.Message + Environment.NewLine);
//            }
//        }
//
//        private void button6_Click(object sender, EventArgs e)
//        {
//            //draw the bezier curve
//            if (bezier.controlpoints.Count > 0)
//            {
//                pictureBox1.Refresh();
//                //draw the curve
//                DrawObject(bezier.controlpoints, pictureBox1.CreateGraphics(), 10.0f, new Coordinate2D(40, 30, 0));
//                DrawBezierCurve(bezier, pictureBox1.CreateGraphics(), 10.0f, new Coordinate2D(40, 30, 0));
//            }
//        }
//
//        private void button5_Click(object sender, EventArgs e)
//        {
//            //refresh the picturebox
//            pictureBox1.Refresh();
//        }
//
//        private void button7_Click(object sender, EventArgs e)
//        {
//            //clear the textbox
//            textBox1.Clear();
//        }

    }
}