﻿using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Trekking:BaseNameEntity
    {
        public ICollection<TourTrekking> TourTrekkings { get; set; }
    }
}