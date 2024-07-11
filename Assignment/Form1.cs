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
            // render vertices on screen
            renderLayer.Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog to load vertices from file
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
            // handle mouse click events on picturebox for vertex selection, addition and deletion
            MouseEventArgs mouseArgs = (MouseEventArgs)e;

            if (selectButton.Checked)
            {
                renderLayer.GetVertexAt(new Coordinate2D(mouseArgs.X, mouseArgs.Y, 1), true);
                OnSelectionChanged();
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

        private void OnSelectionChanged()
        {
            weightInput.Enabled = renderLayer.SelectedCount > 0;

            switch (renderLayer.SelectedCount)
            {
                case 0:
                    weightInput.Value = 1;
                    weightInput.Controls[0].Enabled = true;
                    weightInput.InterceptArrowKeys = true;
                    weightInput.Text = weightInput.Value.ToString();
                    break;
                case 1:
                    weightInput.Value = Convert.ToDecimal(renderLayer.SelectedVertices[0].W);
                    weightInput.Controls[0].Enabled = true;
                    weightInput.InterceptArrowKeys = true;
                    weightInput.Text = weightInput.Value.ToString();
                    break;
                default:
                    weightInput.Controls[0].Enabled = false;
                    weightInput.InterceptArrowKeys = false;
                    weightInput.Text = "";
                    break;
            }
        }

        private void deleteButton_CheckedChanged(object sender, EventArgs e)
        {
            // set mode to delete vertices on mouse click
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
            // move selected vertices by input values
            float moveXFloat = Convert.ToSingle(moveX.Value);
            float moveYFloat = Convert.ToSingle(moveY.Value);

            Coordinate2D moveVector = new(moveXFloat, Convert.ToSingle(moveYFloat), 0);
            renderLayer.MoveSelected(moveVector);
            renderLayer.Render();
        }

        private void zoomInput_ValueChanged(object sender, EventArgs e)
        {
            // set zoom level for rendering to zoomInput value
            renderLayer.Zoom = Convert.ToInt32(zoomInput.Value);
            renderLayer.Render();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // center object on screen
            renderLayer.CenterObject();
            renderLayer.Render();
        }

        private void selectButton_CheckedChanged(object sender, EventArgs e)
        {
            // enable/disable split button and input
            splitButton.Enabled = selectButton.Checked;
            splitInput.Enabled = selectButton.Checked;
        }

        private void splitButton_Click(object sender, EventArgs e)
        {
            // split edge between selected vertices at splitInput value and create new vertex
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

        private void showBezier_CheckedChanged(object sender, EventArgs e)
        {
            // show bezier curve with selected method
            renderLayer.ShowBezierCheck = !renderLayer.ShowBezierCheck;
            renderLayer.Render();
        }

        private void showControlPoints_CheckedChanged(object sender, EventArgs e)
        {
            // show control points or bernstein polynoms
            if (renderLayer.useDeCasteljau)
                renderLayer.ShowControlPointsCheck = !renderLayer.ShowControlPointsCheck;
            else
                renderLayer.ShowBernsteinPolynoms = !renderLayer.ShowBernsteinPolynoms;
            renderLayer.Render();
        }

        private void tInput_ValueChanged(object sender, EventArgs e)
        {
            // set t value for de casteljau
            renderLayer.T = Convert.ToSingle(tInput.Value);
            renderLayer.Render();
        }

        private void increaseCtrlButton_Click(object sender, EventArgs e)
        {
            // increases the used controlpoints by 1
            renderLayer.increaseControlPoints();
            renderLayer.Render();
        }

        private void weightInput_ValueChanged(object sender, EventArgs e)
        {
            if (renderLayer.SelectedCount <= 0)
            {
                return;
            }
            foreach (var vertex in renderLayer.SelectedVertices)
            {
                vertex.W = Convert.ToSingle(weightInput.Value);
            }
            renderLayer.Render();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            // clear all vertices on screen
            renderLayer.ClearVertices();
            renderLayer.Render();
        }

        private void deCastButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!deCastButton.Checked)
            {
                return;
            }

            // switch to de casteljau mode
            BezierCheckBox.Checked = false;
            controlPointsCheckBox.Checked = false;
            renderLayer.useDeCasteljau = true;
            renderLayer.ShowBezierCheck = false;
            renderLayer.ShowBernsteinPolynoms = false;
            controlPointsCheckBox.Text = "Show Control Points";
            renderLayer.Render();
        }

        private void bernsteinButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!bernsteinButton.Checked)
            {
                return;
            }

            // switch to bernstein polynoms mode
            BezierCheckBox.Checked = false;
            controlPointsCheckBox.Checked = false;
            renderLayer.useDeCasteljau = false;
            renderLayer.ShowBezierCheck = false;
            renderLayer.ShowBernsteinPolynoms = false;
            controlPointsCheckBox.Text = "Show Polynoms";
            renderLayer.Render();
        }

        private void showDerive_CheckedChanged(object sender, EventArgs e)
        {
            // change state of showDerive
            renderLayer.ShowDerivation = !renderLayer.ShowDerivation;
            renderLayer.Render();
        }

        private void splittingButton_Click(object sender, EventArgs e)
        {
            string[] tsAsString = splittingTextBox.Text.Split(';');
            List<float> ts = new List<float>();
            foreach (string tsStr in tsAsString)
            {
                ts.Add(float.Parse(tsStr));
            }

            renderLayer.SplitCurve(ts);
            renderLayer.Render();
        }
    }
}