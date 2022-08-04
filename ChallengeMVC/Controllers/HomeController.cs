using ChallengeAplicacao.Operacoes;
using ChallengeAplicacao.Operacoes.Sensores;
using ChallengeDominio.Cache.Localidade;
using ChallengeDominio.Conversores.Localidades.V1;
using ChallengeDominio.Conversores.Sensores.V1;
using ChallengeDominio.Model.Sensores;
using ChallengeDominio.VO.Localidades.V1;
using ChallengeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChallengeMVC.Controllers
{

    public class HomeController : Controller
    {
        private ILogger Logger { get; }
        private IOperacoesSensores OperacaoSensores { get; }
        private IServiceProvider ServiceProvider { get; }
        private IStringLocalizer StringLocalizer { get; }
        private ICacheLocalidade CacheLocalidade { get; }
        private IOpcoesEventosSensores OpcoesEventos { get; }
        private IOperacoesLocalidade OperacoesLocalidade { get; }

        private  IEnumerable<LocalidadeVO> LocalidadesVO { get;}

        public HomeController(ILogger logger,
            IOperacoesSensores operacaoSensores,
            IOperacoesLocalidade operacoesLocalidade,
            IServiceProvider serviceProvider,
            IStringLocalizer stringLocalizer,
            IOpcoesEventosSensores opcoesEventos,
            ICacheLocalidade cacheLocalidade)
        {
            Logger = logger;
            OperacaoSensores = operacaoSensores;
            ServiceProvider = serviceProvider;
            StringLocalizer = stringLocalizer;
            CacheLocalidade = cacheLocalidade;
            OpcoesEventos = opcoesEventos;
            OperacoesLocalidade = operacoesLocalidade;
            LocalidadesVO = new ConversorLocalidade().ListaConverte(OperacoesLocalidade.CarregaCache());

        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Grafico()
        {
            List<EventoSensorGraficoViewModel> vm = new List<EventoSensorGraficoViewModel>();
            EventoSensorGraficoViewModel itemVm;
            var eventoSensors= OperacaoSensores.CarregaEventosSensores(OpcoesEventos);
            var eventosLocalidade = eventoSensors.GroupBy(p => p.IdLocalidade);
            foreach(var evLoc in eventosLocalidade)
            {
                var infoLocalidade = LocalidadesVO.Where(p => p.Id == evLoc.Key).FirstOrDefault();
                itemVm = new EventoSensorGraficoViewModel();
                itemVm.Regiao = (infoLocalidade.Pais.Nome + "." + infoLocalidade.Regiao.Nome).ToLower();
                itemVm.Total = eventoSensors.Where(p => p.IdLocalidade == evLoc.Key).Count();

                vm.Add(itemVm);
            }

          
            return Json(vm);
        }

        public ActionResult TabelaEventosSensores()
        {
           
            EventoSensorViewModel vm = new EventoSensorViewModel();
            vm.EventosSensoresVO = new ConversorEventoSensor(CacheLocalidade, StringLocalizer)
                .ListaConverte(OperacaoSensores.CarregaEventosSensores(OpcoesEventos)).OrderBy(p=>p.TimeStamp);
            vm.LocalidadesVO = LocalidadesVO;
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public ActionResult Pesquisa(EventoSensorViewModel model, string button)
        {
            return View();
        }
    }
}
