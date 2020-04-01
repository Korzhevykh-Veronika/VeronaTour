using System;

namespace VeronaTour.DAL.Entites
{
    public class Tour
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public int Price { get; set; }
        public int MinPeopleCount { get; set; }
        public int MaxPeopleCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] Image { get; set; }
        public bool HotTour { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int CountOfTour { get; set; }

        public virtual TourType Type { get; set; }
        public virtual FeedingType FeedingType { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Country Country { get; set; }

    }
}
