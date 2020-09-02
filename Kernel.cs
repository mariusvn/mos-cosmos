using Cosmos.Debug.Kernel;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace MOS
{
    public class Kernel : Sys.Kernel
    {
        private GUI gui;
        private PIT pit = new PIT();
        protected override void BeforeRun()
        {
            Console.WriteLine("MOS Successfully booted !");
            Console.WriteLine("Copyright Marius Van Nieuwenhuyse");
            this.gui = new GUI();
        }

        protected override void Run()
        {
            try
            {
                this.gui.draw();
            } catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                mDebugger.Send(e.Message);
            }
        }

        private void DelayCode(uint milliseconds)
        {
            this.pit.Wait(milliseconds);
        }
    }
}
