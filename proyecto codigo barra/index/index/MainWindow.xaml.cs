using System;
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

namespace WpfApplication1

{
    public partial class MainWindow : Window
    {
        List<List<string>> Menu = new MenuDefault().allMenu();
        List<string> frasesDiarias = new FrasesDiarias().allFrases();
        public MainWindow()
        {
            InitializeComponent();
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
            XmlDocument pagina = new XmlDocument();
            pagina.Load("http://weather.yahooapis.com/forecastrss?w=349871&u=c");

            XmlNamespaceManager man = new XmlNamespaceManager(pagina.NameTable);
            man.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
            String[] diasEng = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            String[] diasEs = { "Lunes","Martes","Miercoles","Jueves","Viernes","Sabado","Domingo"};
            
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
            for (int i = 0; i < diasEng.Length; i++) this.lDate.Content = diasEng[i] == build.Substring(0, 3) ? diasEs[i]+ ", " + build.Substring(16, 5) : "";//CARGA EL DIA EN ESPAÑOL
            this.lTemperature.Content =chill+"º"+temperature;//CARCA LA TEMPERATURA ACTUAL
            string imgTiempo = cdata.Substring(cdata.Substring(11, 40).Replace("\"/>", "").Length + 5, 2).Replace("\"/>", "");//OBTIENE LA IMAGEN DEL TIEMPO
            this.lImageTiempo.Source = new BitmapImage(new Uri(string.Format("https://s.yimg.com/os/mit/media/m/weather/images/icons/l/{0}n-100567.png", imgTiempo)));//CARGA IMAGEN DEL TIEMPO
        
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void image2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }



    }
}
