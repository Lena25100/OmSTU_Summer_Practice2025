namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();      
        void Rotate(int angle);  
        void Fire();             
        int Speed { get; }       
        int FirePower { get; }   
    }
    public class Cruiser : ISpaceship
    {
        public int Position { get; private set; } = 0;
        public int Angle { get; private set; } = 0;
        public int MissilCount { get; private set; } = 100;
        public int Speed { get; } = 50;
        public int FirePower { get; } = 100;

        public void MoveForward()
        {
            Position += Speed;
        }

        public void Rotate(int angle)
        {
            Angle = (360 + Angle + angle % 360) % 360;
        }
        public void Fire()
        {
            if (MissilCount > 0) MissilCount -= 1;

        }
    }

    public class Fighter : ISpaceship
    {
        public int Position { get; private set; } = 0;
        public int Angle { get; private set; } = 0;
        public int MissilCount { get; private set; } = 100;
        public int Speed { get; } = 100;
        public int FirePower { get; } = 50;

        public void MoveForward()
        {
            Position += Speed;
        }

        public void Rotate(int angle)
        {
            Angle = (360 + Angle + angle % 360) % 360;
        }
        public void Fire()
        {
            if (MissilCount > 0) MissilCount -= 1;

        }
    }
}
