namespace DroneHarvesting
{
    public interface IDroneState
    {
        void EnterState(Drone drone);
        void UpdateState();
    }
}
