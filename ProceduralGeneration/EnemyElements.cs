using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public static class EnemyElements
    {
        private static Random random = new Random();

        private static List<Func<Point, XElement>> enemyElements = new List<Func<Point, XElement>>()
        {
            Stalfos,
            Goriya,
            Keese,
            Rope,
            Gel

        };

        public static XElement RandomEnemy(Point location)
        {
            int randNum = random.Next(0, enemyElements.Count);
            return enemyElements[randNum](location);
        }
        public static XElement Stalfos(Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", "Stalfos"),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }

        public static XElement Goriya(Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", "Goriya"),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }

        public static XElement Keese(Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", "Keese"),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }

        public static XElement Gel(Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", "Gel"),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }

        public static XElement Rope(Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", "Rope"),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }
    }
}
