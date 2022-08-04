using ChallengeDominio.VO.EventoSensor.V1;
using ChallengeSimuladorSensores.Services.V1;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace ChallengeSimuladorSensores
{
    class Program
    {
        private static Timer _simuladorTime;
        private static int count = 0;
        private static ServiceEventoSensor serviceEventoSensorReg1;
        private static ServiceEventoSensor serviceEventoSensorReg2;
        private static ServiceEventoSensor serviceEventoSensorReg3;
        private static ServiceEventoSensor serviceEventoSensorReg4;
        private static ServiceEventoSensor serviceEventoSensorReg5;

        private static Random _rnd = new Random();
        private static EventoSensorVO eventoSensorVO1 = new EventoSensorVO();
        private static EventoSensorVO eventoSensorVO2 = new EventoSensorVO();
        private static EventoSensorVO eventoSensorVO3 = new EventoSensorVO();
        private static EventoSensorVO eventoSensorVO4 = new EventoSensorVO();
        private static EventoSensorVO eventoSensorVO5 = new EventoSensorVO();
        static void Main(string[] args )
        {
            serviceEventoSensorReg1 = new ServiceEventoSensor(new HttpClient());
            serviceEventoSensorReg2 = new ServiceEventoSensor(new HttpClient());
            serviceEventoSensorReg3 = new ServiceEventoSensor(new HttpClient());
            serviceEventoSensorReg4 = new ServiceEventoSensor(new HttpClient());
            serviceEventoSensorReg5 = new ServiceEventoSensor(new HttpClient());
            _simuladorTime = new Timer(2000);
            _simuladorTime.Elapsed += SimulaSensorReg1Event;
            _simuladorTime.Elapsed += SimulaSensorReg2Event;
            _simuladorTime.Elapsed += SimulaSensorReg3Event;
            _simuladorTime.Elapsed += SimulaSensorReg4Event;
            _simuladorTime.Elapsed += SimulaSensorReg5Event;
            _simuladorTime.AutoReset = true;
            _simuladorTime.Enabled = true;
            _simuladorTime.Interval = 2000;
            _simuladorTime.Start();
            Console.WriteLine("Iniciado Simulador");
            Console.ReadLine();
        }
     
        private static void SimulaSensorReg1Event(object sender, ElapsedEventArgs e)
        {
            try
            {
                eventoSensorVO1.Id = Guid.NewGuid();
                eventoSensorVO1.Tag = "brasil.sudeste.sensor001";
                eventoSensorVO1.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO1.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg1.InsereEventos(eventoSensorVO1);
                Console.WriteLine(eventoSensorVO1.Tag + ": " + eventoSensorVO1.Valor);
                eventoSensorVO1.Id = Guid.NewGuid();
                eventoSensorVO1.Tag = "brasil.sudeste.sensor002";
                eventoSensorVO1.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO1.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg1.InsereEventos(eventoSensorVO1);
                Console.WriteLine(eventoSensorVO1.Tag + ": " + eventoSensorVO1.Valor);
                eventoSensorVO1.Id = Guid.NewGuid();
                eventoSensorVO1.Tag = "brasil.sudeste.sensor003";
                eventoSensorVO1.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO1.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg1.InsereEventos(eventoSensorVO1);
                Console.WriteLine(eventoSensorVO1.Tag + ": " + eventoSensorVO1.Valor);

            }
            catch (Exception ex)
            {

            }

        }
        private static void SimulaSensorReg2Event(object sender, ElapsedEventArgs e)
        {
            try
            {
                eventoSensorVO2.Id = Guid.NewGuid();
                eventoSensorVO2.Tag = "brasil.sul.sensor001";
                eventoSensorVO2.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO2.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg2.InsereEventos(eventoSensorVO2);
                Console.WriteLine(eventoSensorVO2.Tag + ": " + eventoSensorVO2.Valor);
                eventoSensorVO2.Id = Guid.NewGuid();
                eventoSensorVO2.Tag = "brasil.sul.sensor002";
                eventoSensorVO2.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO2.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg2.InsereEventos(eventoSensorVO2);
                Console.WriteLine(eventoSensorVO2.Tag + ": " + eventoSensorVO2.Valor);
                eventoSensorVO2.Id = Guid.NewGuid();
                eventoSensorVO2.Tag = "brasil.sul.sensor003";
                eventoSensorVO2.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO2.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg2.InsereEventos(eventoSensorVO2);
                Console.WriteLine(eventoSensorVO2.Tag + ": " + eventoSensorVO2.Valor);

            }
            catch (Exception ex)
            {

            }

        }
        private static void SimulaSensorReg3Event(object sender, ElapsedEventArgs e)
        {
            try
            {
                eventoSensorVO3.Id = Guid.NewGuid();
                eventoSensorVO3.Tag = "brasil.norte.sensor001";
                eventoSensorVO3.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO3.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg2.InsereEventos(eventoSensorVO3);
                Console.WriteLine(eventoSensorVO3.Tag + ": " + eventoSensorVO3.Valor);
                eventoSensorVO3.Id = Guid.NewGuid();
                eventoSensorVO3.Tag = "brasil.norte.sensor002";
                eventoSensorVO3.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO3.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg3.InsereEventos(eventoSensorVO3);
                Console.WriteLine(eventoSensorVO3.Tag + ": " + eventoSensorVO3.Valor);
                eventoSensorVO3.Id = Guid.NewGuid();
                eventoSensorVO3.Tag = "brasil.norte.sensor003";
                eventoSensorVO3.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO3.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg3.InsereEventos(eventoSensorVO3);
                Console.WriteLine(eventoSensorVO3.Tag + ": " + eventoSensorVO3.Valor);

            }
            catch (Exception ex)
            {

            }

        }
        private static void SimulaSensorReg4Event(object sender, ElapsedEventArgs e)
        {
            try
            {
                eventoSensorVO4.Id = Guid.NewGuid();
                eventoSensorVO4.Tag = "brasil.nordeste.sensor001";
                eventoSensorVO4.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO4.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg4.InsereEventos(eventoSensorVO4);
                Console.WriteLine(eventoSensorVO4.Tag + ": " + eventoSensorVO4.Valor);
                eventoSensorVO4.Id = Guid.NewGuid();
                eventoSensorVO4.Tag = "brasil.nordeste.sensor002";
                eventoSensorVO4.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO4.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg4.InsereEventos(eventoSensorVO4);
                Console.WriteLine(eventoSensorVO4.Tag + ": " + eventoSensorVO4.Valor);
                eventoSensorVO4.Id = Guid.NewGuid();
                eventoSensorVO4.Tag = "brasil.nordeste.sensor003";
                eventoSensorVO4.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO4.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg4.InsereEventos(eventoSensorVO4);
                Console.WriteLine(eventoSensorVO4.Tag + ": " + eventoSensorVO4.Valor);

            }
            catch (Exception ex)
            {

            }

        }
        private static void SimulaSensorReg5Event(object sender, ElapsedEventArgs e)
        {
            try
            {
                eventoSensorVO5.Id = Guid.NewGuid();
                eventoSensorVO5.Tag = "brasil.centro-oeste.sensor001";
                eventoSensorVO5.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO5.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg5.InsereEventos(eventoSensorVO5);
                Console.WriteLine(eventoSensorVO5.Tag + ": " + eventoSensorVO5.Valor);
                eventoSensorVO5.Id = Guid.NewGuid();
                eventoSensorVO5.Tag = "brasil.centro-oeste.sensor002";
                eventoSensorVO5.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO5.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg5.InsereEventos(eventoSensorVO5);
                Console.WriteLine(eventoSensorVO5.Tag + ": " + eventoSensorVO5.Valor);
                eventoSensorVO5.Id = Guid.NewGuid();
                eventoSensorVO5.Tag = "brasil.centro-oeste.sensor003";
                eventoSensorVO5.Valor = _rnd.Next(0, 100).ToString();
                eventoSensorVO5.TimeStamp = new DateTimeOffset().ToUnixTimeSeconds();
                serviceEventoSensorReg5.InsereEventos(eventoSensorVO5);
                Console.WriteLine(eventoSensorVO5.Tag + ": " + eventoSensorVO5.Valor);

            }
            catch (Exception ex)
            {

            }

        }


    }
}
