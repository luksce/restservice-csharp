using RESTService.Models.Dto;
using RESTService.Models.Entity;

namespace RESTService.Services.Interface
{
    public interface IQueueService
    {
        Return AddItem(Coins item);
        Return AddItemRange(List<CoinDto> listCoins);
        void ValidateListItem(List<CoinDto> queueCoin);
        void VerifyDateValid(string date, int id);
        string GetListItem();
    }
}
