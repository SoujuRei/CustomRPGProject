using CustomProjectRPG.Entities;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.World
{
    public interface IInteractableObjects
    {

        string Name { get; }
        string Description { get; }
        void Interact(PlayerEntity player);

    }
}
