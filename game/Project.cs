using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace game
{
    public enum Weight
    {
        Null,
        Small,
        Medium,
        High
    }
    public enum Risk
    {
        Null,
        Small,
        Medium,
        High
    }
    public class Project
    {
        private string name;
        private int[] checkIndexes;
        public bool used;
        private int p;
        private int c;
        private Weight weight;
        private Risk risk;
        private string imgName;

        public string Name { get => name; set => name = value; }
        public int[] CheckIndexes { get => checkIndexes; set => checkIndexes = value; }
        public int P { get => p; set => p = value; }
        public int C { get => c; set => c = value; }
        public Weight Weight { get => weight; set => weight = value; }
        public Risk Risk { get => risk; set => risk = value; }
        public string ImgName { get => imgName; set => imgName = value; }

        public Project(string name, int[] checkindexes, int p, int c, Weight weight, Risk risk, string imgName)
        {
            this.name = name;
            this.checkIndexes = checkindexes;
            this.used = false;
            this.p = p;
            this.c = c;
            this.weight = weight;
            this.risk = risk;
            this.imgName = imgName;
        }
        private static Project Null()
        {
            return new Project("Nulo", new int[] { -1 }, 0, 0, Weight.Null, Risk.Null, "black.png");
        }
        public static Project[] InitializeProjects()
        {
            Project[] projects;

            string jsonString = File.ReadAllText(Main.projectDirectory + "cases\\projects.json");
            projects = JsonSerializer.Deserialize<Project[]>(jsonString);

            return projects;
        }
        public static Project GetRandomProject(Project[] projects, int n)
        {
            if (n >= projects.Length) //Condição de parada, provavelmente temporario
                return Project.Null();

            Random random = new Random();
            int projectIndex = random.Next(projects.Length);
            while (projects[projectIndex].IsUsed())
            {
                projectIndex = random.Next(projects.Length);
            }
            return projects[projectIndex];
        }
        public bool IsUsed()
        {
            if (used) return true;
            else
            {
                used = true;
                return false;
            }
        }
        public string RiskText()
        {
            switch (this.risk)
            {
                case Risk.Null:
                    return "Nulo";
                case Risk.Small:
                    return "Pequeno";
                case Risk.Medium:
                    return "Médio";
                case Risk.High:
                    return "Grave";
                default:
                    return "";
            }
        }
        public string WeightText()
        {
            switch (this.weight)
            {
                case Weight.Null:
                    return "Nulo";
                case Weight.Small:
                    return "Pequeno";
                case Weight.Medium:
                    return "Médio";
                case Weight.High:
                    return "Grande";
                default:
                    return "";
            }
        }
    }
}
