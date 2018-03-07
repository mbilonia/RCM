﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RCM.Application.ViewModels
{
    public class EstadoViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Cidades")]
        public ICollection<CidadeViewModel> Cidades { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}