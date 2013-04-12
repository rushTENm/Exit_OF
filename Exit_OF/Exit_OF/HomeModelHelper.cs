using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Exit_OF
{
    class HomeModelHelper
    {
        GameModel homeBathroom = new GameModel();
        GameModel homeBathroomSink = new GameModel();
        GameModel homeCeiling = new GameModel();
        GameModel homeCouch = new GameModel();
        GameModel homeCurtain = new GameModel();
        GameModel homeDoor = new GameModel();
        GameModel homeElevator = new GameModel();
        GameModel homeFloor = new GameModel();
        GameModel homeKitchenSink = new GameModel();
        GameModel homeKitchenTable = new GameModel();
        GameModel homeKitchenware = new GameModel();
        GameModel homeRoomBig = new GameModel();
        GameModel homeRoomSmall = new GameModel();
        GameModel homeRoomSmallBed = new GameModel();
        GameModel homeRoomSmallChest = new GameModel();
        GameModel homeStairs = new GameModel();
        GameModel homeTable = new GameModel();
        GameModel homeTV = new GameModel();
        GameModel homeWallFront = new GameModel();
        GameModel homeWindow = new GameModel();

        public void Init(ContentManager content)
        {
            homeBathroom.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeBathroom");
            homeBathroomSink.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeBathroomSink");
            homeCeiling.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeCeiling");
            homeCouch.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeCouch");
            homeCurtain.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeCurtain");
            homeDoor.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeDoor");
            homeElevator.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeElevator");
            homeFloor.Init(content, 1, Vector3.Zero, @"HomeEScreen\home\homeFloor");
            homeKitchenSink.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeKitchenSink");
            homeKitchenTable.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeKitchenTable");
            homeKitchenware.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeKitchenware");
            homeRoomBig.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeRoomBig");
            homeRoomSmall.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeRoomSmall");
            homeRoomSmallBed.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeRoomSmallBed");
            homeRoomSmallChest.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeRoomSmallChest");
            homeStairs.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeStairs");
            homeTable.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeTable");
            homeTV.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeTV");
            homeWallFront.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeWallFront");
            homeWindow.Init(content, 1f, Vector3.Zero, @"HomeEScreen\home\homeWindow");
        }

        public void Draw(ChaseCamera camera)
        {
            homeBathroom.DrawMeshes(camera);
            homeBathroomSink.DrawMeshes(camera);
            homeCeiling.DrawMeshes(camera);
            homeCouch.DrawMeshes(camera);
            homeCurtain.DrawMeshes(camera);
            homeDoor.DrawMeshes(camera);
            homeElevator.DrawMeshes(camera);
            homeFloor.DrawMeshes(camera);
            homeKitchenSink.DrawMeshes(camera);
            homeKitchenTable.DrawMeshes(camera);
            homeKitchenware.DrawMeshes(camera);
            homeRoomBig.DrawMeshes(camera);
            homeRoomSmall.DrawMeshes(camera);
            homeRoomSmallBed.DrawMeshes(camera);
            homeRoomSmallChest.DrawMeshes(camera);
            homeStairs.DrawMeshes(camera);
            homeTable.DrawMeshes(camera);
            homeTV.DrawMeshes(camera);
            homeWallFront.DrawMeshes(camera);
            homeWindow.DrawMeshes(camera);
        }
    }
}
