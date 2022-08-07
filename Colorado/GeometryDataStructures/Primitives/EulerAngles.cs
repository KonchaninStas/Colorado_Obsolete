namespace Colorado.GeometryDataStructures.Primitives
{
    public class EulerAngles
    {
        public EulerAngles(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        public double Roll { get; }

        public double Pitch { get; }

        public double Yaw { get; }
    }
}
