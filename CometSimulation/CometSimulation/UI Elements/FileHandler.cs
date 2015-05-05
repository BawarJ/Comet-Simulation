using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace CometSimulation
{
    class FileHandler
    {
        TextReader textReader;
        TextWriter textWriter;
        string Input;

        public FileHandler()
        {

        }

        public void Save(Manager m)
        {
            textWriter = new StreamWriter("state.txt");

            textWriter.WriteLine("#BEGIN#");

            if (m.comets.Count > 0)
            {
                textWriter.WriteLine("#comets#");
                for (int i = 0; i <= m.comets.Count - 1; i++)
                {
                    textWriter.WriteLine(m.comets[i].Position.X);
                    textWriter.WriteLine(m.comets[i].Position.Y);
                    textWriter.WriteLine(m.comets[i].Velocity.X);
                    textWriter.WriteLine(m.comets[i].Velocity.Y);
                    textWriter.WriteLine(m.comets[i].m);
                    textWriter.WriteLine(m.comets[i].density);
                    if (i != m.comets.Count - 1)
                        textWriter.WriteLine("~");
                }
                textWriter.WriteLine("##");
            }

            if (m.planets.Count > 0)
            {
                textWriter.WriteLine("#planets#");
                for (int i = 0; i <= m.planets.Count - 1; i++)
                {
                    textWriter.WriteLine(m.planets[i].Position.X);
                    textWriter.WriteLine(m.planets[i].Position.Y);
                    textWriter.WriteLine(m.planets[i].Velocity.X);
                    textWriter.WriteLine(m.planets[i].Velocity.Y);
                    textWriter.WriteLine(m.planets[i].m);
                    textWriter.WriteLine(m.planets[i].density);
                    if (i != m.planets.Count - 1)
                        textWriter.WriteLine("~");
                }
                textWriter.WriteLine("##");
            }

            textWriter.WriteLine("#stars#");
            foreach (Sun s in m.suns)
            {
                textWriter.WriteLine(s.Position.X);
                textWriter.WriteLine(s.Position.Y);
            }
            textWriter.WriteLine("##");

            textWriter.Write("#END#");
            textWriter.Close();
        }

        public void Load(Manager m)
        {
            textReader = new StreamReader("state.txt");
            Input = textReader.ReadLine();
            while (Input != "#END#")
            {
                Input = textReader.ReadLine();
                switch (Input)
                {
                    case "#comets#":
                        while (Input != "##")
                        {
                            float cposX = float.Parse(textReader.ReadLine());
                            float cposY = float.Parse(textReader.ReadLine());
                            float cvelX = float.Parse(textReader.ReadLine());
                            float cvelY = float.Parse(textReader.ReadLine());
                            float cmass = float.Parse(textReader.ReadLine());
                            float cdensity = float.Parse(textReader.ReadLine());
                            m.createComet(false, cposX, cposY, cvelX, cvelY, cmass, cdensity);
                            Input = textReader.ReadLine();
                        }
                        break;
                     case "#planets#":
                        while (Input != "##")
                        {
                            float pposX = float.Parse(textReader.ReadLine());
                            float pposY = float.Parse(textReader.ReadLine());
                            float pvelX = float.Parse(textReader.ReadLine());
                            float pvelY = float.Parse(textReader.ReadLine());
                            float cmass = float.Parse(textReader.ReadLine());
                            float cdensity = float.Parse(textReader.ReadLine());
                            m.createPlanet(true, pposX, pposY, pvelX, pvelY, cmass, cdensity);
                            Input = textReader.ReadLine();
                        }
                        break;
                    case "#stars#":
                        float sposX = float.Parse(textReader.ReadLine());
                        float sposY = float.Parse(textReader.ReadLine());
                        foreach (Sun s in m.suns)
                            s.Position = new Vector2(sposX, sposY);
                        break;
                }
            }
        }
    }
}
