﻿using System.ComponentModel.DataAnnotations;

namespace MagicCollection.Data.Entities;

public class Edition
{
  [Key]
  public Guid Id { get; set; }
  
  [MaxLength(8)]
  public string Code { get; set; }
  
  [MaxLength(64)]
  public string Name { get; set; }
  
  public DateOnly ReleaseDate { get; set; }
}