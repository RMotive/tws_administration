using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Contracts.Services.Interfaces;
public interface IBusinessService {
    public Task<ForeignSessionModel> Create (AccountIdentityModel Identity);
}