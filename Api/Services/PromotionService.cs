using Api.Interfaces;
using Api.Models;

namespace Api.Services;

public class PromotionService : IPromotionService
{
    private ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    private ICollection<Participant> Participants { get; set; } = new List<Participant>();
    private ICollection<Prize> Prizes { get; set; } = new List<Prize>();

    public Promotion CreatePromotion(string name, string description)
    {
        var promotion = new Promotion
        {
            Id = Promotions.Count + 1,
            Name = name,
            Description = description
        };

        Promotions.Add(promotion);

        return promotion;
    }

    public void EditPromotion(int id, string name, string description)
    {
        var promotion = Promotions.ElementAt(id - 1);
        promotion.Name = name;
        promotion.Description = description;
    }

    public Promotion GetPromotion(int id)
    {
        return Promotions.ElementAt(id - 1);
    }

    public IEnumerable<Promotion> GetAllPromotions()
    {
        return Promotions;
    }

    public void DeletePromotion(int id)
    {
        Promotions.Remove(Promotions.ElementAt(id - 1));
    }

    public Participant AddParticipant(int id, string name)
    {
        var participant = new Participant
        {
            Id = Participants.Count + 1,
            Name = name
        };

        Participants.Add(participant);
        var promotion = Promotions.ElementAt(id - 1);
        promotion.Participants.Add(participant);

        return participant;
    }

    public void DeleteParticipant(int id, int participantId)
    {
        var promotion = Promotions.ElementAt(id - 1);
        promotion.Participants.Remove(promotion.Participants.ElementAt(participantId));
    }

    public Prize AddPrize(int id, string description)
    {
        var prize = new Prize
        {
            Id = Prizes.Count + 1,
            Description = description
        };

        Prizes.Add(prize);
        var promotion = Promotions.ElementAt(id - 1);
        promotion.Prizes.Add(prize);

        return prize;
    }

    public void DeletePrize(int id, int prizeId)
    {
        var promotion = Promotions.ElementAt(id - 1);
        promotion.Prizes.Remove(promotion.Prizes.ElementAt(prizeId));
    }

    public IEnumerable<Raffle> CreateRaffle(int id)
    {
        var promotion = Promotions.ElementAt(id - 1);
        var participants = promotion.Participants;
        var prizes = promotion.Prizes;

        if (participants.Count != prizes.Count)
        {
            return null;
        }

        var raffle = participants.Select(
            (_, id) => new Raffle
            {
                Winner = participants.ElementAt(id), 
                Prize = prizes.ElementAt(id),
            }).ToList();

        return raffle;
    }
}