namespace Week1
{
    interface IShape
    {
        void Area();
    }

    interface IColor
    {
        void GetColor();
    }

    class Circle : IShape, IColor
    {
        public double Radius { get; set; }
        public string Color { get; set; }

        public Circle(double radius, string color) => (Radius, Color) = (radius, color);

        public void Area()
        {
            Console.WriteLine("Area: " + (double.Pi * Radius * Radius).ToString("0.###"));
        }

        public void GetColor() => Console.WriteLine("Color: " + Color);

    }
    internal class Question3
    {
        public static void Run()
        {
            Circle circle = new(10, "red");
            circle.Area();
            circle.GetColor();
        }
    }
}
