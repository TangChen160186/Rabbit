namespace Rabbit_Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using Window window = new Window(800, 600, "LearnOpenTK");
            //Run takes a double, which is how many frames per second it should strive to reach.
            //You can leave that out and it'll just update as fast as the hardware will allow it.
            window.RenderFrequency = 60;
            window.Run();
        }
    }
}