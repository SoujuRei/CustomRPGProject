using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomProjectRPG.Interfaces;

namespace CustomProjectRPG

{
    public class APComponent: ISerializeJSON<APComponent>
    {
        private int _currentAP;
        private const int _maxAP = 5;

        public APComponent()
        {
            _currentAP = _maxAP;
        }

        public int CurrentAP
        {
            get { return _currentAP; }
        }

        public int MaxAP
        {
            get { return _maxAP; }
        }

        public bool UseAP(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("AP usage must be greater than zero.");
            }

            if (_currentAP < amount)
            {
                return false;
            }

            _currentAP -= amount;
            return true;
        }

        public void Reset()
        {
            _currentAP = _maxAP;
        }

        public bool IsDepleted()
        {
            return _currentAP == 0;
        }


        public string Serialize()
        {
            return JsonSerializer.Serialize(_currentAP);
        }

        public void Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return;
            int value = JsonSerializer.Deserialize<int>(json);
            _currentAP = Math.Clamp(value, 0, _maxAP);
        }

    }


}
