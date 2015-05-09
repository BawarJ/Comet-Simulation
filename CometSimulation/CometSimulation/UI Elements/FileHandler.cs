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
        
        //This function will save the simulation state when called
        public void Save(Manager m)
        {
            //Specifies the file to write to
            textWriter = new StreamWriter("state.txt");

            textWriter.WriteLine("#BEGIN#"); //Start of file

            //If comets are present save their variables
            if (m.comets.Count > 0)
            {
                textWriter.WriteLine("#comets#"); //Start of comets
                for (int i = 0; i <= m.comets.Count - 1; i++)
                {
                    textWriter.WriteLine(m.comets[i].Position.X);
                    textWriter.WriteLine(m.comets[i].Position.Y);
                    textWriter.WriteLine(m.comets[i].Velocity.X);
                    textWriter.WriteLine(m.comets[i].Velocity.Y);
                    textWriter.WriteLine(m.comets[i].Mass);
                    textWriter.WriteLine(m.comets[i].Density);
                    if (i != m.comets.Count - 1)
                        textWriter.WriteLine("~"); //End of comet
                }
                textWriter.WriteLine("##"); //End of comets
            }

            if (m.planets.Count > 0)
            {
                //If planets are present save their variables
                textWriter.WriteLine("#planets#"); //Start of planets
                for (int i = 0; i <= m.planets.Count - 1; i++)
                {
                    textWriter.WriteLine(m.planets[i].Position.X);
                    textWriter.WriteLine(m.planets[i].Position.Y);
                    textWriter.WriteLine(m.planets[i].Velocity.X);
                    textWriter.WriteLine(m.planets[i].Velocity.Y);
                    textWriter.WriteLine(m.planets[i].Mass);
                    textWriter.WriteLine(m.planets[i].Density);
                    if (i != m.planets.Count - 1)
                        textWriter.WriteLine("~"); //End of planet
                }
                textWriter.WriteLine("##"); //End of planets
            }

            //Save the sun position
            textWriter.WriteLine("#stars#");
            foreach (Sun s in m.sun)
            {
                textWriter.WriteLine(s.Position.X);
                textWriter.WriteLine(s.Position.Y);
            }
            textWriter.WriteLine("##"); //End of stars

            textWriter.Write("#END#"); //End of file
            textWriter.Close();
        }

        //This function will load the simulation state when called
        public void Load(Manager m)
        {
            textReader = new StreamReader("state.txt");
            Input = textReader.ReadLine();
            while (Input != "#END#")
            {
                Input = textReader.ReadLine();
                switch (Input)
                {
                    //Load and create any comets
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

                    //Load and create any planets
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

                    //Load sun position
                    case "#stars#":
                        float sposX = float.Parse(textReader.ReadLine());
                        float sposY = float.Parse(textReader.ReadLine());
                        foreach (Sun s in m.sun)
                            s.Position = new Vector2(sposX, sposY);
                        break;
                }
            }
        }
    }
}
