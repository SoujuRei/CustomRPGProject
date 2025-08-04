using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProjectRPG.Interfaces
{
    public interface IDayListener
    {
        public void OnDayChanged(int day);
        public void OnLoopStart(); 
    }
}
