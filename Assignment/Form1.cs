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
    }
}