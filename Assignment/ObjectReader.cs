using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class ObjectReader
    {
        public ObjectReader() 
        {
        }

        public List<Coordinate2D> ReadFile(string path)
        {
            StreamReader reader = null;
            try
            {
                List<Coordinate2D> coordinates = new();
                using (reader = new(path))
                {
                    string currentLine;
                    while((currentLine = reader.ReadLine()) != null)
                    {
                        if (currentLine == string.Empty || currentLine[0] != 'v')
                        {
                            continue;
                        }

                        string[] valueStrings = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        float[] values = new float[valueStrings.Length - 1];
                        for(int i = 1; i < valueStrings.Length; i++)
                        {
                            values[i - 1] = float.Parse(valueStrings[i], CultureInfo.InvariantCulture.NumberFormat);
                        }

                        coordinates.Add(new Coordinate2D(values[0], values[1], 1));
                    }
                }
                return coordinates;
            }
            catch (Exception)
            {
                reader.Close();
                throw;
            }
        }
    }
}
