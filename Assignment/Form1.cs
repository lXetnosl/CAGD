using System.Reflection;

namespace Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coordinate2D vec1 = new(7, 4, 0);
            Coordinate2D point1 = new(2, 3, 1);
            Coordinate2D vec2 = new(3, 8, 0);
            Coordinate2D point2 = new(5, 2, 1);
            float scalar = 5.3f;

            /*
            textBox1.AppendText((vec1 + vec2).ToString() + Environment.NewLine);
            textBox1.AppendText((vec1 + point1).ToString() + Environment.NewLine);
            textBox1.AppendText((vec1 - vec2).ToString() + Environment.NewLine);
            textBox1.AppendText((point1 - point2).ToString() + Environment.NewLine);
            //textBox1.AppendText((vec1 - point1).ToString() + Environment.NewLine);
            textBox1.AppendText((scalar * vec2).ToString() + Environment.NewLine);
            textBox1.AppendText((vec1 * vec2).ToString() + Environment.NewLine);
            */

            ObjectReader objReader = new();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "BezierCurve_1.obj");
            List<Coordinate2D> coordList = objReader.ReadFile(filePath);

            foreach (Coordinate2D coord in coordList)
            {
                textBox1.AppendText(coord.ToString() + Environment.NewLine);
            }

            DrawObject(coordList, pictureBox1.CreateGraphics(), 10.0f, new Coordinate2D(10, 10, 0));
        }

        private void DrawObject(List<Coordinate2D> coords, Graphics g, float zoom, Coordinate2D displacementVector)
        {
            Pen redPen = new Pen(Color.Red)
            {
                Width = 2
            };
            Pen blackPen = new Pen(Color.Black)
            {
                Width = 2
            };
            List<Coordinate2D> displacedCoords = new();
            foreach (Coordinate2D coord in coords)
            {
                displacedCoords.Add(coord + displacementVector);
            }
            for (int i = 0; i < coords.Count - 1; i++)
            {
                Point point1 = new Point(Convert.ToInt32(displacedCoords[i].x * zoom), Convert.ToInt32(displacedCoords[i].y * zoom));
                Point point2 = new Point(Convert.ToInt32(displacedCoords[i + 1].x * zoom), Convert.ToInt32(displacedCoords[i + 1].y * zoom));
                int radius = Convert.ToInt32(1 * zoom);
                g.DrawEllipse(redPen, point1.X - radius, point1.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(redPen, point2.X - radius, point2.Y - radius, radius * 2, radius * 2);
                g.DrawLine(blackPen, point1, point2);
            }
        }
    }
}