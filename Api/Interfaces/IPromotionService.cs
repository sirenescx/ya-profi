using Api.Models;

namespace Api.Interfaces;

public interface IPromotionService
{
    public Promotion CreatePromotion(string name, string description);

    public void EditPromotion(int id, string name, string description);

    public Promotion GetPromotion(int id);

    public IEnumerable<Promotion> GetAllPromotions();

    public void DeletePromotion(int id);

    public Participant AddParticipant(int id, string name);

    public void DeleteParticipant(int id, int participantId);
    
    public Prize AddPrize(int id, string description);

    public void DeletePrize(int id, int prizeId);

    public IEnumerable<Raffle> CreateRaffle(int id);
}