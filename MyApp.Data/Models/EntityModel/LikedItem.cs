﻿using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class LikedItem:BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string? Items { get; set; }

}
