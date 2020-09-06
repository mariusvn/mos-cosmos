using System;
using Sys = Cosmos.System;

namespace MOS
{
    public class Kernel : Sys.Kernel
    {
        public static GUI.GUI gui;
        public static FileSystem fileSystem;
        protected override void BeforeRun()
        {
            Console.WriteLine("MOS Successfully booted !");
            Console.WriteLine("Copyright Marius Van Nieuwenhuyse");
            Kernel.fileSystem = new FileSystem();
            try
            {
                new GUI.PixelFont(@"0:\font.pf");
            } catch (Exception e)
            {
                mDebugger.Send("Error: " + e.Message);
            }
            Kernel.gui = new GUI.GUI();
            //GUI.drawables.ContextMenu menu = new GUI.drawables.ContextMenu(100, 150);
            //menu.AddMenuItem("This is a text", () =>
            //{
            //    return;s
            //});
        }

        protected override void Run()
        {
            try
            {
                Kernel.gui.draw();
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
