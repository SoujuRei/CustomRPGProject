using CustomProjectRPG.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Interfaces
{
     public interface ICollidable
    {
        Hitbox Hitbox { get; }
    }
}
