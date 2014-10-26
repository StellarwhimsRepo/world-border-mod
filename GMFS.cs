namespace EMFSBeaconsSource
{
    using Sandbox.Common;
    using Sandbox.Common.Components;
    using Sandbox.Common.ObjectBuilders;
    using System;
    using VRageMath;

    /// <summary>
    /// Adds logic to the Cockpit cube, allowing the Cockpit to broadcast world border proximity (caveat: in a sphere) information in the sector.
    /// requires an Antenna to broadcast.
    /// 
    /// The "gamey" explanation is this allows the Cockpit instruments to measure the natural Gravimetric field
    /// that the asteroids emit so you can be wary of when you are approaching the edge of the field.
    /// 
    /// Author: Tostito
    /// Credit for original ideas and script: Midspace, Draygo
    /// </summary>
    /// <example>
    /// To use, simply add the proper key to the very start of the Cockpit Name field.
    /// For Distance to world border (sphere): "Grav Field:"
    /// </example>
    /// <see cref="http://steamcommunity.com/id/tostito80/myworkshopfiles/?appid=244850"/>
    /// <see cref="http://steamcommunity.com/id/ScreamingAngels/myworkshopfiles/?appid=244850"/>
    /// <see cref="http://steamcommunity.com/id/DraygoKorvan/myworkshopfiles/?appid=244850"/>
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_Cockpit))]
    public class EMFSLogic : MyGameLogicComponent
    {
        
        private const string CustomNameKeyEMFS = "Grav Field:";
        //i dont know how to use the worldsizekm to return the number it is set to.
        //private Int32 Border = Sandbox.ModAPI.MyAPIGateway.Utilities.ConfigDedicated.SessionSettings.WorldSizeKm;
        private int Border = (30000);  //change this to fit your world border ( world size divided by 2)
        public override void Close()
        {
        }

        public override void Init(Sandbox.Common.ObjectBuilders.MyObjectBuilder_EntityBase objectBuilder)
        {
            Entity.NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;

        }

        public override void MarkForClose()
        {
        }

        public override void UpdateAfterSimulation()
        {

        }

        public override void UpdateAfterSimulation10()
		{
			try
			{
				var beacon = (Sandbox.ModAPI.Ingame.IMyTerminalBlock)Entity;
				
				if (beacon.CustomName.Length >= CustomNameKeyEMFS.Length)
				{
					if (beacon.CustomName != null && beacon.CustomName.Substring(0, CustomNameKeyEMFS.Length).ToLower() == CustomNameKeyEMFS.ToLower())
					{
						var pos = beacon.CubeGrid.GridIntegerToWorld(beacon.Position);
                        double orig = ((Border) - (Math.Sqrt(Math.Pow(pos.X,2) + Math.Pow(pos.Y,2) + Math.Pow(pos.Z,2)))) / 1000;
						beacon.SetCustomName(string.Format("{0} [Dist:{1:N1} km]", CustomNameKeyEMFS, orig));
					}
				}
			}
			catch (Exception)
			{

			}

		}
        public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy)
        {
            return Entity.GetObjectBuilder();
        }
        public override void UpdateAfterSimulation100()
        {
        }

        public override void UpdateBeforeSimulation()
        {
        }

        public override void UpdateBeforeSimulation10()
        {
        }

        public override void UpdateBeforeSimulation100()
        {
        }

        public override void UpdateOnceBeforeFrame()
        {
        }
    }

}
