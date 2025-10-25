using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Infrastructure
{
    public interface IConnectFactory
    {
        IDbConnection CreateConnection();
    }
}
