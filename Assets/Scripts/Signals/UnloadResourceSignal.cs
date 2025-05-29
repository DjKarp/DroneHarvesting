namespace DroneHarvesting
{
    public class UnloadResourceSignal
    {
        public DroneData.DroneTeam DroneTeamResource;

        public UnloadResourceSignal(DroneData.DroneTeam droneTeam)
        {
            DroneTeamResource = droneTeam;
        }
    }
}
