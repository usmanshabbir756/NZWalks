﻿namespace NZWalks.API.Models.Dto
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
