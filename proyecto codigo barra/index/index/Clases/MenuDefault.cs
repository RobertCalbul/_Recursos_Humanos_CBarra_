using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.Clases
{
    class MenuDefault
    {
        List<List<string>> Menu = new List<List<string>>();
        List<string> m1 = new List<string>();
        List<string> m2 = new List<string>();
        List<string> m3 = new List<string>();
        List<string> m4 = new List<string>();
        List<string> m5 = new List<string>();
        List<string> m6 = new List<string>();
        List<string> m7 = new List<string>();
        public List<List<string>> allMenu()
        {
            m1.Add("Pure con Huevo.");
            m1.Add("Papas fritas con bistec.");
            m1.Add("Pollo asado al merken.");

            m2.Add("Vegano.");
            m2.Add("Pastel de choclo.");

            m3.Add("Porotos con longaniza.");
            m3.Add("Pastel de choclo.");
            m3.Add("Pollo con arroz.");

            m4.Add("Vegano.");
            m4.Add("Pastel de choclo.");
            m4.Add("Sopaipilla con palta.");

            m5.Add("Ensalada de garbanzos.");
            m5.Add("Tallarines mediterraneo.");
            m5.Add("Filete ruso.");
            m5.Add("Ensalada de arroz.");
            m5.Add("Flan de atún con tomates.");

            m6.Add("Crema de espinacas.");
            m6.Add("Tortilla de espallagos.");
            m6.Add("Crema de zanahorias.");
            m6.Add("Merluza en salsa verde.");

            Menu.Add(m1);
            Menu.Add(m2);
            Menu.Add(m3);
            Menu.Add(m4);
            Menu.Add(m5);
            Menu.Add(m6);

            return Menu;
        }

    }
}
