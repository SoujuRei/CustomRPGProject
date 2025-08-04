using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enum = CustomProjectRPG.Entities.Enum.Enum;

namespace CustomProjectRPG.Interfaces
{
    public interface ICorruption
    {
        float GetStatMultiplier();
        Enum.CorruptionState GetCorruptionState();

        bool IsDialogueCorrupted();
        bool IsNoResponse();

        float CorruptionPercentage { get; }

        void IncreaseCorruption(float amount);

        void DecreaseCorruption(float amount);

        string Serialize();
         
        void Deserialize(string json);


    }
}
