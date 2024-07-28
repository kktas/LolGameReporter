using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Cache.Models
{
    public record Command(long UserId, string CommandName);
}
