using RESTService.Models.Dto;
using RESTService.Models.Entity;

namespace RESTService.Infrastructure.Interface
{
    public interface IQueueRepository
    {
        Return AddItem(Coins item);
        Return AddItemRange(List<CoinDto> listCoins);
        void ValidateListItem(List<CoinDto> list);
        void VerifyDateValid(string data, int id);
        string GetListItem();
    }
}
