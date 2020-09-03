using System;
using Sys = Cosmos.System;

namespace MOS
{
    public class Kernel : Sys.Kernel
    {
        private GUI gui;
        private FileSystem fileSystem;
        protected override void BeforeRun()
        {
            Console.WriteLine("MOS Successfully booted !");
            Console.WriteLine("Copyright Marius Van Nieuwenhuyse");
            this.fileSystem = new FileSystem();
            this.gui = new GUI();
        }

        protected override void Run()
        {
            try
            {
                this.gui.draw();
                this.WaitBetweenFrames();
            }
            catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                mDebugger.Send(e.Message);
            }
        }

        // TODO Replace by a PIT.Wait when the bug is resolved
        private void WaitBetweenFrames()
        {
            Cosmos.Core.Global.CPU.Halt();
        }
    }
}
