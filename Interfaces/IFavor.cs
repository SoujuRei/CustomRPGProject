using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Interfaces
{
    public interface IFavor
    {
        void GrantFavor(string entityId, int amount);
        Enum.FavorResult CheckFavorResult(string entityId);
    }
}
