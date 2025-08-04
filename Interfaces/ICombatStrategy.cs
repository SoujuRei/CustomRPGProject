using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Entities;


namespace CustomProjectRPG.Interfaces
{
    public interface ICombatStrats
    {
        void Chase(BeastEntity beast, PlayerEntity player);
        bool TryCorrupt(BeastEntity beast, PlayerEntity player);
        void Flee(BeastEntity beast);
        void Update(BeastEntity beast);
    }
}
