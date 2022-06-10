using RESTService.Infrastructure.Interface;
using RESTService.Models.Dto;
using RESTService.Models.Entity;
using RESTService.Services.Interface;

namespace RESTService.Services
{
    public class QueueService : IQueueService
    {   
        private readonly IQueueRepository _queueRepository;

        public QueueService(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }
        
        public Return AddItem(Coins item)
        {
            return _queueRepository.AddItem(item);
        }

        public Return AddItemRange(List<CoinDto> listCoins)
        {
            return _queueRepository.AddItemRange(listCoins);
        }
        
        public string GetListItem()
        {
            return _queueRepository.GetListItem();
        }
        public void ValidateListItem(List<CoinDto> list)
        {
            _queueRepository.ValidateListItem(list);
        }
        public void VerifyDateValid(string date, int id)
        {
            _queueRepository.VerifyDateValid(date, id);
        }
    }
}
