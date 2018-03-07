﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RCM.Application.ViewModels
{
    public class NotaFiscalViewModel
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo Id é requerido.")]
        public int Id { get; set; }

        [Display(Name = "Número do documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo número do documento é requerido.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "O campo número do documento deve ter 6 caracteres.")]
        public string NumeroDocumento { get; set; }

        [Display(Name = "Duplicatas")]
        public virtual ICollection<DuplicataViewModel> Duplicatas { get; set; }

        [Display(Name = "Data de Emissão")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo data de emissão é requerido.")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Data de Chegada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo data de chegada é requerido.")]
        public DateTime DataChegada { get; set; }

        [Display(Name = "Valor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo valor é requerido.")]
        [Range(0, 9999, ErrorMessage = "O campo valor deve ter entre 1 e 5 caracteres.")]
        public decimal Valor { get; set; }

        public override string ToString()
        {
            return NumeroDocumento;
        }
    }
}