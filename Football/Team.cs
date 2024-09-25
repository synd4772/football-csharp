using System;
using System.Collections.Generic;
using System.Numerics;

namespace Football;

// Meeskonna klass, mis sisaldab mängijaid ja mängu
public class Team
{
    // Mängijate loend
    public List<Player> Players { get; } = new List<Player>();
    // Meeskonna nimi
    public string Name { get; private set; }
    // Seotud mäng
    public Game Game { get; set; }

    // Konstruktor, mis määrab meeskonna nime
    public Team(string name)
    {
        Name = name;
    }

    // Mängu alustamine antud laiuse ja kõrgusega
    public void StartGame(int width, int height)
    {
        Random rnd = new Random();
        // Määrab iga mängija positsiooni juhuslikult
        foreach (var player in Players)
        {
            player.SetPosition(
                rnd.NextDouble() * width,
                rnd.NextDouble() * height
                );
        }
    }

    // Mängija lisamine meeskonda
    public void AddPlayer(Player player)
    {
        // Kontrollib, kas mängija kuulub juba mõnda meeskonda
        if (player.Team != null) return;
        Players.Add(player); // Lisab mängija meeskonda
        player.Team = this;   // Seob mängija selle meeskonnaga
    }

    // Tagastab palli positsiooni
    public (double, double) GetBallPosition()
    {
        return Game.GetBallPositionForTeam(this);
    }

    // Määrab palli kiirus (vx, vy)
    public void SetBallSpeed(double vx, double vy)
    {
        Game.SetBallSpeedForTeam(this, vx, vy);
    }

    // Otsib lähima mängija palli
    public Player GetClosestPlayerToBall()
    {
        Player closestPlayer = Players[0]; // Eeldab, et esimene mängija on lähim
        double bestDistance = Double.MaxValue; // Algne kaugus on maksimaalne
        // Iga mängija kauguse arvutamine pallist
        foreach (var player in Players)
        {
            var distance = player.GetDistanceToBall();
            if (distance < bestDistance) // Kui kaugus on väiksem, uuendab lähimat mängijat
            {
                closestPlayer = player;
                bestDistance = distance;
            }
        }

        return closestPlayer; // Tagastab lähima mängija
    }

    // Liikuma asumine
    public void Move()
    {
        // Lähim mängija liigub palli poole
        GetClosestPlayerToBall().MoveTowardsBall();
        // Kõik mängijad liiguvad
        Players.ForEach(player => player.Move());
    }
}
