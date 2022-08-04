using System;
using Microsoft.Extensions.Localization;

namespace ChallengeDominio
{
    public interface IEntidade
    {
        Guid Id { get; set; }
    }
}
