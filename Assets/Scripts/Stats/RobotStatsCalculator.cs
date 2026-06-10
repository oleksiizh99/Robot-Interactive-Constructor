public static class RobotStatsCalculator
{
    public static RobotStats Calculate(RobotPartData head, RobotPartData torso, RobotPartData legs)
    {
        return new RobotStats(head.Weight + torso.Weight + legs.Weight, head.Power + torso.Power + legs.Power);
    }
}