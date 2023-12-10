using Announcements.Domain.Enums;
using Announcements.Domain.Models;
using Bogus;

namespace Announcements.Domain.Stub;

public static class AnnouncementGenerator
{
    public static AnnouncementDTO Create(int announcementId)
    {
        var faker = GetFaker();
        var announcement = faker.Generate();
        announcement.Id = announcementId;
        return announcement;
    }
    
    public static List<AnnouncementDTO> CreateList()
    {
        var faker = GetFaker();
        var random = new Bogus.Randomizer();
        return faker.Generate(random.Int(1, 7));
    }

    private static Faker<AnnouncementDTO> GetFaker()
    {
        return new Faker<AnnouncementDTO>()
            .RuleFor(u => u.Id, f => f.Random.Number(1, 1000))
            .RuleFor(u => u.Title, f => f.Lorem.Sentence())
            .RuleFor(u => u.Description, f => f.Lorem.Paragraph())
            .RuleFor(u => u.Price, f => f.Commerce.Price(10, 1000))
            .RuleFor(u => u.Category, f => f.PickRandom<Category>())
            .RuleFor(u => u.Images, f => f.Make(5, () => f.Image.PicsumUrl()));
    }
}