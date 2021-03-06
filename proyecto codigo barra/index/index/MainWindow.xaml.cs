﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Timers;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Threading;
using WpfApplication1.Clases;
using System.Threading;

namespace WpfApplication1

{
    public partial class MainWindow : Window
    {
        List<List<string>> Menu = new MenuDefault().allMenu();
        List<string> frasesDiarias = new FrasesDiarias().allFrases();

        public MainWindow()
        {
            InitializeComponent();
            this.tRut.Focus();
            this.lMision.Text = "   Desarrollar e implementar las mejores soluciones de Gestión de Recursos Humanos " +
                                "para el mercado global y local. Siempre de acuerdo a los más altos estándares de calidad," +
                                "las necesidades de nuestros clientes y las tendencias del mercado," +
                                " garantizamos un servicio altamente eficiente en tiempo, coste y calidad.";

            reloj_.Content = DateTime.Now.ToLongTimeString();//Doy la hora actual al reloj
            carga_dias.Content = new Random().Next(1, 5).ToString();//DIAS SIN ACCIDENTE
            frase_.Text = frasesDiarias[new Random().Next(frasesDiarias.Count)];//FRASE ALEATORIA DIARIA
            menu_.Content = "";
            foreach (string list in Menu[new Random().Next(Menu.Count)]) menu_.Content += "*"+list+"\n";//CARGA MENU ALEATORIO
            cargar_tiempo();//CARGA EL TIEMPO
            
            //Timer que actualiza el relog
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            reloj_.Content = DateTime.Now.ToLongTimeString();
        }


        private void cargar_tiempo()
        {
            try
            {
                String[] diasEng = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
                String[] diasEs = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
                XmlDocument pagina = new XmlDocument();
                pagina.Load("http://weather.yahooapis.com/forecastrss?w=349871&u=c");

                XmlNamespaceManager man = new XmlNamespaceManager(pagina.NameTable);
                man.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

                XmlNode chanel = pagina.SelectSingleNode("rss").SelectSingleNode("channel");
                String build = chanel.SelectSingleNode("lastBuildDate").InnerText;
                String cuidad = chanel.SelectSingleNode("yweather:location", man).Attributes["city"].Value;
                String region = chanel.SelectSingleNode("yweather:location", man).Attributes["region"].Value;
                String country = chanel.SelectSingleNode("yweather:location", man).Attributes["country"].Value;
                String temperature = chanel.SelectSingleNode("yweather:units", man).Attributes["temperature"].Value;
                String chill = chanel.SelectSingleNode("yweather:wind", man).Attributes["chill"].Value;
                String humidity = chanel.SelectSingleNode("yweather:atmosphere", man).Attributes["humidity"].Value;
                String sunrise = chanel.SelectSingleNode("yweather:astronomy", man).Attributes["sunrise"].Value;
                String sunset = chanel.SelectSingleNode("yweather:astronomy", man).Attributes["sunset"].Value;
                String cdata = chanel.SelectSingleNode("item", man).SelectSingleNode("description").InnerText;//.InnerText;

                this.lCity.Content = cuidad + ", " + country;//CIUDAD, PAIN
                for (int i = 0; i < diasEng.Length; i++)
                    if (diasEng[i] == build.Substring(0, 3)) this.lDate.Content = diasEs[i] + ", " + build.Substring(16, 5); //CARGA EL DIA EN ESPAÑOL
                this.lTemperature.Content = chill + "º" + temperature;//CARCA LA TEMPERATURA ACTUAL
                string imgTiempo = cdata.Substring(cdata.Substring(11, 40).Replace("\"/>", "").Length + 5, 2).Replace("\"/>", "");//OBTIENE LA IMAGEN DEL TIEMPO
                this.lImageTiempo.Source = new BitmapImage(new Uri(string.Format("https://s.yimg.com/os/mit/media/m/weather/images/icons/l/{0}n-100567.png", imgTiempo)));//CARGA IMAGEN DEL TIEMPO
            }
            catch (Exception e)
            {
                MessageBox.Show("Revise su conexion a internet para mostrar datos metereologico.");
                Console.WriteLine("ERROR MainWindow.cargar_tiempo() " + e.Message.ToString());
            }
        }


        private void birthDay(Persona per) 
        {
            //Console.WriteLine(Fecha.Substring(0, 4) + " " + int.Parse(Fecha.Substring(5, 2)) + " " + Fecha.Substring(8, 2));
            int diaCumple = int.Parse(per.fecha_nac.Substring(8, 2));//Dia del Cumpleanios
            int mesCumple = int.Parse(per.fecha_nac.Substring(5, 2));//Mes de Cumple 4=Abril
            int anioCumple = int.Parse(per.fecha_nac.Substring(0, 4)); //Anio de Cumple

           // DateTime proximoCumple = new DateTime(DateTime.Now.Year, mesCumple, diaCumple);
            TimeSpan faltan = new DateTime(DateTime.Now.Year, mesCumple, diaCumple).Subtract(DateTime.Now);
            int diasRestantes = faltan.Days >= 0 ? faltan.Days : 365 + faltan.Days;

            this.imagen_cumple.Visibility = Visibility.Hidden;
            if (diasRestantes >= 0)
            {
                if (diasRestantes > 0 && faltan.Hours != 0)cumple_label.Text = "Faltan " + diasRestantes + " dias para tu cumpleaños ";
                else if (faltan.Hours != 0 && faltan.Hours > 0)
                    cumple_label.Text = "Faltan " + faltan.Hours + ":" + faltan.Minutes + " horas para tu cumpleaños";
                else {
                    cumple_label.Text = "Feliz cumpleaños " + per.nombre.ToUpper() + " que tengas un buen dia.";
                    this.imagen_cumple.Visibility = Visibility.Visible; 
                }
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void image2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }
        //BORAR DATOS CAMPOS
        public void Clear() {
            Thread.Sleep(1000);
            this.tRut.Text = "";
            this.tRut.Focus();
            this.tName.Text = "";
            this.cumple_label.Text = "";
            this.imagen_cumple.Visibility = Visibility.Hidden;

        }

        private void tRut_keyDown(object sender, KeyEventArgs e)
        {
            Persona dato = null;
            if (e.Key == Key.Enter) {
                String rut = new Validaciones().validaRut(this.tRut.Text.Trim(),this.tRut);
                dato = new Persona(rut).findByRut();
                if (dato != null)
                {
                    this.tRut.IsEnabled = false;
                    this.tRut.Text = dato.rut;
                    this.tName.Text = dato.nombre;
                    birthDay(dato);
                    String id_personal = new Persona(this.tRut.Text).get_idPersonal().ToString();
                    String fecha = new Validaciones().DateFormat(DateTime.Today.ToString("d"));
                    String llegada = /*int.Parse(*/this.reloj_.Content.ToString()/*) < 12 ? this.reloj_.Content.ToString() : ""*/;
                    String salida = "0";// int.Parse(this.reloj_.Content.ToString().Split(':')[0]) > 12 ? this.reloj_.Content.ToString() : "";
                    
                    RegistroHorario horario = new RegistroHorario(id_personal, fecha, llegada, salida);
                    if (horario.save() > 0) Console.WriteLine("OKEY REGISTRO ENTRADA" + llegada+" "+ salida);
                    else {
                        salida = this.reloj_.Content.ToString();
                        RegistroHorario h2 = new RegistroHorario(id_personal,fecha,llegada,salida);
                        h2.update(); Console.WriteLine("OKEY REGISTRO SALIDA" + llegada + " " + salida); 
                    }


                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        Thread.Sleep(2000);
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                this.tRut.IsEnabled = true;
                                Clear();// this.lDate.Content = result;
                            }));
                    });   
                }
                else MessageBox.Show("No se encuentra registrado como empleado.");
            }
            
        }

    }
}
