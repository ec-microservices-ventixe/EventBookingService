using Microsoft.EntityFrameworkCore;
using WebApi.Data.Context;
using WebApi.Data.Entities;
using WebApi.Data.Interfaces;

namespace WebApi.Data.Repositories;

public class EventInfoRepository(ApplicationDbContext context) : Repository<EventInfoEntity>(context), IEventInfoRepository
{
}
