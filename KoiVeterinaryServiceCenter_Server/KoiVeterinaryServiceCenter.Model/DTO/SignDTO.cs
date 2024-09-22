﻿using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Model.DTO;

public class SignDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}