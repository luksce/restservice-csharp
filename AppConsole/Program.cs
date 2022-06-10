using AppConsole.Model;
using Newtonsoft.Json;

namespace AppConsole
{
    class Program
    {
        static string pathBase = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("ConsoleAppThead")) + @"ConsoleAppThead\\DocsCsv";
        static bool flag = true;
        static void Main(string[] args)
        {
            Console.WriteLine("AppConsole");
            var tarefa = new Thread(ExecutarTarefa);
            tarefa.Start();
        }

        static async void ExecutarTarefa()
        {
            var listaDadosCotacao = ListaDadosCotacaoCsv();

            int seq = 0;
            while (flag)
            {
                seq++;
                var tempo = DateTime.Now;
                var moeda = await GetUltimoItem();
                var listaMoedas = ListaDadosMoedaCsv(moeda);
                var listaCotacao = ListaDadosCotacaoCsv();
                var listaMoedaCotacao = ListaMoedaCotacaoJson();

                var resultado = (from p in listaMoedas
                                 join d in listaMoedaCotacao on p.ID_MOEDA equals d.ID_MOEDA
                                 join m in listaCotacao on d.cod_cotacao equals m.cod_cotacao
                                 select new ResultadoMoedaCotacao
                                 {
                                     ID_MOEDA = p.ID_MOEDA,
                                     DATA_REF = m.dat_cotacao,
                                     VL_COTACAO = m.vlr_cotacao
                                 }).ToList();

                if (resultado.Count > 0)
                {
                    GravarResultadoCsv(resultado);
                }
                var Tempo = DateTime.Now - tempo;
                string restTempo = Tempo.ToString();
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.Write($"\n {seq} executou no tempo de: {restTempo} a {moeda.Moeda}, {moeda.Mensagem}\n");
            }

        }

        static async Task<Moedas> GetUltimoItem()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:41031/api/Queue/");
                    var resposta = client.GetAsync("GetQueueItem").Result;
                    if (resposta.IsSuccessStatusCode)
                    {
                        string resp = await resposta.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Moedas>(resp);
                    }
                    return new Moedas() { Id = -1, Mensagem = "Retorno sem sucesso." };
                }
            }
            catch (Exception ex)
            {
                return new Moedas()
                {
                    Id = -1,
                    Mensagem = ex.Message.Contains("No connection could be made because the target machine actively refused it")
                    ? "Sem comunicacao com API, verificar servico WEBAPI..." : ex.Message
                };
            }

        }

        static List<DadosMoeda> ListaDadosMoedaCsv(Moedas moeda)
        {

            var listaDadosMoeda = new List<DadosMoeda>();
            var lista = File.ReadAllLines(@$"{pathBase}\DadosMoeda.csv");
            foreach (var lin in lista)
            {

                if (lin != "ID_MOEDA;DATA_REF")
                {
                    string[] values = lin.Split(';');
                    listaDadosMoeda.Add(new DadosMoeda()
                    {
                        ID_MOEDA = values[0].ToString(),
                        DATA_REF = Convert.ToDateTime(values[1])
                    });
                }
            }

            return listaDadosMoeda.Where(
                x => x.ID_MOEDA == moeda.Moeda
                && x.DATA_REF >= moeda.DataInicio
                && x.DATA_REF <= moeda.DataFim).ToList();
        }

        static List<DadosCotacao> ListaDadosCotacaoCsv()
        {
            var listaDadosCotacao = new List<DadosCotacao>();
            var lista = File.ReadAllLines(@$"{pathBase}\DadosCotacao.csv");
            foreach (var lin in lista)
            {

                if (lin != "vlr_cotacao;cod_cotacao;dat_cotacao")
                {
                    string[] values = lin.Split(';');
                    listaDadosCotacao.Add(new DadosCotacao()
                    {
                        vlr_cotacao = decimal.Parse(values[0].ToString()),
                        cod_cotacao = int.Parse(values[1].ToString()),
                        dat_cotacao = Convert.ToDateTime(values[2])
                    });
                }
            }

            return listaDadosCotacao.ToList();
        }

        private static List<MoedaCotacao> ListaMoedaCotacaoJson()
        {
            var listaMoedaCotacao = new List<MoedaCotacao>();
            using (StreamReader r = new StreamReader(pathBase + @"\moedacotacao.json"))
            {

                string dados = r.ReadToEnd();
                listaMoedaCotacao = JsonConvert.DeserializeObject<List<MoedaCotacao>>(dados);
            }

            return listaMoedaCotacao;
        }

        private static void GravarResultadoCsv(List<ResultadoMoedaCotacao> resultado)
        {
            var nomeArquivo = "Resultado_" + DateTime.Now.ToString("yyyymmdd_HHmmss") + ".csv";

            using (var file = File.CreateText(pathBase + @"\" + nomeArquivo))
            {
                file.WriteLine("ID_MOEDA;DATA_REF;VL_COTACAO");
                foreach (var arr in resultado)
                {
                    file.WriteLine(arr.ID_MOEDA + ";" + arr.DATA_REF + ";" + arr.VL_COTACAO);
                }
            }
        }
    }
}
