﻿using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.AuthenticationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.AuthenticationServices.Contracts
{
    public interface IAdministratorAuthService
    {
        Task<IdentityResult> CreateAdministratorAsync(AdministratorForCreationDto admin);
    }
}