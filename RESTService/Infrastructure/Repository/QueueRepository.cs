using RESTService.Models.Dto;
using RESTService.Models.Entity;
using RESTService.Infrastructure.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RESTService.Models.Entity;
using RESTService.Data;

namespace RESTService.Infrastructure.Repository
{
    public class QueueRepository : IQueueRepository
    {
        public Return AddItem(Coins item)
        {
            try
            {
                using (var contxt = new RESTContext())
                {
                    contxt.Set<Coins>().Add(item);
                    int r = contxt.SaveChanges();
                    return new Return("Incluido com sucesso.");
                }
            }
            catch(Exception ex)
            {
                return new Return(ex);
            }
        }

        public Return AddItemRange(List<CoinDto> listCoins)
        {
            try
            {
                ValidateListItem(listCoins);
                var newList = new List<Coins>();
                int quantidadeItem = 0;
                listCoins.ForEach(x =>
                {
                    newList.Add(new Coins()
                    {
                        Moeda = x.moeda,
                        DataInicio = DateTime.Parse(x.data_inicio),
                        DataFim = DateTime.Parse(x.data_fim)
                    });
                    quantidadeItem++;
                });

                using (var contxt = new RESTContext())
                {
                    contxt.Set<Coins>().AddRange(newList);
                    int r = contxt.SaveChanges();
                    return new Return("Incluido com sucesso.");
                }
            }
            catch (Exception ex)
            {
                return new Return(ex);
            }
        }

        public string GetListItem()
        {
            try
            {
                using(var contxt = new RESTContext())
                {
                    var lastItemList = contxt.Coins.ToList().LastOrDefault<Coins>();
                    if(lastItemList != null)
                    {
                        var lastDelete = contxt.Coins.Find(lastItemList.Id);
                        contxt.Coins.Remove(lastDelete);
                        contxt.SaveChanges();

                        return JsonConvert.SerializeObject(lastItemList, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd" }).ToString();
                    }
                    return JsonConvert.SerializeObject(new Coins() { Id = -1, Mensagem = "Nao existe registro a ser retornado." });
                }
            }
            catch(Exception exc)
            {
                return JsonConvert.SerializeObject(new Coins() { Id = -1, Mensagem = exc.Message });
            }
        }

        public void ValidateListItem(List<CoinDto> list)
        {
            int quantidadeItem = 0;
            try{
                list.ForEach(x =>
                {
                    if (x.moeda.Length != 3) throw new Exception("Moeda com valor inválido.");
                    VerifyDateValid(x.data_inicio, quantidadeItem);
                    VerifyDateValid(x.data_fim, quantidadeItem);
                    x.Id = quantidadeItem++;
                });
            }
            catch
            {
                throw;
            }
        }

        public void VerifyDateValid(string date, int id)
        {
            try
            {
                DateTime _date = DateTime.Parse(date);
            }
            catch
            {
                throw new Exception($"Index: {id} da lista com Data Inválida.");
            }
        }
    }
}
