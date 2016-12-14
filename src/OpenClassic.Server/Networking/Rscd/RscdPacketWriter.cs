﻿using System;
using System.Diagnostics;
using System.Text;

namespace OpenClassic.Server.Networking.Rscd
{
    public class RscdPacketWriter : AbstractPacketWriter
    {
        public void SendServerInfo(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 110);
            WriteLong(session, DateTime.Now.Ticks);
            WriteBytes(session, Encoding.UTF8.GetBytes("Australia"));
            FormatPacket(session);
        }

        public void SendFatigue(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 126);
            WriteShort(session, 50);
            FormatPacket(session);
        }

        public void SendWorldInfo(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 131);

            var location = session.Player.Location;

            WriteShort(session, session.Player.Index);
            WriteShort(session, 2304);
            WriteShort(session, 1776);
            WriteShort(session, 0);
            WriteShort(session, 944);
            FormatPacket(session);
        }

        public void SendInventory(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 114);

            WriteByte(session, 2); // Number of items in inventory

            // TODO: Loop through the inventory here instead of just sending one item.
            WriteShort(session, 10); // ID for coins
            WriteInt(session, 1337); // Amount

            WriteShort(session, 11); // ID for bronze arrows
            WriteInt(session, 1337); // Amount
            // End loop

            FormatPacket(session);
        }

        public void SendBank(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 93);

            var itemCount = 1;
            var maxItems = byte.MaxValue;

            WriteByte(session, itemCount); // Bank item count
            WriteByte(session, maxItems); // Max number of bank items

            WriteShort(session, 10); // Item id
            WriteInt(session, 1337); // Item amount

            FormatPacket(session);
        }

        public void SendCombatStyle(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 129);
            WriteByte(session, 2); // Combat style - 0 to 3 inclusive.
            FormatPacket(session);
        }

        public void SendShowAppearanceScreen(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 207);
            FormatPacket(session);
        }

        public void SendCloseShop(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 220);
            FormatPacket(session);
        }

        public void SendClientConfig(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 152);

            WriteByte(session, 1); // Auto camera angle
            WriteByte(session, 1); // Mouse button setting
            WriteByte(session, 1); // Sound effects
            WriteByte(session, 1); // Show roofs
            WriteByte(session, 1); // Enable automatic screenshots
            WriteByte(session, 1); // Always show combat type window.

            FormatPacket(session);
        }

        public void SendStats(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 180);

            const int statCount = 18;

            for (var i = 0; i < statCount; i++)
            {
                const int currentLevel = 50;
                WriteByte(session, currentLevel);
            }

            for (var i = 0; i < statCount; i++)
            {
                const int baseLevel = 50;
                WriteByte(session, baseLevel);
            }

            for (var i = 0; i < statCount; i++)
            {
                const int experience = 100000;
                WriteInt(session, experience);
            }

            FormatPacket(session);
        }

        public void SendLoginBox(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 248);

            const int daysSinceLastLogin = 180;
            WriteShort(session, daysSinceLastLogin);

            const int subscriptionDaysLeft = 30;
            WriteShort(session, subscriptionDaysLeft);

            WriteBytes(session, Encoding.UTF8.GetBytes("127.0.0.1"));

            FormatPacket(session);
        }

        public void SendCantLogout(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 136);
            FormatPacket(session);
        }

        public void SendDied(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 165);
            FormatPacket(session);
        }

        public void SendPrayers(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 209);
            // TODO: Finish
        }

        #region Update packets

        public void SendPlayerPositionUpdate(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 145);

            var player = session.Player;
            var loc = player.Location;

            WriteBits(session, loc.X, 11);
            WriteBits(session, loc.Y, 13);
            WriteBits(session, 7, 4); // sprite
            WriteBits(session, 0, 8); // number of known players

            // TODO: Loop through each known player, send update information

            // TODO: Loop through each new player, send update information

            FormatPacket(session);
        }

        public void SendNpcPositionUpdate(ISession session)
        {
            Debug.Assert(session != null);

            CreatePacket(session, 77);

            WriteBits(session, 0, 8); // Number of known NPCs

            // TODO: Loop through known NPCs here.

            // TODO: Loop through each new NPC here

            FormatPacket(session);
        }

        public void SendGameObjectUpdate(ISession session)
        {
            Debug.Assert(session != null);

            // TODO: Implement the 'send game objects' packet.
            // Note: Only send this packet if the player's watched game objects have changed.

            if (false) // If the player's visible game objects have changed...
            {
#pragma warning disable CS0162 // Unreachable code detected
                CreatePacket(session, 27);
#pragma warning restore CS0162 // Unreachable code detected

                // TODO: Loop through known objects, remove any that are no longer visible.

                // TODO: Loop through new objects.

                FormatPacket(session);
            }
        }

        public void SendWallObjectUpdate(ISession session)
        {
            Debug.Assert(session != null);

            // TODO: Implement the 'send wall objects' packet.
            // Note: Only send this packet if the player's watched wall objects have changed.

            if (false) // If the player's visible wall objects have changed...
            {
#pragma warning disable CS0162 // Unreachable code detected
                CreatePacket(session, 95);
#pragma warning restore CS0162 // Unreachable code detected

                // TODO: Loop through known wall objects, remove any that are no longer visible.

                // TODO: Loop through new wall objects.

                FormatPacket(session);
            }
        }

        public void SendItemUpdate(ISession session)
        {
            Debug.Assert(session != null);

            // TODO: Implement the 'send item update' packet.
            // Note: Only send this packet if the player's watched items have changed.

            if (false) // If the player's visible items have changed...
            {
#pragma warning disable CS0162 // Unreachable code detected
                CreatePacket(session, 109);
#pragma warning restore CS0162 // Unreachable code detected

                // TODO: Loop through known items, remove any that are no longer visible.

                // TODO: Loop through any new items.

                FormatPacket(session);
            }
        }

        public void SendPlayerAppearanceUpdate(ISession session)
        {
            Debug.Assert(session != null);

            // TODO: Implement the 'send player appearance update' packet.
            // Note: This packet is only sent if something about the player's worldview has changed.
            // Changes include: Bubbles, chat messages, hit updates, projectiles, and player appearances.

            if (false)
            {
#pragma warning disable CS0162 // Unreachable code detected
                CreatePacket(session, 53);
#pragma warning restore CS0162 // Unreachable code detected

                WriteShort(session, 0); // Update size.

                // TODO: Implement the following:
                // 1. Loop through bubbles
                // 2. Loop through chat messages
                // 3. Loop through players requiring hit (damage) updates
                // 4. Loop through projectiles
                // 5. Loop through player appearance updates

                FormatPacket(session);
            }
        }

        public void SendNpcAppearanceUpdate(ISession session)
        {
            Debug.Assert(session != null);

            // TODO: Implement the 'send NPC appearance update' packet.
            // Note: This packet is only sent if NPC chat messages or damage hits need to be displayed.

            if (false)
            {
#pragma warning disable CS0162 // Unreachable code detected
                CreatePacket(session, 190);
#pragma warning restore CS0162 // Unreachable code detected

                WriteShort(session, 0); // Update size.

                // TODO: Loop through NPC chat messages.
                // TODO: Loop through NPC hit updates.

                FormatPacket(session);
            }
        }

        #endregion
    }
}