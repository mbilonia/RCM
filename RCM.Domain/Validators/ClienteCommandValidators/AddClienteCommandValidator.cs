﻿using RCM.Domain.Commands.ClienteCommands;

namespace RCM.Domain.Validators.ClienteCommandValidators
{
    public class AddClienteCommandValidator : ClienteCommandValidator<AddClienteCommand>
    {
        public AddClienteCommandValidator()
        {
            ValidateNome();
            ValidatePontuacao();
            ValidateDescricao();
            ValidateDocumento();
            ValidateContato();
            ValidateEndereco();
        }
    }
}
