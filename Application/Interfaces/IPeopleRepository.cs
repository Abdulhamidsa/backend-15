using Application.DTOs;
using Application.RowClasses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPeopleRepository
    {
        Task<List<CelebritySummaryRow>> GetPopularAsync();
        Task<CelebrityProfileRow?> GetProfileAsync(string nconst);
    }
}
